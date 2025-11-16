using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SL.DAL.Contracts
{
    public interface IBackupRepository
    {
        // Crea un .bak y devuelve la ruta completa
        Task<string> BackupAsync(string backupFolder, string backupName = null, bool copyOnly = true, CancellationToken ct = default);

        // Verifica la integridad del .bak
        Task VerifyAsync(string backupFileFullPath, CancellationToken ct = default);

        // Restaura el .bak. Si viene de otra máquina, pasá dataDir/logDir para hacer WITH MOVE.
        Task RestoreAsync(string backupFileFullPath, bool withReplace = false, string dataDir = null, string logDir = null, CancellationToken ct = default);
    }
}
