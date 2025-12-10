using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Excepción utilizada cuando un usuario debe cambiar su contraseña
    /// antes de poder continuar usando el sistema.
    /// </summary>
    public class CambioPasswordRequeridoException : BusinessException
    {
        /// <summary>
        /// Crea una nueva instancia indicando que se requiere cambio de contraseña.
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error.</param>
        public CambioPasswordRequeridoException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Crea una nueva instancia indicando que se requiere cambio de contraseña,
        /// incluyendo la excepción interna que originó el error.
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error.</param>
        /// <param name="innerException">Excepción que causó el problema.</param>
        public CambioPasswordRequeridoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
