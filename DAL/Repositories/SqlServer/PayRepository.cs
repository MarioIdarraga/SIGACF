using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    internal class PayRepository : IPayRepository
    {


        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Pay] ([IdPay], [IdBooking], [NroDocument], [Date], [MethodPay], [Amount], [State]) " +
                   "VALUES (@IdPay, @IdBooking, @NroDocument, @Date, @MethodPay, @Amount, @State)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Pay] SET [IdBooking] = @IdBooking, [NroDocument] = @NroDocument, [Date] = @Date, " +
                   "[MethodPay] = @MethodPay, [Amount] = @Amount, [State] = @State WHERE [IdPay] = @IdPay";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Pay] WHERE [IdPay] = @IdPay";
        }

        private string SelectOneStatement
        {
            get => "SELECT [IdPay], [IdBooking], [NroDocument], [Date], [MethodPay], [Amount], [State] " +
                   "FROM [dbo].[Pay] WHERE [IdPay] = @IdPay";
        }

        private string SelectAllStatement
        {
            get => "SELECT [IdPay], [IdBooking], [NroDocument], [Date], [MethodPay], [Amount], [State] FROM [dbo].[Pay]";
        }
        #endregion


        #region Methods

        public void Insert(Pay obj)
        {
            SqlHelper.ExecuteNonQuery(
                InsertStatement,
                CommandType.Text,
                new SqlParameter[]
                {
            new SqlParameter("@IdPay", obj.IdPay),
            new SqlParameter("@IdBooking", obj.IdBooking),
            new SqlParameter("@NroDocument", obj.NroDocument),
            new SqlParameter("@Date", obj.Date),
            new SqlParameter("@MethodPay", obj.MethodPay),
            new SqlParameter("@Amount", obj.Amount),
            new SqlParameter("@State", obj.State)
                }
            );
        }

        public void Update(Guid id, Pay obj)
        {
            SqlHelper.ExecuteNonQuery(
                UpdateStatement,
                CommandType.Text,
                new SqlParameter[]
                {
            new SqlParameter("@IdPay", id),
            new SqlParameter("@IdBooking", obj.IdBooking),
            new SqlParameter("@NroDocument", obj.NroDocument),
            new SqlParameter("@Date", obj.Date),
            new SqlParameter("@MethodPay", obj.MethodPay),
            new SqlParameter("@Amount", obj.Amount),
            new SqlParameter("@State", obj.State)
                }
            );
        }

        public void Delete(Guid id)
        {
            SqlHelper.ExecuteNonQuery(
                DeleteStatement,
                CommandType.Text,
                new SqlParameter[]
                {
            new SqlParameter("@IdPay", id)
                }
            );
        }

        public Pay GetOne(Guid id)
        {
            using (var reader = SqlHelper.ExecuteReader(
                SelectOneStatement,
                CommandType.Text,
                new SqlParameter[]
                {
            new SqlParameter("@IdPay", id)
                }))
            {
                if (reader.Read())
                {
                    return new Pay
                    {
                        IdPay = reader.GetGuid(0),
                        IdBooking = reader.GetGuid(1),
                        NroDocument = reader.GetInt32(2),
                        Date = reader.GetDateTime(3),
                        MethodPay = reader.GetInt32(4),
                        Amount = Convert.ToSingle(reader["Amount"]),
                        State = reader.GetInt32(6)
                    };
                }
            }
            return null;
        }

        public IEnumerable<Pay> GetAll()
        {
            var list = new List<Pay>();

            using (var reader = SqlHelper.ExecuteReader(
                SelectAllStatement,
                CommandType.Text))
            {
                while (reader.Read())
                {
                    list.Add(new Pay
                    {
                        IdPay = reader.GetGuid(0),
                        IdBooking = reader.GetGuid(1),
                        NroDocument = reader.GetInt32(2),
                        Date = reader.GetDateTime(3),
                        MethodPay = reader.GetInt32(4),
                        Amount = Convert.ToSingle(reader["Amount"]),
                        State = reader.GetInt32(6)
                    });
                }
            }

            return list;
        }

        public IEnumerable<Pay> GetAll(DateTime? since, DateTime? until)
        {
            var list = new List<Pay>();

            var query = SelectAllStatement + " WHERE 1=1";
            var parameters = new List<SqlParameter>();

            if (since.HasValue)
            {
                query += " AND [Date] >= @Since";
                parameters.Add(new SqlParameter("@Since", since.Value));
            }

            if (until.HasValue)
            {
                query += " AND [Date] <= @Until";
                parameters.Add(new SqlParameter("@Until", until.Value));
            }

            using (var reader = SqlHelper.ExecuteReader(
                query,
                CommandType.Text,
                parameters.ToArray()))
            {
                while (reader.Read())
                {
                    list.Add(new Pay
                    {
                        IdPay = reader.GetGuid(0),
                        IdBooking = reader.GetGuid(1),
                        NroDocument = reader.GetInt32(2),
                        Date = reader.GetDateTime(3),
                        MethodPay = reader.GetInt32(4),
                        Amount = Convert.ToSingle(reader["Amount"]),
                        State = reader.GetInt32(6)
                    });
                }
            }

            return list;
        }

        #endregion

    }
}
