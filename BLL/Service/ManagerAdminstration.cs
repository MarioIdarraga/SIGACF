using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DAL.Tools;

namespace BLL.Service
{
    public class ManagerAdminstration
    {
        private readonly string _connectionString;

        public ManagerAdminstration()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"]?.ConnectionString
                ?? throw new InvalidOperationException("No se pudo encontrar la cadena de conexión 'SqlConnectionString'.");
        }

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
