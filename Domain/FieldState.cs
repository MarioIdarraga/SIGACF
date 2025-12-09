using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa el estado actual de una cancha dentro del sistema SIGACF.
    /// Incluye el identificador del estado y su descripción correspondiente.
    /// </summary>
    public class FieldState
    {
        /// <summary>
        /// Identificador único del estado de la cancha.
        /// </summary>
        public int IdFieldState { get; set; }

        /// <summary>
        /// Descripción textual del estado de la cancha.
        /// </summary>
        public string FieldStateDescription { get; set; }
    }
}
