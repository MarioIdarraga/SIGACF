using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SL.DAL.Contracts;
using SL.Helpers;

namespace SL.Service
{
    /// <summary>
    /// Servicio de capa lógica (SL) encargado de gestionar operaciones de backup,
    /// verificación y restauración utilizando <see cref="IBackupRepository"/>.
    /// Incluye registro de auditoría mediante <see cref="LoggerService"/>.
    /// </summary>
    public class BackupSLService
    {
        private readonly IBackupRepository _repo;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de backup.
        /// </summary>
        /// <param name="repo">Repositorio concreto para realizar backup/restore.</param>
        public BackupSLService(IBackupRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Crea un archivo de backup (.bak) de la base de datos.
        /// Registra auditoría antes y después de la operación.
        /// </summary>
        /// <param name="carpeta">Directorio donde se almacenará el backup.</param>
        /// <param name="nombre">Nombre opcional del archivo de backup.</param>
        /// <param name="ct">Token de cancelación opcional.</param>
        /// <returns>Ruta completa del archivo generado.</returns>
        /// <exception cref="Exception">Propaga cualquier error ocurrido en el repositorio.</exception>
        public async Task<string> CrearBackupAsync(string carpeta, string nombre = null, CancellationToken ct = default)
        {
            LoggerService.Log("Inicio de backup.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            try
            {
                var path = await _repo.BackupAsync(carpeta, nombre, copyOnly: true, ct);
                LoggerService.Log($"Backup finalizado. Archivo: {path}", EventLevel.Informational, Session.CurrentUser?.LoginName);
                return path;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error en backup: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        /// <summary>
        /// Verifica la integridad de un archivo de backup existente (.bak).
        /// </summary>
        /// <param name="file">Ruta completa del archivo .bak.</param>
        /// <param name="ct">Token de cancelación opcional.</param>
        /// <exception cref="Exception">Propaga cualquier error ocurrido en la verificación.</exception>
        public async Task VerificarBackupAsync(string file, CancellationToken ct = default)
        {
            LoggerService.Log($"Inicio de verificación de backup: {file}", EventLevel.Informational, Session.CurrentUser?.LoginName);
            try
            {
                await _repo.VerifyAsync(file, ct);
                LoggerService.Log("Verificación OK.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error verificando backup: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        /// <summary>
        /// Restaura una base de datos desde un archivo de backup (.bak).
        /// Permite reemplazo de BD y especificar rutas personalizadas de MDF y LDF.
        /// </summary>
        /// <param name="file">Ruta del archivo .bak.</param>
        /// <param name="replace">Indica si debe reemplazar la BD existente (WITH REPLACE).</param>
        /// <param name="dataDir">Ruta destino para el archivo .mdf (opcional).</param>
        /// <param name="logDir">Ruta destino para el archivo .ldf (opcional).</param>
        /// <param name="ct">Token de cancelación opcional.</param>
        /// <exception cref="Exception">Propaga cualquier error ocurrido durante el restore.</exception>
        public async Task RestaurarAsync(string file, bool replace = false, string dataDir = null, string logDir = null, CancellationToken ct = default)
        {
            LoggerService.Log($"Inicio de restore. Archivo: {file}  Replace={replace}", EventLevel.Informational, Session.CurrentUser?.LoginName);
            try
            {
                await _repo.RestoreAsync(file, replace, dataDir, logDir, ct);
                LoggerService.Log("Restore completado.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error en restore: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }
    }
}
