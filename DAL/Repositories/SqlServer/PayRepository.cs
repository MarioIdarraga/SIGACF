using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL para la entidad Pay.
    /// Gestiona operaciones CRUD y consultas filtradas sobre pagos.
    /// </summary>
    internal class PayRepository : IPayRepository
    {
        #region Statements

        private string InsertStatement =>
            "INSERT INTO [dbo].[Pays] ([IdBooking], [NroDocument], [Date], [MethodPay], [Amount], [State]) " +
            "VALUES (@IdBooking, @NroDocument, @Date, @MethodPay, @Amount, @State)";

        private string UpdateStatement =>
            "UPDATE [dbo].[Pays] SET [IdBooking] = @IdBooking, [NroDocument] = @NroDocument, [Date] = @Date, " +
            "[MethodPay] = @MethodPay, [Amount] = @Amount, [State] = @State WHERE [IdPay] = @IdPay";

        private string DeleteStatement =>
            "DELETE FROM [dbo].[Pays] WHERE [IdPay] = @IdPay";

        private string SelectOneStatement =>
            "SELECT [IdPay], [IdBooking], [NroDocument], [Date], [MethodPay], [Amount], [State] " +
            "FROM [dbo].[Pays] WHERE [IdPay] = @IdPay";

        private string SelectAllStatement =>
            "SELECT [IdPay], [IdBooking], [NroDocument], [Date], [MethodPay], [Amount], [State] FROM [dbo].[Pays]";

        #endregion

        #region Methods

        /// <summary>
        /// Inserta un nuevo pago en la base de datos.
        /// </summary>
        public void Insert(Pay obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(
                    InsertStatement,
                    CommandType.Text,
                    new[]
                    {
                        new SqlParameter("@IdBooking", obj.IdBooking),
                        new SqlParameter("@NroDocument", obj.NroDocument),
                        new SqlParameter("@Date", obj.Date),
                        new SqlParameter("@MethodPay", obj.MethodPay),
                        new SqlParameter("@Amount", obj.Amount),
                        new SqlParameter("@State", obj.State)
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el pago.", ex);
            }
        }

        /// <summary>
        /// Actualiza un pago existente.
        /// </summary>
        public void Update(int id, Pay obj)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(
                    UpdateStatement,
                    CommandType.Text,
                    new[]
                    {
                        new SqlParameter("@IdPay", id),
                        new SqlParameter("@IdBooking", obj.IdBooking),
                        new SqlParameter("@NroDocument", obj.NroDocument),
                        new SqlParameter("@Date", obj.Date),
                        new SqlParameter("@MethodPay", obj.MethodPay),
                        new SqlParameter("@Amount", obj.Amount),
                        new SqlParameter("@State", obj.State)
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el pago.", ex);
            }
        }

        /// <summary>
        /// Elimina un pago por Id.
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(
                    DeleteStatement,
                    CommandType.Text,
                    new[] { new SqlParameter("@IdPay", id) });
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el pago.", ex);
            }
        }

        /// <summary>
        /// Obtiene un pago por Id.
        /// </summary>
        public Pay GetOne(int id)
        {
            try
            {
                using (var reader = SqlHelper.ExecuteReader(
                    SelectOneStatement,
                    CommandType.Text,
                    new[] { new SqlParameter("@IdPay", id) }))
                {
                    if (reader.Read())
                    {
                        return MapPay(reader);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el pago.", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos sin filtros.
        /// </summary>
        public IEnumerable<Pay> GetAll()
        {
            var list = new List<Pay>();

            try
            {
                using (var reader = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    while (reader.Read())
                        list.Add(MapPay(reader));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pagos.", ex);
            }
        }

        /// <summary>
        /// Obtiene pagos filtrados por rango de fechas.
        /// </summary>
        public IEnumerable<Pay> GetAll(DateTime? since, DateTime? until)
        {
            var list = new List<Pay>();

            try
            {
                string query = SelectAllStatement + " WHERE 1=1 ";
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

                using (var reader = SqlHelper.ExecuteReader(query, CommandType.Text, parameters.ToArray()))
                {
                    while (reader.Read())
                        list.Add(MapPay(reader));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pagos filtrados.", ex);
            }
        }

        /// <summary>
        /// Mapea un SqlDataReader a la entidad Pay.
        /// Maneja conversiones seguras.
        /// </summary>
        private Pay MapPay(SqlDataReader reader)
        {
            return new Pay
            {
                IdPay = reader.GetInt32(0),
                IdBooking = reader.GetGuid(1),
                NroDocument = reader.GetInt32(2),
                Date = reader.GetDateTime(3),
                MethodPay = reader.GetInt32(4),
                Amount = Convert.ToDecimal(reader[5]),

                State = reader.GetInt32(6)
            };
        }

        #endregion
    }
}
