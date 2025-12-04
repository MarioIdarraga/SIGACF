using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Se lanza cuando un usuario ha superado los intentos permitidos
    /// y su cuenta se encuentra bloqueada.
    /// </summary>
    public class UsuarioBloqueadoException : BusinessException
    {
        public UsuarioBloqueadoException(string message)
            : base(message) { }

        public UsuarioBloqueadoException(string message, Exception innerException)
            : base(message, innerException) { }
    }

}
