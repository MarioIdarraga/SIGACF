using BLL.Service;
using SL.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Service
{
    /// <summary>
    /// Capa SL para manejar la obtención y descripción de los estados de pago.
    /// </summary>
    public class PayStateSLService
    {
        private readonly PayStateService _payStateService;

        /// <summary>
        /// Inicializa una nueva instancia del servicio SL para estados de pago.
        /// </summary>
        public PayStateSLService(PayStateService service)
        {
            _payStateService = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Usuario actual para dejar trazas en el log.
        /// </summary>
        private string CurrentUser => Session.CurrentUser?.LoginName;

        /// <summary>
        /// Devuelve la descripción del estado de pago correspondiente al ID indicado.
        /// </summary>
        public string GetDescription(int idState)
        {
            LoggerService.Log(
                $"Inicio obtención de descripción de estado de pago {idState}.",
                EventLevel.Informational,
                CurrentUser);

            try
            {
                var list = _payStateService.GetAll();
                var description = list.FirstOrDefault(s => s.IdPayState == idState)?.Description
                                  ?? "Desconocido";

                LoggerService.Log(
                    $"Fin obtención de descripción de estado de pago {idState}. Resultado: {description}",
                    EventLevel.Informational,
                    CurrentUser);

                return description;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error al obtener descripción de estado de pago {idState}: {ex}",
                    EventLevel.Error,
                    CurrentUser);

                throw;
            }
        }
    }
}
