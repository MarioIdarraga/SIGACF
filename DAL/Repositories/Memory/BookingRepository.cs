using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para la entidad <see cref="Booking"/>.
    /// </summary>
    internal class BookingRepository : IGenericRepository<Booking>
    {
        /// <summary>
        /// Lista estática en memoria que almacena las reservas.
        /// </summary>
        private static readonly List<Booking> _bookings = new List<Booking>()
        {
            new Booking
            {
                IdBooking = Guid.NewGuid(),
                IdCustomer = Guid.NewGuid(),
                NroDocument = "99999999",
                RegistrationDate = DateTime.Now.AddDays(-2),
                RegistrationBooking = DateTime.Today,
                StartTime = new TimeSpan(14,0,0),
                EndTime = new TimeSpan(15,0,0),
                Field = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Promotion = Guid.Empty,
                State = 1, // Registrada
                ImporteBooking = 5000,
                DVH = ""
            },
            new Booking
            {
                IdBooking = Guid.NewGuid(),
                IdCustomer = Guid.NewGuid(),
                NroDocument = "12345678",
                RegistrationDate = DateTime.Now.AddDays(-10),
                RegistrationBooking = DateTime.Today.AddDays(-5),
                StartTime = new TimeSpan(18,0,0),
                EndTime = new TimeSpan(19,0,0),
                Field = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Promotion = Guid.Empty,
                State = 3, // Pagada
                ImporteBooking = 7000,
                DVH = ""
            }
        };

        /// <summary>
        /// Inserta una nueva reserva en memoria.
        /// </summary>
        public void Insert(Booking Object)
        {
            try
            {
                if (Object.IdBooking == Guid.Empty)
                    Object.IdBooking = Guid.NewGuid();

                _bookings.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la reserva en memoria.", ex);
            }
        }

        /// <summary>
        /// Actualiza una reserva existente en memoria.
        /// </summary>
        public void Update(Guid Id, Booking Object)
        {
            try
            {
                var existing = _bookings.FirstOrDefault(b => b.IdBooking == Id);

                if (existing == null)
                    throw new Exception("La reserva indicada no existe.");

                _bookings.Remove(existing);

                Object.IdBooking = Id;
                _bookings.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la reserva en memoria.", ex);
            }
        }

        /// <summary>
        /// Elimina una reserva en memoria.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var booking = _bookings.FirstOrDefault(b => b.IdBooking == Id);

                if (booking == null)
                    throw new Exception("La reserva indicada no existe.");

                _bookings.Remove(booking);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reserva en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene una reserva por su identificador.
        /// </summary>
        public Booking GetOne(Guid Id)
        {
            try
            {
                return _bookings.FirstOrDefault(b => b.IdBooking == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la reserva en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las reservas sin filtro.
        /// </summary>
        public IEnumerable<Booking> GetAll()
        {
            try
            {
                return _bookings.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las reservas en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene reservas filtradas por documento y fechas.
        /// </summary>
        public IEnumerable<Booking> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            try
            {
                var list = _bookings.AsEnumerable();

                if (nroDocument.HasValue)
                    list = list.Where(x => x.NroDocument == nroDocument.Value.ToString());

                if (registrationBooking.HasValue)
                    list = list.Where(x => x.RegistrationBooking.Date == registrationBooking.Value.Date);

                if (registrationDate.HasValue)
                    list = list.Where(x => x.RegistrationDate.Date == registrationDate.Value.Date);

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas filtradas en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene reservas filtradas por documento, cliente y estado.
        /// </summary>
        public IEnumerable<Booking> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            try
            {
                var list = _bookings.AsEnumerable();

                if (nroDocument.HasValue)
                    list = list.Where(x => x.NroDocument == nroDocument.Value.ToString());

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas filtradas en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene reservas filtradas por documento, cliente y estado.
        /// </summary>
        public IEnumerable<Booking> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            try
            {
                var list = _bookings.AsEnumerable();

                if (state >= 0)
                    list = list.Where(x => x.State == state);

                if (nroDocument.HasValue)
                    list = list.Where(x => x.NroDocument == nroDocument.Value.ToString());

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas filtradas por estado en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene reservas por fecha y estado.
        /// </summary>
        public IEnumerable<Booking> GetAll(DateTime? from, DateTime? to, int state)
        {
            try
            {
                var list = _bookings.AsEnumerable();

                list = list.Where(x => x.State == state);

                if (from.HasValue)
                    list = list.Where(x => x.RegistrationBooking.Date >= from.Value.Date);

                if (to.HasValue)
                    list = list.Where(x => x.RegistrationBooking.Date <= to.Value.Date);

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas por fecha y estado en memoria.", ex);
            }
        }
    }
}

