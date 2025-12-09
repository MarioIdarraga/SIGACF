using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa el estado de un pago dentro del sistema SIGACF.
    /// Incluye el identificador y una descripción del estado.
    /// </summary>
    public class PayState
    {
        /// <summary>
        /// Identificador único del estado del pago.
        /// </summary>
        public int IdPayState { get; set; }

        /// <summary>
        /// Descripción textual del estado del pago.
        /// </summary>
        public string Description { get; set; }
    }
}
