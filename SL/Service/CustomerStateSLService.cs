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
    /// Service Layer para gestionar los estados de cliente (CustomerStates).
    /// Encapsula la lógica de logging y el acceso a la capa BLL.
    /// </summary>
    public class CustomerStateSLService
    {
        private readonly CustomerStateService _customerStateService;

        /// <summary>
        /// Crea una nueva instancia de <see cref="CustomerStateSLService"/>.
        /// </summary>
        /// <param name="customerStateService">
        /// Servicio de negocio asociado a los estados de cliente.
        /// </param>
        public CustomerStateSLService(CustomerStateService customerStateService)
        {
            _customerStateService = customerStateService;
        }

        /// <summary>
        /// Obtiene todos los estados de cliente.
        /// </summary>
        /// <returns>
        /// Lista de objetos <see cref="CustomerState"/> con todos los estados.
        /// </returns>
        public List<CustomerState> GetAll()
        {
            LoggerService.Log(
                "Inicio obtención de estados de cliente.",
                EventLevel.Informational,
                Session.CurrentUser?.LoginName);

            try
            {
                var result = _customerStateService.GetAll();

                LoggerService.Log(
                    $"Fin obtención de estados de cliente. Resultados: {result.Count}",
                    EventLevel.Informational,
                    Session.CurrentUser?.LoginName);

                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al obtener estados de cliente: {ex.Message}",
                    EventLevel.Error,
                    Session.CurrentUser?.LoginName);

                throw;
            }
        }

        /// <summary>
        /// Devuelve un diccionario para UI donde la clave es el IdCustomerState
        /// y el valor es la descripción del estado.
        /// </summary>
        /// <returns>
        /// Diccionario clave → IdCustomerState, valor → Description.
        /// </returns>
        public IDictionary<int, string> GetStatesLookup()
        {
            LoggerService.Log(
                "Inicio carga de lookup de estados de cliente.",
                EventLevel.Informational,
                Session.CurrentUser?.LoginName);

            try
            {
                var states = GetAll(); 

                var lookup = states.ToDictionary(
                    s => s.IdCustomerState,
                    s => s.Description);

                LoggerService.Log(
                    $"Fin carga lookup de estados de cliente. Cantidad: {lookup.Count}",
                    EventLevel.Informational,
                    Session.CurrentUser?.LoginName);

                return lookup;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al cargar lookup de estados de cliente: {ex.Message}",
                    EventLevel.Error,
                    Session.CurrentUser?.LoginName);

                throw;
            }
        }
    }
}
