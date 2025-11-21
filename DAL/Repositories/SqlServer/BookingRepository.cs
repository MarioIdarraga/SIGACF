using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL Server para la entidad <see cref="Booking"/>.
    /// Se encarga de realizar las operaciones CRUD contra la tabla [Bookings].
    /// </summary>
    internal class BookingRepository : IGenericRepository<Booking>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Bookings] (IdCustomer, NroDocument, RegistrationDate, RegistrationBooking, StartTime, EndTime, Field, Promotion, State, ImporteBooking, DVH) VALUES (@IdCustomer, @NroDocument, @RegistrationDate, @RegistrationBooking, @StartTime, @EndTime, @Field, @Promotion, @State, @ImporteBooking, @DVH)";
        }
        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Bookings] SET IdCustomer = @IdCustomer, NroDocument = @NroDocument, RegistrationDate = @RegistrationDate, RegistrationBooking = @RegistrationBooking, StartTime = @StartTime, EndTime = @EndTime, Field = @Field, Promotion = @Promotion, State = @State, ImporteBooking = @ImporteBooking, DVH =@DVH WHERE IdBooking = @IdBooking";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Bookings] WHERE IdBooking = @IdBooking";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdBooking, IdCustomer, NroDocument, RegistrationDate, RegistrationBooking, StartTime, EndTime, Field, Promotion, State, ImporteBooking, DVH FROM [dbo].[Bookings] WHERE IdBooking = @IdBooking";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdBooking, IdCustomer, NroDocument, RegistrationDate, RegistrationBooking, StartTime, EndTime, Field, Promotion, State, ImporteBooking, DVH FROM [dbo].[Bookings]";
        }

        /// <summary>
        /// Sentencia base para obtener reservas y poder filtrar por datos del cliente
        /// (nombre, apellido, teléfono, mail) realizando un JOIN con Customers.
        /// </summary>
        private string SelectAllWithCustomerStatement
        {
            get => @"
                SELECT 
                    b.IdBooking,
                    b.IdCustomer,
                    b.NroDocument,
                    b.RegistrationDate,
                    b.RegistrationBooking,
                    b.StartTime,
                    b.EndTime,
                    b.Field,
                    b.Promotion,
                    b.State,
                    b.ImporteBooking,
                    b.DVH
                FROM [dbo].[Bookings] b
                INNER JOIN [dbo].[Customers] c ON b.IdCustomer = c.IdCustomer";
        }
        #endregion

        /// <summary>
        /// Elimina una reserva por su identificador.
        /// </summary>
        /// <param name="Id">Identificador de la reserva a eliminar.</param>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al eliminar la reserva en la base de datos.
        /// </exception>
        public void Delete(Guid Id)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(
                    DeleteStatement,
                    CommandType.Text,
                    new SqlParameter[] { new SqlParameter("@IdBooking", Id) });
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar la reserva.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al eliminar la reserva.", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las reservas que coinciden con los filtros opcionales
        /// por número de documento y datos del cliente.
        /// </summary>
        /// <param name="nroDocument">
        /// Número de documento del cliente. Si es <c>null</c>, no se filtra por documento.
        /// </param>
        /// <param name="firstName">
        /// Nombre del cliente. Si es nulo o vacío, no se aplica este filtro.
        /// </param>
        /// <param name="lastName">
        /// Apellido del cliente. Si es nulo o vacío, no se aplica este filtro.
        /// </param>
        /// <param name="telephone">
        /// Teléfono del cliente. Si es nulo o vacío, no se aplica este filtro.
        /// </param>
        /// <param name="mail">
        /// Mail del cliente. Si es nulo o vacío, no se aplica este filtro.
        /// </param>
        /// <returns>Colección de reservas que cumplen con los filtros indicados.</returns>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al consultar las reservas en la base de datos.
        /// </exception>
        public IEnumerable<Booking> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            var bookings = new List<Booking>();

            try
            {
                string query = SelectAllWithCustomerStatement + " WHERE 1 = 1";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (nroDocument.HasValue)
                {
                    query += " AND b.NroDocument = @NroDocument";
                    parameters.Add(new SqlParameter("@NroDocument", nroDocument.Value));
                }

                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    query += " AND c.FirstName LIKE @FirstName";
                    parameters.Add(new SqlParameter("@FirstName", firstName + "%"));
                }

                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    query += " AND c.LastName LIKE @LastName";
                    parameters.Add(new SqlParameter("@LastName", lastName + "%"));
                }

                if (!string.IsNullOrWhiteSpace(telephone))
                {
                    query += " AND c.Telephone LIKE @Telephone";
                    parameters.Add(new SqlParameter("@Telephone", telephone + "%"));
                }

                if (!string.IsNullOrWhiteSpace(mail))
                {
                    query += " AND c.Mail LIKE @Mail";
                    parameters.Add(new SqlParameter("@Mail", mail + "%"));
                }

                using (var reader = SqlHelper.ExecuteReader(
                    query,
                    CommandType.Text,
                    parameters.ToArray()))
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            IdBooking = reader.GetGuid(0),
                            IdCustomer = reader.GetGuid(1),
                            NroDocument = reader.GetString(2),
                            RegistrationDate = reader.GetDateTime(3),
                            RegistrationBooking = reader.GetDateTime(4),
                            StartTime = reader.GetTimeSpan(5),
                            EndTime = reader.GetTimeSpan(6),
                            Field = reader.GetGuid(7),
                            Promotion = reader.GetGuid(8),
                            State = reader.GetInt32(9),
                            ImporteBooking = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10),
                            DVH = reader.GetString(11)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener las reservas filtradas por datos del cliente.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al obtener las reservas filtradas por datos del cliente.", ex);
            }

            return bookings;
        }

        /// <summary>
        /// Obtiene todas las reservas sin aplicar filtros.
        /// </summary>
        /// <returns>Colección de todas las reservas.</returns>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al consultar las reservas en la base de datos.
        /// </exception>
        public IEnumerable<Booking> GetAll()
        {
            var bookings = new List<Booking>();

            try
            {
                string query = SelectAllStatement + " WHERE 1=1";
                List<SqlParameter> parameters = new List<SqlParameter>();

                using (var reader = SqlHelper.ExecuteReader(
                    query,
                    CommandType.Text,
                    parameters.ToArray()))
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            IdBooking = reader.GetGuid(0),
                            IdCustomer = reader.GetGuid(1),
                            NroDocument = reader.GetString(2),
                            RegistrationDate = reader.GetDateTime(3),
                            RegistrationBooking = reader.GetDateTime(4),
                            StartTime = reader.GetTimeSpan(5),
                            EndTime = reader.GetTimeSpan(6),
                            Field = reader.GetGuid(7),
                            Promotion = reader.GetGuid(8),
                            State = reader.GetInt32(9),
                            ImporteBooking = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10),
                            DVH = reader.GetString(11)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener todas las reservas.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al obtener todas las reservas.", ex);
            }

            return bookings;
        }

        /// <summary>
        /// Obtiene las reservas aplicando filtros opcionales por número de documento
        /// y fechas (reserva y fecha de alta).
        /// </summary>
        /// <param name="nroDocument">
        /// Número de documento del cliente. Si es <c>null</c>, no se filtra por documento.
        /// </param>
        /// <param name="registrationBooking">
        /// Fecha de la reserva. Si es <c>null</c>, no se aplica este filtro.
        /// </param>
        /// <param name="registrationDate">
        /// Fecha de alta de la reserva. Si es <c>null</c>, no se aplica este filtro.
        /// </param>
        /// <returns>Colección de reservas que cumplen con los filtros indicados.</returns>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al consultar las reservas en la base de datos.
        /// </exception>
        public IEnumerable<Booking> GetAll(int? nroDocument = null, DateTime? registrationBooking = null, DateTime? registrationDate = null)
        {
            var bookings = new List<Booking>();

            try
            {
                string query = SelectAllStatement + " WHERE 1=1";
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

                using (var reader = SqlHelper.ExecuteReader(
                    query,
                    CommandType.Text,
                    parameters.ToArray()))
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            IdBooking = reader.GetGuid(0),
                            IdCustomer = reader.GetGuid(1),
                            NroDocument = reader.GetString(2),
                            RegistrationDate = reader.GetDateTime(3),
                            RegistrationBooking = reader.GetDateTime(4),
                            StartTime = reader.GetTimeSpan(5),
                            EndTime = reader.GetTimeSpan(6),
                            Field = reader.GetGuid(7),
                            Promotion = reader.GetGuid(8),
                            State = reader.GetInt32(9),
                            ImporteBooking = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10),
                            DVH = reader.GetString(11)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener las reservas filtradas por documento y fechas.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al obtener las reservas filtradas por documento y fechas.", ex);
            }

            return bookings;
        }

        /// <summary>
        /// Obtiene las reservas filtradas por número de documento y por rangos de fechas
        /// obligatorios (reserva y fecha de alta).
        /// </summary>
        /// <param name="nroDocument">Número de documento del cliente. Si es <c>null</c>, no se filtra por documento.</param>
        /// <param name="registrationBooking">Fecha de la reserva (obligatoria).</param>
        /// <param name="registrationDate">Fecha de alta de la reserva (obligatoria).</param>
        /// <returns>Colección de reservas que cumplen con los filtros indicados.</returns>
        public IEnumerable<Booking> GetAll(int? nroDocument, DateTime registrationBooking, DateTime registrationDate)
        {
            // Reutilizamos la sobrecarga con parámetros opcionales.
            return GetAll(nroDocument, (DateTime?)registrationBooking, (DateTime?)registrationDate);
        }

        /// <summary>
        /// Obtiene las reservas filtradas por número de documento, datos del cliente
        /// y estado de la reserva.
        /// </summary>
        /// <param name="nroDocument">Número de documento del cliente. Si es <c>null</c>, no se filtra por documento.</param>
        /// <param name="firstName">Nombre del cliente. Si es nulo o vacío, no se aplica este filtro.</param>
        /// <param name="lastName">Apellido del cliente. Si es nulo o vacío, no se aplica este filtro.</param>
        /// <param name="telephone">Teléfono del cliente. Si es nulo o vacío, no se aplica este filtro.</param>
        /// <param name="mail">Mail del cliente. Si es nulo o vacío, no se aplica este filtro.</param>
        /// <param name="state">Estado de la reserva. Si es menor que 0, no se filtra por estado.</param>
        /// <returns>Colección de reservas que cumplen con los filtros indicados.</returns>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al consultar las reservas en la base de datos.
        /// </exception>
        public IEnumerable<Booking> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            var bookings = new List<Booking>();

            try
            {
                string query = SelectAllWithCustomerStatement + " WHERE 1 = 1";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (nroDocument.HasValue)
                {
                    query += " AND b.NroDocument = @NroDocument";
                    parameters.Add(new SqlParameter("@NroDocument", nroDocument.Value));
                }

                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    query += " AND c.FirstName LIKE @FirstName";
                    parameters.Add(new SqlParameter("@FirstName", firstName + "%"));
                }

                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    query += " AND c.LastName LIKE @LastName";
                    parameters.Add(new SqlParameter("@LastName", lastName + "%"));
                }

                if (!string.IsNullOrWhiteSpace(telephone))
                {
                    query += " AND c.Telephone LIKE @Telephone";
                    parameters.Add(new SqlParameter("@Telephone", telephone + "%"));
                }

                if (!string.IsNullOrWhiteSpace(mail))
                {
                    query += " AND c.Mail LIKE @Mail";
                    parameters.Add(new SqlParameter("@Mail", mail + "%"));
                }

                if (state >= 0)
                {
                    query += " AND b.State = @State";
                    parameters.Add(new SqlParameter("@State", state));
                }

                using (var reader = SqlHelper.ExecuteReader(
                    query,
                    CommandType.Text,
                    parameters.ToArray()))
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            IdBooking = reader.GetGuid(0),
                            IdCustomer = reader.GetGuid(1),
                            NroDocument = reader.GetString(2),
                            RegistrationDate = reader.GetDateTime(3),
                            RegistrationBooking = reader.GetDateTime(4),
                            StartTime = reader.GetTimeSpan(5),
                            EndTime = reader.GetTimeSpan(6),
                            Field = reader.GetGuid(7),
                            Promotion = reader.GetGuid(8),
                            State = reader.GetInt32(9),
                            ImporteBooking = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10),
                            DVH = reader.GetString(11)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener las reservas filtradas por datos del cliente y estado.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al obtener las reservas filtradas por datos del cliente y estado.", ex);
            }

            return bookings;
        }

        /// <summary>
        /// Obtiene una reserva por su identificador.
        /// </summary>
        /// <param name="Id">Identificador de la reserva.</param>
        /// <returns>
        /// La reserva encontrada; o <c>null</c> si no existe una reserva con el identificador indicado.
        /// </returns>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al consultar la reserva en la base de datos.
        /// </exception>
        public Booking GetOne(Guid Id)
        {
            Booking booking = null;

            try
            {
                using (var reader = SqlHelper.ExecuteReader(
                    SelectOneStatement,
                    CommandType.Text,
                    new SqlParameter[] { new SqlParameter("@IdBooking", Id) }))
                {
                    if (reader.Read())
                    {
                        booking = new Booking
                        {
                            IdBooking = reader.GetGuid(0),
                            IdCustomer = reader.GetGuid(1),
                            NroDocument = reader.GetString(2),
                            RegistrationDate = reader.GetDateTime(3),
                            RegistrationBooking = reader.GetDateTime(4),
                            StartTime = reader.GetTimeSpan(5),
                            EndTime = reader.GetTimeSpan(6),
                            Field = reader.GetGuid(7),
                            Promotion = reader.GetGuid(8),
                            State = reader.GetInt32(9),
                            ImporteBooking = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10),
                            DVH = reader.GetString(11)
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener la reserva.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al obtener la reserva.", ex);
            }

            return booking;
        }

        /// <summary>
        /// Inserta una nueva reserva en la base de datos.
        /// </summary>
        /// <param name="Object">Objeto <see cref="Booking"/> a insertar.</param>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al insertar la reserva en la base de datos.
        /// </exception>
        public void Insert(Booking Object)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(
                    InsertStatement,
                    CommandType.Text,
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
                        new SqlParameter("@State", Object.State),
                        new SqlParameter("@ImporteBooking", Object.ImporteBooking),
                        new SqlParameter("@DVH", Object.DVH)
                    });
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al insertar la reserva.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al insertar la reserva.", ex);
            }
        }

        /// <summary>
        /// Actualiza una reserva existente en la base de datos.
        /// </summary>
        /// <param name="Id">Identificador de la reserva a actualizar.</param>
        /// <param name="Object">Objeto <see cref="Booking"/> con los nuevos valores.</param>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al actualizar la reserva en la base de datos.
        /// </exception>
        public void Update(Guid Id, Booking Object)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(
                    UpdateStatement,
                    CommandType.Text,
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
                        new SqlParameter("@State", Object.State),
                        new SqlParameter("@ImporteBooking", Object.ImporteBooking),
                        new SqlParameter("@DVH", Object.DVH)
                    });
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar la reserva.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al actualizar la reserva.", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las reservas con un estado específico, y opcionalmente filtradas por fecha.
        /// </summary>
        /// <param name="from">Fecha inicial del filtro (opcional)</param>
        /// <param name="to">Fecha final del filtro (opcional)</param>
        /// <param name="state">Estado de la reserva (ej: 3 = Cancelado)</param>
        /// <returns>Listado de reservas</returns>
        public IEnumerable<Booking> GetAll(DateTime? from, DateTime? to, int state)
        {
            var bookings = new List<Booking>();

            try
            {
                string query = SelectAllStatement + " WHERE State = @State";
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@State", state)
                };

                if (from.HasValue)
                {
                    query += " AND CAST(RegistrationBooking AS DATE) >= @From";
                    parameters.Add(new SqlParameter("@From", from.Value.Date));
                }

                if (to.HasValue)
                {
                    query += " AND CAST(RegistrationBooking AS DATE) <= @To";
                    parameters.Add(new SqlParameter("@To", to.Value.Date));
                }

                using (var reader = SqlHelper.ExecuteReader(query, CommandType.Text, parameters.ToArray()))
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Booking
                        {
                            IdBooking = reader.GetGuid(0),
                            IdCustomer = reader.GetGuid(1),
                            NroDocument = reader.GetString(2),
                            RegistrationDate = reader.GetDateTime(3),
                            RegistrationBooking = reader.GetDateTime(4),
                            StartTime = reader.GetTimeSpan(5),
                            EndTime = reader.GetTimeSpan(6),
                            Field = reader.GetGuid(7),
                            Promotion = reader.GetGuid(8),
                            State = reader.GetInt32(9),
                            ImporteBooking = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10),
                            DVH = reader.GetString(11)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las reservas por fecha y estado.", ex);
            }

            return bookings;
        }
    }
}
