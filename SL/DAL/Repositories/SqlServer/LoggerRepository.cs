using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.DAL.Contracts;
using SL.DAL.Tools;
using SL.Domain;
using SL.Service.Extension;
using System.Data.SqlClient;

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

        public List<Log> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Store(string message, EventLevel severity, string performedBy)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text,
                    new SqlParameter[]
                    {
                new SqlParameter("@Message", message),
                new SqlParameter("@Severity", severity.ToString()),
                new SqlParameter("@PerformedBy", performedBy)
                    });
            }
            catch (Exception ex)
            {
                // Manejo interno
            }
        }

        #endregion
    }
}
