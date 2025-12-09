using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa el estado actual de un cliente dentro del sistema SIGACF.
    /// Incluye el identificador del estado y su descripción correspondiente.
    /// </summary>
    public class CustomerState
    {
        /// <summary>
        /// Identificador único del estado del cliente.
        /// </summary>
        public int IdCustomerState { get; set; }

        /// <summary>
        /// Descripción textual del estado del cliente.
        /// </summary>
        public string Description { get; set; }
    }
}
