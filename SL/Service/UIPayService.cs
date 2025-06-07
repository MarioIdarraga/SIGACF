using System;
using System.Diagnostics.Tracing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Service;
using Domain;
using SL.BLL;
using SL.Domain;
using SL.Services;


namespace SL.Services
{
    public class UIPayService
    {
        private readonly PayService _payService;

        public UIPayService(PayService payService)
        {
            _payService = payService;
        }

        public IEnumerable<Pay> GetFilteredPayments(DateTime? since, DateTime? until)
        {
            try
            {
                LoggerService.Log($"Inicio búsqueda de pagos desde {since} hasta {until}", EventLevel.Informational);
                var result = _payService.GetPayments(since, until);
                LoggerService.Log($"Se encontraron {result.Count()} pagos", EventLevel.Informational);
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"[ERROR] al obtener pagos: {ex.Message}", EventLevel.Error);
                throw;
            }
        }
    }
}
