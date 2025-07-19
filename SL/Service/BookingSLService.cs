using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using BLL.Service;
using Domain;
using SL.Helpers;

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
            LoggerService.Log("Inicio de registro de reserva.");

            try
            {
                _bookingService.RegisterBooking(booking);
                LoggerService.Log("Reserva registrada correctamente.");
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar reserva: {ex.Message}", EventLevel.Error);
                throw;
            }
        }

        public void Update(Guid idBooking, Booking booking)
        {
            LoggerService.Log("Inicio de modificación de reserva.");

            try
            {
                _bookingService.UpdateBooking(booking);
                LoggerService.Log("Reserva modificada correctamente.");
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar reserva: {ex.Message}", EventLevel.Error);
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

    }
}

