using BLL.Service;
using Domain;
using SL.Helpers;
using SL.Service;
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
        private readonly PaymentMethodSLService _paymentMethodSLService;

        /// <summary>
        /// Crea una nueva instancia del servicio de pagos en la capa SL y la de Métodos de Pago.
        /// </summary>
        public PaySLService(PayService payService, PaymentMethodSLService paymentMethodSLService)
        {
            _payService = payService;
            _paymentMethodSLService = paymentMethodSLService;
        }

        /// <summary>
        /// Devuelve el login del usuario actual para dejar trazas en el log.
        /// </summary>
        private string CurrentUser => Session.CurrentUser?.LoginName;

        /// <summary>
        /// Inserta un pago aplicando logging y validaciones.
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
        /// Obtiene pagos filtrados por documento, fecha desde/hasta y método de pago.
        /// Asigna la descripción del método de pago y del estado del pago.
        /// </summary>
        public IEnumerable<Pay> GetAll(int? nroDocument, DateTime? dateFrom, DateTime? dateTo, int? method)
        {
            LoggerService.Log("Inicio búsqueda de pagos.", EventLevel.Informational, CurrentUser);

            try
            {
                var result = _payService.GetPayments(dateFrom, dateTo).ToList();

                // Filtrar por documento
                if (nroDocument.HasValue)
                    result = result.Where(p => p.NroDocument == nroDocument.Value).ToList();

                // Filtrar por método de pago
                if (method.HasValue)
                    result = result.Where(p => p.MethodPay == method.Value).ToList();

                // --- Catálogo de métodos de pago ---
                var methods = _paymentMethodSLService.GetAll();

                // --- Catálogo de estados de pago ---
                var payStateRepo = global::DAL.Factory.Factory.Current.GetPayStateRepository();
                var payStateBLL = new PayStateService(payStateRepo);
                var payStateSL = new PayStateSLService(payStateBLL);

                foreach (var pay in result)
                {
                    // Descripción del método de pago
                    pay.PaymentMethodDescription =
                        methods.FirstOrDefault(m => m.IdPayMethod == pay.MethodPay)?.Description
                        ?? "Desconocido";

                    // Descripción del estado del pago
                    pay.StateDescription =
                        payStateSL.GetDescription(pay.State);
                }

                LoggerService.Log(
                    $"Fin búsqueda de pagos. Resultados: {result.Count}.",
                    EventLevel.Informational,
                    CurrentUser);

                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al buscar pagos: {ex.Message}",
                    EventLevel.Error,
                    CurrentUser);

                throw new Exception("No se pudieron obtener los pagos.", ex);
            }
        }
    }
}


