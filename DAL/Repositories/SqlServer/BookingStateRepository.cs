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
    class BookingStateRepository : IGenericRepository<BookingState>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[BookingsStates] (IdStateBooking, Description) VALUES (@IdStateBooking, @Description)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[BookingsStates] SET (IdStateBooking, Description) WHERE IdStateBooking = @IdStateBooking";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[BookingsStates] WHERE IdStateBooking = @IdStateBooking";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdStateBooking, Description FROM [dbo].[BookingsStates] WHERE IdStateBooking = @IdStateBooking";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdStateBooking, Description FROM [dbo].[BookingsStates]";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Elimina un registro de la tabla [BookingsState] según su ID.
        /// </summary>
        public void Delete(Guid Id)
        {
            SqlHelper.ExecuteNonQuery(
                DeleteStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@IdStateBooking", Id)
                }
            );
        }

        /// <summary>
        /// Devuelve un objeto BookingState según su ID (o null si no existe).
        /// </summary>
        public BookingState GetOne(Guid Id)
        {
            using (var reader = SqlHelper.ExecuteReader(
                SelectOneStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@IdStateBooking", Id)
                }))
            {
                if (reader.Read())
                {
                    return new BookingState
                    {
                        // El índice 0 se asume para IdStateBooking y 1 para Description
                        // (coincidiendo con SELECT IdStateBooking, Description).
                        IdStateBooking = reader.GetInt32(0),
                        Description = reader.GetString(1)
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Inserta un nuevo registro en la tabla [BookingsState].
        /// </summary>
        public void Insert(BookingState Object)
        {
            SqlHelper.ExecuteNonQuery(
                InsertStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@IdStateBooking", Object.IdStateBooking),
                    new SqlParameter("@Description", Object.Description)
                }
            );
        }

        /// <summary>
        /// Actualiza la descripción del BookingState cuyo ID se provea.
        /// </summary>
        public void Update(Guid Id, BookingState Object)
        {
            SqlHelper.ExecuteNonQuery(
                UpdateStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@IdStateBooking", Id),
                    new SqlParameter("@Description", Object.Description)
                }
            );
        }

        /// <summary>
        /// Retorna todos los registros de la tabla [BookingsState].
        /// </summary>
        public IEnumerable<BookingState> GetAll()
        {
            var list = new List<BookingState>();

            try
            {
                using (var reader = SqlHelper.ExecuteReader(
                    SelectAllStatement,
                    CommandType.Text))
                {
                    while (reader.Read())
                    {
                        list.Add(new BookingState
                        {
                            IdStateBooking = reader.GetInt32(0),
                            Description = reader.GetString(1)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener los estados de reserva.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al obtener los estados de reserva.", ex);
            }

            return list;
        }

        public IEnumerable<BookingState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingState> GetAll(DateTime? from, DateTime? to, int state)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
