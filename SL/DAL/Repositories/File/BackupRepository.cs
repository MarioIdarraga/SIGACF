using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using SL.DAL.Contracts;

namespace SL.DAL.Repositories.File
{
    /// <summary>
    /// Implementación del repositorio de backup para entornos basados en archivos.
    /// Genera un archivo ZIP con los datos del backend File (logs, idiomas, permisos, etc.).
    /// </summary>
    internal class BackupRepository : IBackupRepository
    {
        /// <summary>
        /// Crea un archivo ZIP con las carpetas configuradas del backend File.
        /// </summary>
        public Task<string> BackupAsync(string backupFolder, string backupName = null, bool copyOnly = true, CancellationToken ct = default)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string[] foldersToInclude =
            {
                "Logs",
                "Language",
                "Permissions"
            };

            if (string.IsNullOrWhiteSpace(backupFolder))
                throw new ArgumentException("Carpeta de backup vacía.", nameof(backupFolder));

            Directory.CreateDirectory(backupFolder);

            string ts = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"{(string.IsNullOrWhiteSpace(backupName) ? "FileBackup" : backupName)}_{ts}.zip";
            string fullPath = Path.Combine(backupFolder, fileName);

            using (var zip = ZipFile.Open(fullPath, ZipArchiveMode.Create))
            {
                foreach (var folder in foldersToInclude)
                {
                    string path = Path.Combine(baseDir, folder);

                    if (!Directory.Exists(path))
                        continue;

                    foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                    {
                        string entryName = GetRelativePath(baseDir, file);
                        zip.CreateEntryFromFile(file, entryName);
                    }
                }
            }

            return Task.FromResult(fullPath);
        }

        /// <summary>
        /// Restaura un backup ZIP sobrescribiendo los archivos del backend File.
        /// </summary>
        public Task RestoreAsync(string backupFileFullPath, bool withReplace = false, string dataDir = null, string logDir = null, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(backupFileFullPath))
                throw new ArgumentException("Ruta de backup vacía.", nameof(backupFileFullPath));

            if (!System.IO.File.Exists(backupFileFullPath))
                throw new FileNotFoundException("No se encontró el archivo ZIP.", backupFileFullPath);

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            using (var zip = ZipFile.OpenRead(backupFileFullPath))
            {
                foreach (var entry in zip.Entries)
                {
                    string targetPath = Path.Combine(baseDir, entry.FullName);
                    string targetDir = Path.GetDirectoryName(targetPath);

                    Directory.CreateDirectory(targetDir);

                    // Sobrescribe el archivo manualmente
                    entry.ExtractToFile(targetPath, overwrite: true);
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Verifica que el ZIP sea válido y accesible.
        /// </summary>
        public Task VerifyAsync(string backupFileFullPath, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(backupFileFullPath))
                throw new ArgumentException("Ruta de backup vacía.", nameof(backupFileFullPath));

            if (!System.IO.File.Exists(backupFileFullPath))
                throw new FileNotFoundException("No se encontró el archivo ZIP.", backupFileFullPath);

            using (var zip = ZipFile.OpenRead(backupFileFullPath))
            {
                if (zip.Entries.Count == 0)
                    throw new InvalidDataException("El archivo ZIP está vacío o dañado.");
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Implementación manual de GetRelativePath 
        /// </summary>
        private string GetRelativePath(string basePath, string fullPath)
        {
            Uri baseUri = new Uri(AppendDirectorySeparator(basePath));
            Uri fullUri = new Uri(fullPath);
            return Uri.UnescapeDataString(baseUri.MakeRelativeUri(fullUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        private string AppendDirectorySeparator(string path)
        {
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                return path + Path.DirectorySeparatorChar;
            return path;
        }
    }
}
