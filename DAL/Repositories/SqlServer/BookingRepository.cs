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
            get => "UPDATE [dbo].[Bookings] SET (IdCustomer, NroDocument, RegistrationDate, RegistrationBooking, StartTime, EndTime, Field, Promotion, State ) WHERE IdBooking = @IdBooking";
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

        IEnumerable<Booking> IGenericRepository<Booking>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
