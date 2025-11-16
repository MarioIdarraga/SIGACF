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
    internal class BackupRepository : IBackupRepository
    {
        private readonly string _cs;       // Cadena a tu BD (p.ej. Arena)
        private readonly string _csMaster; // Cadena a master (derivada)

        public BackupRepository(string cs)
        {
            if (string.IsNullOrWhiteSpace(cs))
                throw new ArgumentNullException(nameof(cs));

            _cs = cs;
            var b = new SqlConnectionStringBuilder(cs) { InitialCatalog = "master" };
            _csMaster = b.ConnectionString;
        }

        public async Task<string> BackupAsync(string backupFolder, string backupName = null, bool copyOnly = true, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(backupFolder)) throw new ArgumentException("Carpeta de backup vacía.", nameof(backupFolder));

            Directory.CreateDirectory(backupFolder);

            var dbName = new SqlConnectionStringBuilder(_cs).InitialCatalog;
            var ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = $"{(string.IsNullOrWhiteSpace(backupName) ? dbName : backupName)}_{ts}.bak";
            var fullPath = Path.Combine(backupFolder, fileName);

            // COPY_ONLY evita interferir con backups diferenciales
            var sql = $"BACKUP DATABASE [{dbName}] TO DISK=@p0 WITH {(copyOnly ? "COPY_ONLY," : "")} COMPRESSION, INIT, STATS=5;";

            using (var cn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, cn) { CommandTimeout = 0 })
            {
                cmd.Parameters.AddWithValue("@p0", fullPath);
                await cn.OpenAsync(ct).ConfigureAwait(false);
                await cmd.ExecuteNonQueryAsync(ct).ConfigureAwait(false);
            }

            return fullPath;
        }

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
                // última chance: si esto falla, no re-lanzamos para no tapar el error original del RESTORE
            }
        }

        public async Task RestoreAsync(string backupFileFullPath, bool withReplace = false, string dataDir = null, string logDir = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(backupFileFullPath))
                throw new ArgumentException("Ruta .bak vacía.", nameof(backupFileFullPath));

            if (!global::System.IO.File.Exists(backupFileFullPath))
                throw new FileNotFoundException("No se encontró el .bak", backupFileFullPath);

            var dbName = new SqlConnectionStringBuilder(_cs).InitialCatalog;
            var setSingle = $"IF DB_ID('{dbName}') IS NOT NULL ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            var setMulti = $"ALTER DATABASE [{dbName}] SET MULTI_USER;";

            // SIN MOVE (mismas rutas que el origen)
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

            // CON MOVE (rutas distintas)
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
                        var type = rd["Type"].ToString();           // 'D' data, 'L' log
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
