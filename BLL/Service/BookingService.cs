using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using DAL.Factory;
using Domain;

namespace BLL.Service
{
    public class BookingService
    {
        private readonly IGenericRepository<Booking> _bookingRepo;

        public BookingService(IGenericRepository<Booking> bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public void RegisterBooking(Booking booking)
        {
            ValidateBookingCreate(booking);
            _bookingRepo.Insert(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.IdBooking == Guid.Empty)
                throw new ArgumentException("El ID de la reserva es obligatorio.");

            ValidateBookingCan(booking);

            var existing = _bookingRepo.GetOne(booking.IdBooking);
            if (existing == null)
                throw new InvalidOperationException("No se encontró la reserva a actualizar.");

            _bookingRepo.Update(booking.IdBooking, booking);
        }

        private void ValidateBookingCreate(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.IdCustomer == Guid.Empty)
                throw new ArgumentException("Debe asociarse un cliente a la reserva.");

            if (string.IsNullOrWhiteSpace(booking.NroDocument))
                throw new ArgumentException("El número de documento es obligatorio.");

            if (booking.RegistrationBooking < DateTime.Today)
                throw new ArgumentException("No se puede reservar en fechas anteriores a hoy.");

            if (booking.StartTime >= booking.EndTime)
                throw new ArgumentException("La hora de inicio debe ser anterior a la hora de fin.");

            var duration = (booking.EndTime - booking.StartTime).TotalHours;
            if (duration <= 0)
                throw new ArgumentException("La duración de la reserva debe ser positiva.");
            if (duration > 3)
                throw new ArgumentException("La duración de la reserva no puede exceder las 3 horas.");

            if (booking.Field == Guid.Empty)
                throw new ArgumentException("Debe seleccionarse una cancha.");
        }
        private void ValidateBookingCan(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.IdCustomer == Guid.Empty)
                throw new ArgumentException("Debe asociarse un cliente a la reserva.");

            if (string.IsNullOrWhiteSpace(booking.NroDocument))
                throw new ArgumentException("El número de documento es obligatorio.");

            if (booking.Field == Guid.Empty)
                throw new ArgumentException("Debe seleccionarse una cancha.");

        }

        public List<Booking> GetAll(int? nroDocumento, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return _bookingRepo.GetAll(nroDocumento, registrationBooking, registrationDate).ToList();
        }

        public List<Booking> GetAllRep(int? nroDocumento, string firstName, string lastName, string telephone, string mail, int estado)
        {
            return _bookingRepo.GetAll(nroDocumento, firstName, lastName, telephone, mail, estado).ToList();
        }
        public decimal CalcularImporteReserva(Guid idField, TimeSpan startTime, TimeSpan endTime, Guid idPromotion)
        {
            var fieldRepo = Factory.Current.GetFieldRepository();
            var promotionRepo = Factory.Current.GetPromotionRepository();

            var field = fieldRepo.GetOne(idField);
            var promotion = promotionRepo.GetOne(idPromotion);

            if (field == null)
                throw new ArgumentException("La cancha no existe.");

            double horas = (endTime - startTime).TotalHours;
            if (horas <= 0)
                throw new ArgumentException("La duración debe ser positiva.");

            decimal importe = (decimal)horas * (decimal)field.HourlyCost;

            if (promotion != null && promotion.DiscountPercentage > 0)
            {
                importe -= importe * promotion.DiscountPercentage / 100;
            }

            return importe;
        }

        /// <summary>
        /// Obtiene todas las reservas en un rango de fechas y con un estado específico.
        /// </summary>
        /// <param name="from">Fecha inicial (opcional)</param>
        /// <param name="to">Fecha final (opcional)</param>
        /// <param name="state">Estado de la reserva</param>
        /// <returns>Lista de reservas</returns>
        public List<Booking> GetAllByDateRangeAndState(DateTime? from, DateTime? to, int state)
        {
            try
            {
                return _bookingRepo.GetAll(from, to, state).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas por rango de fechas y estado.", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las reservas de una cancha para una fecha específica.
        /// </summary>
        /// <param name="fieldId">Identificador de la cancha.</param>
        /// <param name="bookingDate">Fecha de la reserva (solo parte de fecha).</param>
        /// <returns>Lista de reservas para esa cancha en esa fecha.</returns>
        public List<Booking> GetBookingsByFieldAndDate(Guid fieldId, DateTime bookingDate)
        {
            try
            {
                // Reutilizamos GetAll(nroDocument, registrationBooking, registrationDate)
                // nroDocument = null, registrationBooking = bookingDate
                var reservasDelDia = _bookingRepo
                    .GetAll(null, bookingDate.Date, null)
                    .ToList();

                return reservasDelDia
                    .Where(b => b.Field == fieldId)
                    .OrderBy(b => b.StartTime)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas por cancha y fecha.", ex);
            }
        }
        /// <summary>
        /// Obtiene las reservas dentro de un rango de fechas.
        /// Filtra por RegistrationBooking.Date entre from y to.
        /// Si algún parámetro es null, no se aplica ese filtro.
        /// </summary>
        /// <param name="from">Fecha inicial (opcional).</param>
        /// <param name="to">Fecha final (opcional).</param>
        /// <returns>Listado de reservas en el rango indicado.</returns>
        public IEnumerable<Booking> GetBookingsByDateRange(DateTime? from, DateTime? to)
        {
            try
            {
                var all = _bookingRepo.GetAll().ToList();

                if (from.HasValue)
                    all = all
                        .Where(b => b.RegistrationBooking.Date >= from.Value.Date)
                        .ToList();

                if (to.HasValue)
                    all = all
                        .Where(b => b.RegistrationBooking.Date <= to.Value.Date)
                        .ToList();

                return all;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener reservas por rango de fechas.", ex);
            }
        }
    }
}

