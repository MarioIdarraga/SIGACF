using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using BLL.Service;
using Domain;
using SL.Helpers;
using SL.Services;

namespace SL
{
    /// <summary>
    /// Service Layer para gestionar los estados de reserva (BookingsStates).
    /// Encapsula la lógica de logging y el acceso a la capa BLL.
    /// </summary>
    public class BookingStateSLService
    {
        private readonly BookingStateService _bookingStateService;

        /// <summary>
        /// Crea una nueva instancia de <see cref="BookingStateSLService"/>.
        /// </summary>
        /// <param name="bookingStateService">
        /// Servicio de negocio para estados de reserva.
        /// </param>
        public BookingStateSLService(BookingStateService bookingStateService)
        {
            _bookingStateService = bookingStateService;
        }

        /// <summary>
        /// Obtiene todos los estados de reserva.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="BookingState"/> con todos los estados.
        /// </returns>
        public List<BookingState> GetAll()
        {
            LoggerService.Log(
                "Inicio obtención de estados de reserva.",
                EventLevel.Informational,
                Session.CurrentUser?.LoginName);

            try
            {
                var result = _bookingStateService.GetAll();

                LoggerService.Log(
                    $"Fin obtención de estados de reserva. Resultados: {result.Count}",
                    EventLevel.Informational,
                    Session.CurrentUser?.LoginName);

                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al obtener estados de reserva: {ex.Message}",
                    EventLevel.Error,
                    Session.CurrentUser?.LoginName);

                throw;
            }
        }

        /// <summary>
        /// Devuelve un diccionario de ayuda para UI con
        /// IdStateBooking como clave y Description como valor.
        /// </summary>
        /// <returns>
        /// Diccionario donde la clave es el Id del estado y el valor su descripción.
        /// </returns>
        public IDictionary<int, string> GetStatesLookup()
        {
            LoggerService.Log(
                "Inicio carga de lookup de estados de reserva.",
                EventLevel.Informational,
                Session.CurrentUser?.LoginName);

            try
            {
                var states = GetAll(); // ya loguea internamente
                var lookup = states.ToDictionary(s => s.IdStateBooking, s => s.Description);

                LoggerService.Log(
                    $"Fin carga lookup de estados de reserva. Cantidad: {lookup.Count}",
                    EventLevel.Informational,
                    Session.CurrentUser?.LoginName);

                return lookup;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al cargar lookup de estados de reserva: {ex.Message}",
                    EventLevel.Error,
                    Session.CurrentUser?.LoginName);

                throw;
            }
        }
    }
}
