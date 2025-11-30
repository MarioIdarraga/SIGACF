using DAL.Contracts;
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
    /// Service Layer para consultar los métodos de pago disponibles.
    /// Centraliza el acceso al repositorio, aplica logging y validaciones.
    /// </summary>
    public class PaymentMethodSLService
    {
        /// <summary>
        /// Devuelve el login del usuario actual para dejar trazas en el log.
        /// </summary>
        private string CurrentUser => Session.CurrentUser?.LoginName;

        /// <summary>
        /// Obtiene todos los métodos de pago habilitados en el sistema.
        /// </summary>
        public IEnumerable<PaymentMethod> GetAll()
        {
            LoggerService.Log(
                "Inicio carga de métodos de pago.", EventLevel.Informational, CurrentUser);
            try
            {
                var repo = global::DAL.Factory.Factory.Current.GetPaymentMethodRepository();
                var list = repo.GetAll();

                LoggerService.Log(
                    $"Fin carga de métodos de pago. Total: {list?.Count() ?? 0}.", EventLevel.Informational, CurrentUser);
                return list;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al obtener métodos de pago: {ex.Message}", EventLevel.Error, CurrentUser);
                throw new Exception("No se pudieron cargar los métodos de pago.", ex);
            }
        }
    }
}
