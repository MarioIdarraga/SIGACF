using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa un método de pago disponible dentro del sistema SIGACF.
    /// Incluye el identificador y la descripción del método.
    /// </summary>
    public class PaymentMethod
    {
        /// <summary>
        /// Identificador único del método de pago.
        /// </summary>
        public int IdPayMethod { get; set; }

        /// <summary>
        /// Descripción textual del método de pago.
        /// </summary>
        public string Description { get; set; }
    }
}
