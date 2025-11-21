using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Tracing;
using System.IO;
using SL.DAL.Contracts;
using SL.Domain;

namespace SL.DAL.Repositories.File
{
    /// <summary>
    /// Repositorio de logs que escribe en archivos de texto.
    /// Utiliza configuración de appSettings: LogFile (carpeta) y LogFileName (prefijo).
    /// </summary>
    public class LoggerRepository : ILogger
    {
        private static readonly LoggerRepository _instance = new LoggerRepository();
        public static LoggerRepository Current => _instance;

        private readonly string _logFileName;
        private readonly string _logDirectory;

        /// <summary>
        /// Constructor. Inicializa el nombre de archivo y la carpeta de logs
        /// a partir de appSettings o valores por defecto.
        /// </summary>
        public LoggerRepository()
        {
            _logFileName = ConfigurationManager.AppSettings["LogFileName"];
            _logDirectory = ConfigurationManager.AppSettings["LogFile"];

            if (string.IsNullOrWhiteSpace(_logFileName))
                _logFileName = "SIGACF_Log";

            if (string.IsNullOrWhiteSpace(_logDirectory))
                _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        }

        /// <summary>
        /// Obtiene la ruta completa del archivo de log para la fecha actual.
        /// </summary>
        /// <returns>Ruta completa del archivo de log.</returns>
        private string GetLogFullPath()
        {
            Directory.CreateDirectory(_logDirectory); // asegura que exista la carpeta

            var fileName = $"{_logFileName}-{DateTime.Now:yyyyMMdd}.log";
            return Path.Combine(_logDirectory, fileName);
        }

        /// <summary>
        /// Almacena un mensaje de log en archivo, incluyendo nivel y usuario.
        /// Nunca lanza excepción hacia afuera.
        /// </summary>
        public void Store(string message, EventLevel severity, string performedBy)
        {
            try
            {
                var fullPath = GetLogFullPath();

                using (var sw = new StreamWriter(fullPath, true))
                {
                    string formattedMessage =
                        $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} | " +
                        $"LEVEL: {severity} | USER: {performedBy} | MSG: {message}";

                    sw.WriteLine(formattedMessage);
                }
            }
            catch
            {
                // El logger nunca debe romper la app
            }
        }

        /// <summary>
        /// Almacena un mensaje de log en archivo usando "System" como usuario.
        /// </summary>
        public void Store(string message, EventLevel severity)
        {
            Store(message, severity, "System");
        }

        /// <summary>
        /// No implementado: lectura de logs desde archivo.
        /// </summary>
        public List<Log> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
