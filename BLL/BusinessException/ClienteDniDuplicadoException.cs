using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Se lanza cuando se intenta registrar un cliente cuyo DNI ya existe en el sistema.
    /// </summary>
    public class ClienteDniDuplicadoException : BusinessException
    {
        public ClienteDniDuplicadoException(string message)
            : base(message)
        {
        }

        public ClienteDniDuplicadoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
