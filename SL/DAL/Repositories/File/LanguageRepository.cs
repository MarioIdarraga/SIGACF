using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using SL.Domain.BusinessException;

namespace SL.DAL.Repositories.File
{
    public sealed class LanguageRepository
    {
        #region Singleton
        private static readonly LanguageRepository _instance = new LanguageRepository();
        public static LanguageRepository Current => _instance;

        public LanguageRepository()
        {
        }
        #endregion

        private readonly string folderLanguage = ConfigurationManager.AppSettings["FolderLanguage"];
        private readonly string filePathLanguage = ConfigurationManager.AppSettings["FilePathLanguage"];

        public string Traductor(string key)
        {
            string lang = Thread.CurrentThread.CurrentUICulture.Name;
            string fileName = $"{filePathLanguage}{lang}"; // sin .txt

            // Carpeta de ejecución (bin\Debug o bin\Release)
            string runtimeFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderLanguage);
            string runtimePath = Path.Combine(runtimeFolder, fileName);

            if (!System.IO.File.Exists(runtimePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(runtimePath));
                System.IO.File.WriteAllText(runtimePath, "WELCOME=Bienvenido\nACCESS_GRANTED=Acceso concedido\n");
            }

            foreach (var line in System.IO.File.ReadLines(runtimePath))
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && parts[0] == key)
                {
#if DEBUG
                    // Solo en Debug: sincronizar con carpeta del proyecto
                    string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\", folderLanguage));
                    string projectPath = Path.Combine(projectFolder, fileName);

                    if (!System.IO.File.Exists(projectPath))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(projectPath));
                        System.IO.File.WriteAllText(projectPath, "WELCOME=Bienvenido\nACCESS_GRANTED=Acceso concedido\n");
                    }

                    var projectLines = System.IO.File.ReadAllLines(projectPath);
                    if (!projectLines.Any(l => l.StartsWith(key + "=")))
                    {
                        System.IO.File.AppendAllText(projectPath, $"{key}={parts[1]}\n");
                    }
#endif
                    return parts[1];
                }
            }

            throw new NoSeEncontroLaPalabraException();
        }

        public List<string> GetAllLanguages()
        {
            string runtimeFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderLanguage);

            if (!Directory.Exists(runtimeFolder))
                return new List<string>();

            return Directory
                .GetFiles(runtimeFolder)
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
        }
    }
}
