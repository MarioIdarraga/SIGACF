using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using SL.DAL.Contracts;
using SL.DAL.Tools;
using SL.Domain;

namespace SL.DAL.Repositories.SqlServer
{
    public class LoggerRepository : ILogger
    {
        #region Statements

        private string InsertStatement =>
            "INSERT INTO [dbo].[Logs] ([Message], [Severity], [CreatedAt]) VALUES (@Message, @Severity, GETDATE())";

        private string SelectAllStatement =>
            "SELECT [IdLog], [Message], [Severity], [CreatedAt] FROM [dbo].[Logs]";

        #endregion

        #region Métodos públicos

        public void Store(string message, EventLevel severity)
        {
            try
            {
                InsertLog(message, severity);
            }
            catch (Exception ex)
            {
                // ex.Handle(this);
            }
        }

        internal void StoreCritical(string message, EventLevel severity)
        {
            try
            {
                InsertLog(message, severity);
            }
            catch (Exception ex)
            {
                // ex.Handle(this);
            }
        }

        internal void StoreWarning(string message, EventLevel severity)
        {
            try
            {
                InsertLog(message, severity);
            }
            catch (Exception ex)
            {
                // ex.Handle(this);
            }
        }

        public List<Log> GetAll()
        {
            var result = new List<Log>();

            try
            {
                using (var reader = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        result.Add(new Log
                        {
                            Message = reader["Message"].ToString(),
                            severity = Enum.TryParse(reader["Severity"].ToString(), out Log.Severity sev)
                                ? sev
                                : Log.Severity.Warning
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // ex.Handle(this);
            }

            return result;
        }

        #endregion

        #region Métodos privados

        private void InsertLog(string message, EventLevel severity)
        {
            SqlHelper.ExecuteNonQuery(
                InsertStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@Message", message),
                    new SqlParameter("@Severity", severity.ToString())
                });
        }

        #endregion
    }
}


