using System;
using System.Data;
using System.Data.SqlClient;
using SL.DAL.Contracts;
using SL.DAL.Tools;
using SL.Factory;

namespace SL.DAL.Repositories.SqlServer
{
    public class VerificadorVerticalRepository : IVerificadorVerticalRepository
    {
        #region Statements
        private string SelectStatement
        {
            get => "SELECT Checksum FROM TableChecksums WHERE TableName = @TableName";
        }

        private string UpsertStatement
        {
            get => @"MERGE TableChecksums AS target
                    USING (SELECT @TableName AS TableName, @Checksum AS Checksum) AS source
                    ON target.TableName = source.TableName
                    WHEN MATCHED THEN
                        UPDATE SET Checksum = source.Checksum
                    WHEN NOT MATCHED THEN
                        INSERT (TableName, Checksum) VALUES (source.TableName, source.Checksum);";
        }
        #endregion

        #region Methods
        public string GetDVV(string tabla)
        {
            using (var reader = SqlHelper.ExecuteReader(
                SelectStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@TableName", tabla)
                }))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return string.Empty;
        }

        public void SetDVV(string tabla, string dvv)
        {
            SqlHelper.ExecuteNonQuery(
                UpsertStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@TableName", tabla),
                    new SqlParameter("@Checksum", dvv)
                });
        }
        #endregion
    }
}
