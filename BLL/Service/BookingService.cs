using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using DAL.Factory;
using Domain;
using BLL.BusinessException;

namespace BLL.Service
{
    /// <summary>
    /// Servicio encargado de manejar la lógica de negocio para las reservas (Bookings):
    /// - Validaciones de datos obligatorios
    /// - Validaciones de reglas de negocio (duración, duplicados, cancha existente, cliente existente)
    /// - Exposición de consultas utilizadas por la capa SL
    /// </summary>
    public class BookingService
    {
        private readonly IGenericRepository<Booking> _bookingRepo;

        public BookingService(IGenericRepository<Booking> bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }


        /// <summary>
        /// Registra una nueva reserva aplicando TODAS las reglas de negocio.
        /// </summary>
        public void RegisterBooking(Booking booking)
        {
            ValidateBookingCreate(booking);
            ValidateDuplicateBooking(booking);

            _bookingRepo.Insert(booking);
        }


        /// <summary>
        /// Actualiza una reserva existente aplicando validaciones generales.
        /// </summary>
        public void UpdateBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.IdBooking == Guid.Empty)
                throw new ArgumentException("El ID de la reserva es obligatorio.");

            ValidateBookingCan(booking);
            ValidateDuplicateBooking(booking, isUpdate: true);

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
                throw new ClienteInexistenteException("Debe asociarse un cliente a la reserva.");

            if (string.IsNullOrWhiteSpace(booking.NroDocument))
                throw new ArgumentException("El número de documento es obligatorio.");

            if (booking.RegistrationBooking < DateTime.Today)
                throw new ArgumentException("No se puede reservar en fechas anteriores a hoy.");

            if (booking.StartTime >= booking.EndTime)
                throw new DuracionReservaInvalidaException("La hora de inicio debe ser anterior a la hora de fin.");

            var duration = (booking.EndTime - booking.StartTime).TotalHours;

            if (duration <= 0)
                throw new DuracionReservaInvalidaException("La duración de la reserva debe ser positiva.");

            if (duration > 3)
                throw new DuracionReservaInvalidaException("La duración de la reserva no puede exceder las 3 horas.");

            if (booking.Field == Guid.Empty)
                throw new CanchaInexistenteException("Debe seleccionarse una cancha.");
        }

        private void ValidateBookingCan(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.IdCustomer == Guid.Empty)
                throw new ClienteInexistenteException("Debe asociarse un cliente a la reserva.");

            if (string.IsNullOrWhiteSpace(booking.NroDocument))
                throw new ArgumentException("El número de documento es obligatorio.");

            if (booking.Field == Guid.Empty)
                throw new CanchaInexistenteException("Debe seleccionarse una cancha.");
        }

        /// <summary>
        /// Valida si existe una reserva solapada para la misma cancha.
        /// Regla de negocio: una cancha no puede reservarse dos veces en el mismo horario.
        /// </summary>
        private void ValidateDuplicateBooking(Booking booking, bool isUpdate = false)
        {
            var reservasDia = _bookingRepo
                .GetAll(null, booking.RegistrationBooking.Date, null)
                .ToList();

            // Filtrado para edición → ignorar la reserva que se está actualizando.
            if (isUpdate)
                reservasDia = reservasDia.Where(x => x.IdBooking != booking.IdBooking).ToList();

            bool solapamiento = reservasDia.Any(r =>
                !(booking.EndTime <= r.StartTime || booking.StartTime >= r.EndTime)
            );

            if (solapamiento)
                throw new ReservaDuplicadaException(
                    "Ya existe una reserva en ese horario para la cancha seleccionada.");
        }


        /// <summary>
        /// Devuelve todas las reservas filtradas por documento y fechas.
        /// </summary>
        public List<Booking> GetAll(int? nroDocumento, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return _bookingRepo.GetAll(nroDocumento, registrationBooking, registrationDate).ToList();
        }

        /// <summary>
        /// Devuelve reservas filtradas por datos del cliente.
        /// Utilizado para reportes (ej: reservas canceladas).
        /// </summary>
        public List<Booking> GetAllRep(int? nroDocumento, string firstName, string lastName,
                                       string telephone, string mail, int estado)
        {
            return _bookingRepo
                .GetAll(nroDocumento, firstName, lastName, telephone, mail, estado)
                .ToList();
        }

        /// <summary>
        /// Obtiene reservas en un rango de fechas y según estado.
        /// </summary>
        public List<Booking> GetAllByDateRangeAndState(DateTime? from, DateTime? to, int state)
        {
            return _bookingRepo.GetAll(from, to, state).ToList();
        }

        /// <summary>
        /// Obtiene todas las reservas de una cancha en una fecha específica.
        /// </summary>
        public List<Booking> GetBookingsByFieldAndDate(Guid fieldId, DateTime bookingDate)
        {
            var reservas = _bookingRepo.GetAll(null, bookingDate.Date, null).ToList();

            return reservas
                .Where(b => b.Field == fieldId)
                .OrderBy(b => b.StartTime)
                .ToList();
        }

        /// <summary>
        /// Obtiene las reservas en un rango de fechas.
        /// </summary>
        public IEnumerable<Booking> GetBookingsByDateRange(DateTime? from, DateTime? to)
        {
            var all = _bookingRepo.GetAll().ToList();

            if (from.HasValue)
                all = all.Where(b => b.RegistrationBooking.Date >= from.Value.Date).ToList();

            if (to.HasValue)
                all = all.Where(b => b.RegistrationBooking.Date <= to.Value.Date).ToList();

            return all;
        }


        /// <summary>
        /// Calcula el importe total de la reserva según cancha, horario y promoción.
        /// </summary>
        public decimal CalcularImporteReserva(Guid idField, TimeSpan startTime, TimeSpan endTime, Guid idPromotion)
        {
            var fieldRepo = Factory.Current.GetFieldRepository();
            var promotionRepo = Factory.Current.GetPromotionRepository();

            var field = fieldRepo.GetOne(idField);
            if (field == null)
                throw new CanchaInexistenteException("La cancha no existe.");

            var duration = (endTime - startTime).TotalHours;
            if (duration <= 0)
                throw new DuracionReservaInvalidaException("La duración debe ser positiva.");

            decimal importe = (decimal)duration * (decimal)field.HourlyCost;

            var promotion = promotionRepo.GetOne(idPromotion);
            if (promotion != null && (promotion.DiscountPercentage ?? 0) > 0)
            {
                importe -= importe * ((promotion.DiscountPercentage ?? 0) / 100m);
            }

            return importe;
        }
    }
}


