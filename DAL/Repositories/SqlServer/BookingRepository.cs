using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    internal class BookingRepository : IGenericRepository<Booking>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Bookings] (IdCustomer, NroDocument, RegistrationDate, RegistrationBooking, StartTime, EndTime, Field, Promotion, State ) VALUES (@IdCustomer, @NroDocument, @RegistrationDate, @RegistrationBooking, @StartTime, @EndTime, @Field, @Promotion, @State)";
        }
        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Bookings] SET IdCustomer = @IdCustomer, NroDocument = @NroDocument, RegistrationDate = @RegistrationDate, RegistrationBooking = @RegistrationBooking, StartTime = @StartTime, EndTime = @EndTime, Field = @Field, Promotion = @Promotion, State = @State WHERE IdBooking = @IdBooking";
        }


        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Bookings] WHERE IdBooking = @IdBooking";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdBooking, IdCustomer, NroDocument, RegistrationDate, RegistrationBooking, StartTime, EndTime, Field, Promotion, State  FROM [dbo].[Bookings] WHERE IdBooking = @IdBooking";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdBooking, IdCustomer, NroDocument, RegistrationDate, RegistrationBooking, StartTime, EndTime, Field, Promotion, State  FROM [dbo].[Bookings]";
        }
        #endregion


        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAll(int? nroDocument = null, DateTime? registrationBooking = null, DateTime? registrationDate = null)
        {
            var bookings = new List<Booking>();
            string query = SelectAllStatement + " WHERE 1=1"; // Se usa WHERE 1=1 para facilitar concatenación de condiciones
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (nroDocument.HasValue)
            {
                query += " AND NroDocument = @NroDocument";
                parameters.Add(new SqlParameter("@NroDocument", nroDocument.Value));
            }

            if (registrationBooking.HasValue)
            {
                query += " AND CAST(RegistrationBooking AS DATE) = @RegistrationBooking";
                parameters.Add(new SqlParameter("@RegistrationBooking", registrationBooking.Value.Date));
            }

            if (registrationDate.HasValue)
            {
                query += " AND CAST(RegistrationDate AS DATE) = @RegistrationDate";
                parameters.Add(new SqlParameter("@RegistrationDate", registrationDate.Value.Date));
            }

            using (var reader = SqlHelper.ExecuteReader(query, System.Data.CommandType.Text, parameters.ToArray()))
            {
                while (reader.Read())
                {
                    bookings.Add(new Booking
                    {
                        IdBooking = reader.GetGuid(0),
                        IdCustomer = reader.GetGuid(1),
                        NroDocument = reader.GetString(2),
                        RegistrationDate = reader.GetDateTime(3),  // Corrección del orden
                        RegistrationBooking = reader.GetDateTime(4), // Corrección del orden
                        StartTime = reader.GetTimeSpan(5), // Corrección de GetDateTime() a GetTimeSpan()
                        EndTime = reader.GetTimeSpan(6), // Corrección de GetDateTime() a GetTimeSpan()
                        Field = reader.GetGuid(7),
                        Promotion = reader.GetGuid(8),
                        State = reader.GetInt32(9)
                    });
                }
            }
            return bookings;
        }

        public IEnumerable<Booking> GetAll(int? nroDocument, DateTime registrationBooking, DateTime registrationDate)
        {
            throw new NotImplementedException();
        }

        public Booking GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Booking Object)
        {
            SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text,
                new SqlParameter[]
                {
                new SqlParameter("@IdCustomer", Object.IdCustomer),
                new SqlParameter("@NroDocument", Object.NroDocument),
                new SqlParameter("@RegistrationDate", Object.RegistrationDate),
                new SqlParameter("@RegistrationBooking", Object.RegistrationBooking),
                new SqlParameter("@StartTime", Object.StartTime),
                new SqlParameter("@EndTime", Object.EndTime),
                new SqlParameter("@Field", Object.Field),
                new SqlParameter("@Promotion", Object.Promotion),
                new SqlParameter("@State", Object.State)
                });
        }

        public void Update(Guid Id, Booking Object)
        {
            SqlHelper.ExecuteNonQuery(UpdateStatement, System.Data.CommandType.Text,
                new SqlParameter[]
                {
                new SqlParameter("@IdBooking", Id),
                new SqlParameter("@IdCustomer", Object.IdCustomer),
                new SqlParameter("@NroDocument", Object.NroDocument),
                new SqlParameter("@RegistrationDate", Object.RegistrationDate),
                new SqlParameter("@RegistrationBooking", Object.RegistrationBooking),
                new SqlParameter("@StartTime", Object.StartTime),
                new SqlParameter("@EndTime", Object.EndTime),
                new SqlParameter("@Field", Object.Field),
                new SqlParameter("@Promotion", Object.Promotion),
                new SqlParameter("@State", Object.State)
                });
        }
    }
}
