using BLL.BusinessException;
using BLL.Service;
using Domain;
using SL.Helpers;
using SL.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace SL
{
    /// <summary>
    /// Service Layer para operaciones de reservas (Booking).
    /// Centraliza el acceso a BookingService e incorpora logging y DVH/DVV.
    /// </summary>
    public class BookingSLService
    {
        private readonly BookingService _bookingService;

        /// <summary>
        /// Crea una nueva instancia del servicio de reservas en la capa SL.
        /// </summary>
        /// <param name="bookingService">Instancia de la capa de negocio.</param>
        public BookingSLService(BookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        /// <summary>
        /// Devuelve el login del usuario actual para dejar trazas en el log.
        /// </summary>
        private string CurrentUser => Session.CurrentUser?.LoginName;

        #region Insert / Update

        /// <summary>
        /// Inserta una nueva reserva, calculando DVH y actualizando DVV.
        /// </summary>
        public void Insert(Booking booking)
        {
            LoggerService.Log("Inicio de registro de reserva.", EventLevel.Informational, CurrentUser);

            try
            {
                if (booking == null) throw new ArgumentNullException(nameof(booking));

                // Calcular DVH
                booking.DVH = DVHHelper.CalcularDVH(booking);

                // Insertar reserva (capa BLL)
                _bookingService.RegisterBooking(booking);

                // Recalcular DVV sobre la tabla Bookings
                var repo = global::DAL.Factory.Factory.Current.GetBookingRepository();
                var bookings = repo.GetAll();
                new DVVService().RecalcularDVV(bookings, "Bookings");

                LoggerService.Log("Reserva registrada correctamente.", EventLevel.Informational, CurrentUser);
            }
            catch (BusinessException ex)
            {
                LoggerService.Log($"Error de negocio al registrar reserva: {ex.Message}", EventLevel.Warning, CurrentUser);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error inesperado al registrar reserva: {ex}", EventLevel.Error, CurrentUser);
                throw;
            }
        }

        /// <summary>
        /// Actualiza una reserva existente, recalculando DVH y DVV.
        /// </summary>
        public void Update(Guid idBooking, Booking booking)
        {
            LoggerService.Log($"Inicio de modificación de reserva {idBooking}.", EventLevel.Informational, CurrentUser);

            try
            {
                if (booking == null) throw new ArgumentNullException(nameof(booking));

                // Recalcular DVH
                booking.DVH = DVHHelper.CalcularDVH(booking);

                // Actualizar reserva (capa BLL)
                _bookingService.UpdateBooking(booking);

                // Recalcular DVV
                var repo = global::DAL.Factory.Factory.Current.GetBookingRepository();
                var bookings = repo.GetAll();
                new DVVService().RecalcularDVV(bookings, "Bookings");

                LoggerService.Log($"Reserva {idBooking} modificada correctamente.", EventLevel.Informational, CurrentUser);
            }
            catch (BusinessException ex)
            {
                LoggerService.Log($"Error de negocio al modificar reserva {idBooking}: {ex.Message}", EventLevel.Warning, CurrentUser);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error inesperado al modificar reserva {idBooking}: {ex}", EventLevel.Error, CurrentUser);
                throw;
            }
        }

        #endregion

        #region Búsquedas generales

        /// <summary>
        /// Obtiene reservas filtradas por documento y fechas de registro / reserva.
        /// </summary>
        public List<Booking> GetAll(int? nroDocumento, DateTime? registrationBooking, DateTime? registrationDate)
        {
            LoggerService.Log("Inicio búsqueda de reservas.", EventLevel.Informational, CurrentUser);

            try
            {
                var result = _bookingService.GetAll(nroDocumento, registrationBooking, registrationDate);
                LoggerService.Log($"Fin búsqueda de reservas. Resultados: {result.Count}.", EventLevel.Informational, CurrentUser);
                return result;
            }
            catch (BusinessException ex)
            {
                LoggerService.Log($"Error de negocio al buscar reservas: {ex.Message}", EventLevel.Warning, CurrentUser);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error inesperado al buscar reservas: {ex}", EventLevel.Error, CurrentUser);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las reservas dentro de un rango de fechas (RegistrationBooking).
        /// </summary>
        public List<Booking> GetBookingsByDateRange(DateTime? from, DateTime? to)
        {
            LoggerService.Log("Inicio búsqueda de reservas por rango de fechas.", EventLevel.Informational, CurrentUser);

            try
            {
                var result = _bookingService.GetBookingsByDateRange(from, to).ToList();
                LoggerService.Log($"Fin búsqueda de reservas por rango de fechas. Resultados: {result.Count}.", EventLevel.Informational, CurrentUser);
                return result;
            }
            catch (BusinessException ex)
            {
                LoggerService.Log($"Error de negocio al buscar reservas por rango de fechas: {ex.Message}", EventLevel.Warning, CurrentUser);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error inesperado al buscar reservas por rango de fechas: {ex}", EventLevel.Error, CurrentUser);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las reservas de una cancha para una fecha específica.
        /// Se usa para calcular la disponibilidad horaria.
        /// </summary>
        public List<Booking> GetBookingsByFieldAndDate(Guid fieldId, DateTime bookingDate)
        {
            LoggerService.Log(
                $"Inicio búsqueda de reservas por cancha {fieldId} y fecha {bookingDate:yyyy-MM-dd}.",
                EventLevel.Informational,
                CurrentUser);

            try
            {
                var result = _bookingService.GetBookingsByFieldAndDate(fieldId, bookingDate);
                LoggerService.Log(
                    $"Fin búsqueda de reservas por cancha y fecha. Resultados: {result.Count}.",
                    EventLevel.Informational,
                    CurrentUser);

                return result;
            }
            catch (BusinessException ex)
            {
                LoggerService.Log(
                    $"Error de negocio al obtener reservas por cancha y fecha: {ex.Message}",
                    EventLevel.Warning,
                    CurrentUser);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado al obtener reservas por cancha y fecha: {ex}",
                    EventLevel.Error,
                    CurrentUser);
                throw;
            }
        }

        #endregion

        #region Cancelaciones

        /// <summary>
        /// Obtiene reservas canceladas por datos de cliente.
        /// </summary>
        public List<Booking> GetCanceledBookings(int? nroDocumento, string firstName, string lastName, string telephone, string mail)
        {
            LoggerService.Log("Inicio búsqueda de reservas canceladas.", EventLevel.Informational, CurrentUser);

            try
            {
                // Estado cancelado = 3
                var result = _bookingService.GetAllRep(nroDocumento, firstName, lastName, telephone, mail, 3);
                LoggerService.Log(
                    $"Fin búsqueda de reservas canceladas. Resultados: {result.Count}.",
                    EventLevel.Informational,
                    CurrentUser);

                return result;
            }
            catch (BusinessException ex)
            {
                LoggerService.Log($"Error de negocio al buscar cancelaciones: {ex.Message}", EventLevel.Warning, CurrentUser);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error inesperado al buscar cancelaciones: {ex}", EventLevel.Error, CurrentUser);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las reservas canceladas en un rango de fechas.
        /// </summary>
        public List<Booking> GetCanceledBookingsByDateRange(DateTime? from, DateTime? to)
        {
            LoggerService.Log("Inicio búsqueda de cancelaciones por fecha.", EventLevel.Informational, CurrentUser);

            try
            {
                var result = _bookingService.GetAllByDateRangeAndState(from, to, 3); // 3 = Cancelado
                LoggerService.Log(
                    $"Fin búsqueda de cancelaciones por fecha. Resultados: {result.Count}.",
                    EventLevel.Informational,
                    CurrentUser);

                return result;
            }
            catch (BusinessException ex)
            {
                LoggerService.Log(
                    $"Error de negocio en búsqueda de cancelaciones por fecha: {ex.Message}",
                    EventLevel.Warning,
                    CurrentUser);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado en búsqueda de cancelaciones por fecha: {ex}",
                    EventLevel.Error,
                    CurrentUser);
                throw;
            }
        }

        #endregion

        #region Cálculo de importe

        /// <summary>
        /// Calcula el importe de la reserva en base a cancha, horario y promoción.
        /// </summary>
        public decimal CalcularImporteReserva(Guid idField, TimeSpan startTime, TimeSpan endTime, Guid idPromotion)
        {
            LoggerService.Log("Inicio cálculo de importe de la reserva.", EventLevel.Informational, CurrentUser);

            try
            {
                var importe = _bookingService.CalcularImporteReserva(idField, startTime, endTime, idPromotion);

                LoggerService.Log(
                    $"Fin cálculo de importe de la reserva. Importe resultante: {importe}.",
                    EventLevel.Informational,
                    CurrentUser);

                return importe;
            }
            catch (BusinessException ex)
            {
                LoggerService.Log($"Error de negocio al calcular importe: {ex.Message}", EventLevel.Warning, CurrentUser);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error inesperado al calcular importe: {ex}", EventLevel.Error, CurrentUser);
                throw;
            }
        }

        #endregion
    }
}
