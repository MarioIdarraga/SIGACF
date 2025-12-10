using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DAL.Tools;

namespace BLL.Service
{
    /// <summary>
    /// Servicio encargado de administrar operaciones globales de la base de datos,
    /// como la creación de backups mediante comandos SQL directos.
    /// </summary>
    public class ManagerAdminstration
    {
        /// <summary>
        /// Cadena de conexión utilizada para ejecutar las operaciones administrativas.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Inicializa la instancia obteniendo la cadena de conexión configurada en App.config.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si no existe la cadena de conexión 'SqlConnectionString'.
        /// </exception>
        public ManagerAdminstration()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"]?.ConnectionString
                ?? throw new InvalidOperationException("No se pudo encontrar la cadena de conexión 'SqlConnectionString'.");
        }

        /// <summary>
        /// Crea un archivo de respaldo (.bak) de la base de datos definida en la cadena de conexión.
        /// </summary>
        /// <param name="filePath">Ruta completa donde se almacenará el backup.</param>
        /// <exception cref="ArgumentException">
        /// Si la ruta del archivo es nula o vacía.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Si la cadena de conexión no define una base de datos (InitialCatalog vacío).
        /// </exception>
        /// <exception cref="SqlException">
        /// Si ocurre un error durante la ejecución del comando SQL de backup.
        /// </exception>
        public void CreateBackup(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("La ruta del archivo de backup es obligatoria.", nameof(filePath));
            }

            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var builder = new SqlConnectionStringBuilder(_connectionString);
            if (string.IsNullOrWhiteSpace(builder.InitialCatalog))
            {
                throw new InvalidOperationException("La cadena de conexión no define la base de datos a respaldar.");
            }

            var backupCommand = $"BACKUP DATABASE [{builder.InitialCatalog}] TO DISK = @path WITH FORMAT, INIT";
            var pathParameter = new SqlParameter("@path", SqlDbType.NVarChar)
            {
                Value = filePath
            };

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(backupCommand, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(pathParameter);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
