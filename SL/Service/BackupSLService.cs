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
    public class BackupSLService
    {
        private readonly IBackupRepository _repo;
        public BackupSLService(IBackupRepository repo) { _repo = repo; }

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
