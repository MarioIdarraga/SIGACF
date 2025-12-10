using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Excepción utilizada cuando se intenta operar sobre un cliente
    /// que no existe en el sistema.
    /// </summary>
    public class ClienteInexistenteException : BusinessException
    {
        /// <summary>
        /// Crea una nueva instancia indicando que el cliente solicitado no existe.
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error.</param>
        public ClienteInexistenteException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Crea una nueva instancia indicando que el cliente solicitado no existe,
        /// incluyendo la excepción interna que originó el error.
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error.</param>
        /// <param name="innerException">Excepción que causó el problema.</param>
        public ClienteInexistenteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
