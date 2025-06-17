using System;
using System.Diagnostics.Tracing;
using BLL.Service;
using Domain;

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
    }
}

