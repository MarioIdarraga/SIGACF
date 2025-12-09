using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SL.DAL.Contracts
{
    /// <summary>
    /// Define las operaciones necesarias para realizar copias de seguridad,
    /// verificar la integridad de archivos .bak y restaurar bases de datos.
    /// Implementado por la capa DAL en repositorios específicos.
    /// </summary>
    public interface IBackupRepository
    {
        /// <summary>
        /// Genera un archivo de respaldo (.bak) de la base de datos y devuelve
        /// la ruta completa donde se almacenó el archivo.
        /// </summary>
        /// <param name="backupFolder">Carpeta donde se guardará el archivo .bak.</param>
        /// <param name="backupName">Nombre opcional del archivo de respaldo.</param>
        /// <param name="copyOnly">
        /// Indica si el backup debe generarse en modo COPY_ONLY (no afecta la cadena de backups).
        /// </param>
        /// <param name="ct">Token de cancelación opcional.</param>
        /// <returns>Ruta completa del archivo .bak generado.</returns>
        Task<string> BackupAsync(string backupFolder, string backupName = null, bool copyOnly = true, CancellationToken ct = default);

        /// <summary>
        /// Verifica la integridad de un archivo .bak generado.
        /// Utilizado para detectar corrupción en el respaldo antes de restaurar.
        /// </summary>
        /// <param name="backupFileFullPath">Ruta completa del archivo .bak.</param>
        /// <param name="ct">Token de cancelación opcional.</param>
        Task VerifyAsync(string backupFileFullPath, CancellationToken ct = default);

        /// <summary>
        /// Restaura una base de datos a partir de un archivo .bak.
        /// Si el respaldo proviene de otra máquina, puede especificarse las
        /// rutas físicas para los archivos de datos (.mdf) y logs (.ldf) mediante WITH MOVE.
        /// </summary>
        /// <param name="backupFileFullPath">Ruta completa del archivo .bak a restaurar.</param>
        /// <param name="withReplace">Indica si se debe sobrescribir una base existente (WITH REPLACE).</param>
        /// <param name="dataDir">Ruta opcional para el archivo de datos (.mdf) al restaurar.</param>
        /// <param name="logDir">Ruta opcional para el archivo de logs (.ldf) al restaurar.</param>
        /// <returns>Tarea asíncrona que representa la operación de restauración.</returns>
        Task RestoreAsync(string backupFileFullPath, bool withReplace = false, string dataDir = null, string logDir = null, CancellationToken ct = default);
    }
}
