using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Excepción que representa un error al acceder a la base de datos.
    /// Se utiliza cuando ocurre un problema de conexión, consulta o ejecución
    /// durante operaciones de persistencia.
    /// </summary>
    public class DatabaseAccessException : BusinessException
    {
        /// <summary>
        /// Crea una nueva instancia indicando que ocurrió un error
        /// al intentar acceder a la base de datos.
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error.</param>
        public DatabaseAccessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Crea una nueva instancia indicando que ocurrió un error
        /// al acceder a la base de datos, incluyendo la excepción interna.
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error.</param>
        /// <param name="innerException">Excepción original que causó el problema.</param>
        public DatabaseAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

