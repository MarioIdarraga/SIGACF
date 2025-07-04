﻿using System;
using DAL.Contracts;
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
            ValidateBooking(booking);
            _bookingRepo.Insert(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.IdBooking == Guid.Empty)
                throw new ArgumentException("El ID de la reserva es obligatorio.");

            ValidateBooking(booking);

            var existing = _bookingRepo.GetOne(booking.IdBooking);
            if (existing == null)
                throw new InvalidOperationException("No se encontró la reserva a actualizar.");

            _bookingRepo.Update(booking.IdBooking, booking);
        }

        private void ValidateBooking(Booking booking)
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
    }
}

