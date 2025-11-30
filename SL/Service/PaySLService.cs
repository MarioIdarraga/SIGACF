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
    /// Service Layer para operaciones de pagos (Pay).
    /// Centraliza el acceso al PayService e incorpora logging.
    /// </summary>
    public class PaySLService
    {
        private readonly PayService _payService;

        /// <summary>
        /// Crea una nueva instancia del servicio de pagos en la capa SL.
        /// </summary>
        public PaySLService(PayService payService)
        {
            _payService = payService ?? throw new ArgumentNullException(nameof(payService));
        }

        /// <summary>
        /// Devuelve el login del usuario actual para dejar trazas en el log.
        /// </summary>
        private string CurrentUser => Session.CurrentUser?.LoginName;

        /// <summary>
        /// Inserta un pago en la base de datos aplicando logging y validaciones.
        /// </summary>
        public void Insert(Pay pay)
        {
            LoggerService.Log(
                "Inicio de registro de pago.",
                EventLevel.Informational,
                CurrentUser);

            try
            {
                if (pay == null)
                    throw new ArgumentNullException(nameof(pay));

                // Insertar pago usando el repositorio
                var repo = global::DAL.Factory.Factory.Current.GetPayRepository();
                repo.Insert(pay);

                LoggerService.Log(
                    "Pago registrado correctamente.",
                    EventLevel.Informational,
                    CurrentUser);
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al registrar pago: {ex.Message}",
                    EventLevel.Error,
                    CurrentUser);

                throw;
            }
        }

        /// <summary>
        /// Obtiene pagos filtrados por documento, fecha y método de pago.
        /// </summary>
        /// <summary>
        /// Obtiene pagos filtrados por documento, fecha desde/hasta y método de pago.
        /// </summary>
        public IEnumerable<Pay> GetAll(int? nroDocument, DateTime? dateFrom, DateTime? dateTo, int? method)
        {
            LoggerService.Log("Inicio búsqueda de pagos.", EventLevel.Informational, CurrentUser);

            try
            {

                var result = _payService.GetPayments(dateFrom, dateTo).ToList();

                // Filtro por documento
                if (nroDocument.HasValue)
                    result = result.Where(p => p.NroDocument == nroDocument.Value).ToList();

                // Filtro por método de pago
                if (method.HasValue)
                    result = result.Where(p => p.MethodPay == method.Value).ToList();

                LoggerService.Log(
                    $"Fin búsqueda de pagos. Resultados: {result.Count}.",
                    EventLevel.Informational,
                    CurrentUser);

                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar pagos: {ex.Message}",
                    EventLevel.Error,
                    CurrentUser);

                throw new Exception("No se pudieron obtener los pagos.", ex);
            }
        }
    }
}

