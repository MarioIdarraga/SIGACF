using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using SL.Domain.BusinessException;

namespace SL.DAL.Repositories.File
{
    /// <summary>
    /// Repositorio de traducciones basado en archivos.
    /// Lee pares clave=valor desde archivos de idioma almacenados en la carpeta configurada.
    /// Cada idioma se identifica por su código cultural (ej.: es-AR, en-US).
    /// </summary>
    public sealed class LanguageRepository
    {
        #region Singleton

        /// <summary>
        /// Instancia única del repositorio (patrón Singleton).
        /// </summary>
        private static readonly LanguageRepository _instance = new LanguageRepository();

        /// <summary>
        /// Acceso global a la instancia del repositorio.
        /// </summary>
        public static LanguageRepository Current => _instance;

        /// <summary>
        /// Constructor privado para evitar instanciación externa.
        /// </summary>
        public LanguageRepository()
        {
        }

        #endregion

        /// <summary>
        /// Carpeta donde se almacenan los archivos de idioma.
        /// Configurada en AppSettings["FolderLanguage"].
        /// </summary>
        private readonly string folderLanguage = ConfigurationManager.AppSettings["FolderLanguage"];

        /// <summary>
        /// Prefijo del archivo de idioma.
        /// Configurada en AppSettings["FilePathLanguage"].
        /// </summary>
        private readonly string filePathLanguage = ConfigurationManager.AppSettings["FilePathLanguage"];

        /// <summary>
        /// Traduce una clave según el idioma actual configurado en el sistema.
        /// Si el archivo no existe, lo crea con claves básicas por defecto.
        /// Mantiene sincronización automática con el archivo del proyecto en Debug.
        /// </summary>
        /// <param name="key">Clave a buscar en el archivo de idioma.</param>
        /// <returns>Valor traducido correspondiente a la clave.</returns>
        /// <exception cref="NoSeEncontroLaPalabraException">
        /// Se lanza cuando la clave no existe en el archivo de idioma.
        /// </exception>
        public string Traductor(string key)
        {
            string lang = Thread.CurrentThread.CurrentUICulture.Name;
            string fileName = $"{filePathLanguage}{lang}"; // sin .txt

            // Carpeta en tiempo de ejecución (bin\Debug / bin\Release)
            string runtimeFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderLanguage);
            string runtimePath = Path.Combine(runtimeFolder, fileName);

            // Crear archivo con claves mínimas si no existe
            if (!System.IO.File.Exists(runtimePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(runtimePath));
                System.IO.File.WriteAllText(runtimePath, "WELCOME=Bienvenido\nACCESS_GRANTED=Acceso concedido\n");
            }

            // Buscar clave en el archivo de idioma
            foreach (var line in System.IO.File.ReadLines(runtimePath))
            {
                var parts = line.Split('=');
                if (parts.Length == 2 && parts[0] == key)
                {
#if DEBUG
                    // Solo en Debug: sincroniza archivo del proyecto para mantener traducciones actualizadas
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

        /// <summary>
        /// Obtiene la lista de idiomas disponibles detectando los archivos presentes
        /// en la carpeta de idioma configurada.
        /// </summary>
        /// <returns>
        /// Lista de nombres de archivo sin extensión, representando código de idioma.
        /// </returns>
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