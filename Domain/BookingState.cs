using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa el estado actual de una reserva dentro del sistema SIGACF.
    /// Incluye el identificador y la descripción correspondiente al estado.
    /// </summary>
    public class BookingState
    {
        /// <summary>
        /// Identificador único del estado de la reserva.
        /// </summary>
        public int IdStateBooking { get; set; }

        /// <summary>
        /// Descripción del estado de la reserva.
        /// </summary>
        public string Description { get; set; }
    }
}
