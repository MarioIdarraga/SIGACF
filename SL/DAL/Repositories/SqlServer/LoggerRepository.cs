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
    public class LoggerRepository : ILogger
    {
        List<Log> customer = new List<Log>();

        #region Statements

        private string InsertStatement
        {
            get => "INSERT INTO [Logs] ([Message], [Severity], [PerformedBy]) VALUES (@Message, @Severity, @PerformedBy)";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [Logs]";
        }

        #endregion

        public List<Log> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Guarda el log en la tabla [Logs]. 
        /// Si falla por problema de conexión / SQL, hace fallback a archivo.
        /// </summary>
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

                }
            }
        }
    }
}

