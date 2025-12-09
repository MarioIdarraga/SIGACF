using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Data.SqlClient;
using SL.DAL.Contracts;
using SL.DAL.Tools;
using SL.Domain;


using FileLogger = SL.DAL.Repositories.File.LoggerRepository;

namespace SL.DAL.Repositories.SqlServer
{
    /// <summary>
    /// Implementación de <see cref="ILogger"/> que almacena los logs en SQL Server.
    /// En caso de fallar la inserción (problemas de conexión, timeouts, etc.),
    /// realiza fallback automático hacia el logger basado en archivos.
    /// </summary>
    public class LoggerRepository : ILogger
    {
        /// <summary>
        /// Colección en memoria utilizada como estructura base (no utilizada en SQL).
        /// </summary>
        List<Log> customer = new List<Log>();

        #region Statements

        /// <summary>
        /// Sentencia SQL utilizada para insertar un log en la tabla [Logs].
        /// </summary>
        private string InsertStatement
        {
            get => "INSERT INTO [Logs] ([Message], [Severity], [PerformedBy]) VALUES (@Message, @Severity, @PerformedBy)";
        }

        /// <summary>
        /// Sentencia SQL utilizada para obtener todos los registros de log.
        /// </summary>
        private string SelectAllStatement
        {
            get => "SELECT * FROM [Logs]";
        }

        #endregion

        /// <summary>
        /// Obtiene todos los registros de log almacenados en la tabla [Logs].
        /// Este método aún no está implementado.
        /// </summary>
        /// <returns>Lista de registros de log.</returns>
        public List<Log> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Guarda un registro de log en la tabla [Logs] de SQL Server.
        /// En caso de error durante la inserción (e.g. base caída, timeout),
        /// se realiza un fallback automático para almacenar el log en archivo.
        /// </summary>
        /// <param name="message">Mensaje del evento.</param>
        /// <param name="severity">Severidad del evento.</param>
        /// <param name="performedBy">Usuario que ejecutó la acción.</param>
        public void Store(string message, EventLevel severity, string performedBy)
        {
            try
            {
                // Log normal en SQL
                SqlHelper.ExecuteNonQuery(
                    InsertStatement,
                    System.Data.CommandType.Text,
                    new SqlParameter[]
                    {
                        new SqlParameter("@Message", message),
                        new SqlParameter("@Severity", severity.ToString()),
                        new SqlParameter("@PerformedBy", performedBy)
                    });
            }
            catch (Exception ex)
            {
                // FALLBACK: si no se puede loguear en SQL (DB caída, timeout, etc.),
                // se escribe el log en archivo usando el logger de File.
                try
                {
                    var fallbackMessage =
                        $"{message} | FALLBACK_SQL_ERROR: {ex.GetType().Name} - {ex.Message}";

                    FileLogger.Current.Store(fallbackMessage, severity, performedBy);
                }
                catch
                {
                    // Si el fallback también falla, NO se relanza para no afectar el flujo principal.
                }
            }
        }
    }
}
