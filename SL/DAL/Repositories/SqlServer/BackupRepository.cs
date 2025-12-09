using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SL.DAL.Contracts;

namespace SL.DAL.Repositories.SqlServer
{
    /// <summary>
    /// Implementación SQL Server del repositorio de backup y restore.
    /// Permite crear archivos .bak, verificar su integridad y restaurar bases de datos,
    /// incluyendo operaciones con MOVE cuando provienen de otra máquina.
    /// </summary>
    internal class BackupRepository : IBackupRepository
    {
        /// <summary>
        /// Cadena de conexión hacia la base de datos objetivo.
        /// </summary>
        private readonly string _cs;

        /// <summary>
        /// Cadena de conexión hacia la base master, necesaria para operaciones de RESTORE.
        /// </summary>
        private readonly string _csMaster;

        /// <summary>
        /// Inicializa la instancia del repositorio, configurando la conexión
        /// hacia master de forma automática.
        /// </summary>
        /// <param name="cs">Cadena de conexión a la base de datos a respaldar.</param>
        /// <exception cref="ArgumentNullException">Si la cadena es nula o vacía.</exception>
        public BackupRepository(string cs)
        {
            if (string.IsNullOrWhiteSpace(cs))
                throw new ArgumentNullException(nameof(cs));

            _cs = cs;
            var b = new SqlConnectionStringBuilder(cs) { InitialCatalog = "master" };
            _csMaster = b.ConnectionString;
        }

        /// <summary>
        /// Genera un archivo .bak de la base de datos configurada.
        /// </summary>
        /// <param name="backupFolder">Carpeta destino del archivo .bak.</param>
        /// <param name="backupName">Nombre base opcional del archivo.</param>
        /// <param name="copyOnly">Indica si debe usar COPY_ONLY para no alterar la cadena de backups.</param>
        /// <param name="ct">Token de cancelación.</param>
        /// <returns>Ruta completa del archivo .bak generado.</returns>
        /// <exception cref="ArgumentException">Si la carpeta es inválida.</exception>
        public async Task<string> BackupAsync(string backupFolder, string backupName = null, bool copyOnly = true, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(backupFolder))
                throw new ArgumentException("Carpeta de backup vacía.", nameof(backupFolder));

            Directory.CreateDirectory(backupFolder);

            var dbName = new SqlConnectionStringBuilder(_cs).InitialCatalog;
            var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = $"{(string.IsNullOrWhiteSpace(backupName) ? dbName : backupName)}_{ts}.bak";
            var fullPath = Path.Combine(backupFolder, fileName);

            var sql = $"BACKUP DATABASE [{dbName}] TO DISK=@p0 WITH {(copyOnly ? "COPY_ONLY," : "")} INIT, STATS=5;";

            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, cn) { CommandTimeout = 0 })
            {
                cmd.Parameters.AddWithValue("@p0", fullPath);
                await cn.OpenAsync(ct).ConfigureAwait(false);
                await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            }

            return fullPath;
        }

        /// <summary>
        /// Cambia el estado de la base a MULTI_USER,
        /// utilizado después de ejecutar un RESTORE.
        /// </summary>
        /// <param name="setMulti">SQL para establecer MULTI_USER.</param>
        /// <param name="ct">Token de cancelación.</param>
        private async Task EnsureMultiUserAsync(string setMulti, CancellationToken ct)
        {
            try
            {
                using (var cn = new SqlConnection(_csMaster))
                using (var cmd = new SqlCommand(setMulti, cn) { CommandTimeout = 0 })
                {
                    await cn.OpenAsync(ct).ConfigureAwait(false);
                    await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                }
            }
            catch
            {
                // No se relanza porque este error no debe ocultar el error original del restore.
            }
        }

        /// <summary>
        /// Restaura una base desde un archivo .bak, con o sin MOVE según rutas.
        /// </summary>
        /// <param name="backupFileFullPath">Ruta del archivo .bak.</param>
        /// <param name="withReplace">Indica si debe usarse WITH REPLACE.</param>
        /// <param name="dataDir">Directorio destino para .mdf (solo si se usa MOVE).</param>
        /// <param name="logDir">Directorio destino para .ldf (solo si se usa MOVE).</param>
        /// <param name="ct">Token de cancelación.</param>
        /// <exception cref="ArgumentException">Si la ruta del .bak es inválida.</exception>
        /// <exception cref="FileNotFoundException">Si el archivo .bak no existe.</exception>
        public async Task RestoreAsync(string backupFileFullPath, bool withReplace = false, string dataDir = null, string logDir = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(backupFileFullPath))
                throw new ArgumentException("Ruta .bak vacía.", nameof(backupFileFullPath));

            if (!global::System.IO.File.Exists(backupFileFullPath))
                throw new FileNotFoundException("No se encontró el .bak", backupFileFullPath);

            var dbName = new SqlConnectionStringBuilder(_cs).InitialCatalog;

            var setSingle = $"IF DB_ID('{dbName}') IS NOT NULL ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            var setMulti = $"ALTER DATABASE [{dbName}] SET MULTI_USER;";

            // SIN MOVE
            if (string.IsNullOrWhiteSpace(dataDir) || string.IsNullOrWhiteSpace(logDir))
            {
                var restoreSql = $@"
                                    RESTORE DATABASE [{dbName}]
                                    FROM DISK=@p0
                                    WITH STATS=5{(withReplace ? ", REPLACE" : "")};";

                using (var cn = new SqlConnection(_csMaster))
                using (var cmd = new SqlCommand(setSingle + restoreSql, cn) { CommandTimeout = 0 })
                {
                    cmd.Parameters.AddWithValue("@p0", backupFileFullPath);
                    await cn.OpenAsync(ct).ConfigureAwait(false);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                    }
                    finally
                    {
                        await EnsureMultiUserAsync(setMulti, ct).ConfigureAwait(false);
                    }
                }

                return;
            }

            // CON MOVE
            Directory.CreateDirectory(dataDir);
            Directory.CreateDirectory(logDir);

            string logicalData = null, logicalLog = null;

            using (var cn = new SqlConnection(_csMaster))
            using (var cmd = new SqlCommand("RESTORE FILELISTONLY FROM DISK=@p0;", cn) { CommandTimeout = 0 })
            {
                cmd.Parameters.AddWithValue("@p0", backupFileFullPath);
                await cn.OpenAsync(ct).ConfigureAwait(false);

                using (var rd = await cmd.ExecuteReaderAsync(ct).ConfigureAwait(false))
                {
                    while (await rd.ReadAsync(ct).ConfigureAwait(false))
                    {
                        var type = rd["Type"].ToString();
                        var name = rd["LogicalName"].ToString();

                        if (type == "D" && logicalData == null) logicalData = name;
                        if (type == "L" && logicalLog == null) logicalLog = name;
                    }
                }
            }

            logicalData = logicalData ?? dbName;
            logicalLog = logicalLog ?? (dbName + "_log");

            var dataPath = Path.Combine(dataDir, dbName + ".mdf");
            var logPath = Path.Combine(logDir, dbName + "_log.ldf");

            var restoreMoveSql = $@"
                                    RESTORE DATABASE [{dbName}]
                                    FROM DISK=@p0
                                    WITH MOVE @p1 TO @p2,
                                    MOVE @p3 TO @p4,
                                    STATS=5{(withReplace ? ", REPLACE" : "")};";

            using (var cn = new SqlConnection(_csMaster))
            using (var cmd = new SqlCommand(setSingle + restoreMoveSql, cn) { CommandTimeout = 0 })
            {
                cmd.Parameters.AddWithValue("@p0", backupFileFullPath);
                cmd.Parameters.AddWithValue("@p1", logicalData);
                cmd.Parameters.AddWithValue("@p2", dataPath);
                cmd.Parameters.AddWithValue("@p3", logicalLog);
                cmd.Parameters.AddWithValue("@p4", logPath);

                await cn.OpenAsync(ct).ConfigureAwait(false);

                try
                {
                    await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
                }
                finally
                {
                    await EnsureMultiUserAsync(setMulti, ct).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Verifica la integridad de un archivo .bak mediante RESTORE VERIFYONLY.
        /// </summary>
        /// <param name="backupFileFullPath">Ruta completa del archivo .bak.</param>
        /// <param name="ct">Token de cancelación.</param>
        /// <exception cref="ArgumentException">Si la ruta es inválida.</exception>
        /// <exception cref="FileNotFoundException">Si el archivo no existe.</exception>
        public async Task VerifyAsync(string backupFileFullPath, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(backupFileFullPath))
                throw new ArgumentException("Ruta .bak vacía.", nameof(backupFileFullPath));

            if (!global::System.IO.File.Exists(backupFileFullPath))
                throw new FileNotFoundException("No se encontró el .bak", backupFileFullPath);

            using (var cn = new SqlConnection(_csMaster))
            using (var cmd = new SqlCommand("RESTORE VERIFYONLY FROM DISK=@p0;", cn) { CommandTimeout = 0 })
            {
                cmd.Parameters.AddWithValue("@p0", backupFileFullPath);
                await cn.OpenAsync(ct).ConfigureAwait(false);
                await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            }
        }
    }
}
