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
    internal class SqlLoggerRepository : ILogger
    {

        List<Log> customer = new List<Log>();

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [Log].[] () VALUES ()";
        }

        private string SelectAllStatement
        {
            get => "SELECT * FROM [Log]";
        }

        public List<Log> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Store(string message, EventLevel severity)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text,
                        new SqlParameter[]  {   new SqlParameter("@Message", message),
                                                new SqlParameter("@Severity", severity) });
            }
            catch (Exception ex)
            {

                //ex.Handle(this);
            }
        }

        #endregion
    }
}
