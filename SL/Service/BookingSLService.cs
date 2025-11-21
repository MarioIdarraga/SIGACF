using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using BLL.Service;
using Domain;
using SL.Helpers;
using SL.Services;

namespace SL
{
    public class BookingSLService
    {
        private readonly BookingService _bookingService;

        public BookingSLService(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void Insert(Booking booking)
        {
            LoggerService.Log("Inicio de registro de reserva.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                // Calcular DVH
                booking.DVH = DVHHelper.CalcularDVH(booking);

                // Insertar reserva
                _bookingService.RegisterBooking(booking);

                // Recalcular DVV
                var repo = global::DAL.Factory.Factory.Current.GetBookingRepository();
                var bookings = repo.GetAll();
                new DVVService().RecalcularDVV(bookings, "Bookings");

                LoggerService.Log("Reserva registrada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar reserva: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public void Update(Guid idBooking, Booking booking)
        {
            LoggerService.Log("Inicio de modificación de reserva.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                // Recalcular DVH
                booking.DVH = DVHHelper.CalcularDVH(booking);

                // Actualizar reserva
                _bookingService.UpdateBooking(booking);

                // Recalcular DVV
                var repo = global::DAL.Factory.Factory.Current.GetBookingRepository();
                var bookings = repo.GetAll();
                new DVVService().RecalcularDVV(bookings, "Bookings");

                LoggerService.Log("Reserva modificada correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar reserva: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public List<Booking> GetAll(int? nroDocumento, DateTime? registrationBooking, DateTime? registrationDate)
        {
            LoggerService.Log("Inicio búsqueda de reservas.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                var result = _bookingService.GetAll(nroDocumento, registrationBooking, registrationDate);

                LoggerService.Log($"Fin búsqueda de reservas. Resultados: {result.Count}", EventLevel.Informational, Session.CurrentUser?.LoginName);
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar reservas: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public decimal CalcularImporteReserva(Guid idField, TimeSpan startTime, TimeSpan endTime, Guid idPromotion)
        {
            LoggerService.Log("Calculando importe de la reserva...");
            try
            {
                return _bookingService.CalcularImporteReserva(idField, startTime, endTime, idPromotion);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al calcular importe: {ex.Message}", EventLevel.Error);
                throw;
            }
        }

        public List<Booking> GetCanceledBookings(int? nroDocumento, string firstName, string lastName, string telephone, string mail)
        {
            LoggerService.Log("Inicio búsqueda de reservas canceladas.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                // Estado cancelado = 3
                var result = _bookingService.GetAllRep(nroDocumento, firstName, lastName, telephone, mail, 3);

                LoggerService.Log($"Fin búsqueda canceladas. Resultados: {result.Count}", EventLevel.Informational, Session.CurrentUser?.LoginName);
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar cancelaciones: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        /// <summary>
        /// Obtiene todas las reservas canceladas en un rango de fechas determinado.
        /// </summary>
        /// <param name="from">Fecha de inicio (opcional)</param>
        /// <param name="to">Fecha de fin (opcional)</param>
        /// <returns>Lista de reservas canceladas</returns>
        public List<Booking> GetCanceledBookingsByDateRange(DateTime? from, DateTime? to)
        {
            LoggerService.Log("Inicio búsqueda de cancelaciones por fecha.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                var result = _bookingService.GetAllByDateRangeAndState(from, to, 3); // 3 = Cancelado
                LoggerService.Log($"Fin búsqueda. Resultados: {result.Count}", EventLevel.Informational, Session.CurrentUser?.LoginName);
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error en búsqueda de cancelaciones: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }
    }
}

