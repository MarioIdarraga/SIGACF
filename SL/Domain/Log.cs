using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Domain
{
    /// <summary>
    /// Representa una entrada de log dentro del sistema SIGACF.
    /// Contiene información sobre el mensaje registrado, su severidad
    /// y el usuario que realizó la acción.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Mensaje descriptivo del evento registrado.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Nivel de severidad asociado al evento registrado.
        /// </summary>
        public Severity severity { get; set; }

        /// <summary>
        /// Identifica al usuario que realizó la acción registrada en el log.
        /// </summary>
        public string PerformedBy { get; set; }

        /// <summary>
        /// Representa los niveles de severidad para un evento en el log.
        /// </summary>
        public enum Severity
        {
            /// <summary>
            /// Indica advertencias o situaciones no críticas.
            /// </summary>
            Warning,

            /// <summary>
            /// Indica errores que afectan la operación pero permiten continuar.
            /// </summary>
            Error,

            /// <summary>
            /// Indica errores graves que pueden requerir intervención.
            /// </summary>
            CriticalError,

            /// <summary>
            /// Indica errores fatales que impiden continuar la ejecución.
            /// </summary>
            FatalError
        }
    }
}
