using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SL.DAL.Contracts;

namespace SL.DAL.Repositories.File
{
    internal class BackupRepository : IBackupRepository
    {
        public Task<string> BackupAsync(string backupFolder, string backupName = null, bool copyOnly = true, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task RestoreAsync(string backupFileFullPath, bool withReplace = false, string dataDir = null, string logDir = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task VerifyAsync(string backupFileFullPath, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
