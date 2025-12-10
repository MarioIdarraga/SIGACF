using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Representa una excepción controlada del negocio.
    /// Se utiliza para diferenciar errores esperados (reglas de negocio)
    /// de errores inesperados del sistema.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Crea una nueva instancia de BusinessException con un mensaje descriptivo.
        /// </summary>
        /// <param name="message">Mensaje que describe el error de negocio.</param>
        public BusinessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Crea una nueva instancia de BusinessException con un mensaje 
        /// y una excepción interna que originó el problema.
        /// </summary>
        /// <param name="message">Mensaje que describe el error de negocio.</param>
        /// <param name="innerException">Excepción original que causó el error.</param>
        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}