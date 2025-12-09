using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa un pago realizado dentro del sistema SIGACF.
    /// Contiene información sobre la reserva asociada, método de pago,
    /// monto, estado y otros datos relevantes para la gestión financiera.
    /// </summary>
    public class Pay
    {
        /// <summary>
        /// Identificador único del pago.
        /// </summary>
        public int IdPay { get; set; }

        /// <summary>
        /// Identificador de la reserva asociada al pago.
        /// </summary>
        public Guid IdBooking { get; set; }

        /// <summary>
        /// Número de documento del cliente que realizó el pago.
        /// </summary>
        public int NroDocument { get; set; }

        /// <summary>
        /// Fecha en la cual se efectuó el pago.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Identificador del método de pago utilizado.
        /// </summary>
        public int MethodPay { get; set; }

        /// <summary>
        /// Descripción del método de pago utilizado.
        /// </summary>
        public string PaymentMethodDescription { get; set; }

        /// <summary>
        /// Monto total del pago realizado.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Estado actual del pago.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Descripción del estado del pago.
        /// </summary>
        public string StateDescription { get; set; }
    }
}
