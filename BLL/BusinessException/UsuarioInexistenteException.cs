using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Excepción que se lanza cuando el usuario solicitado no existe
    /// en el sistema o no puede ser encontrado.
    /// </summary>
    public class UsuarioInexistenteException : BusinessException
    {
        public UsuarioInexistenteException(string message)
            : base(message) { }

        public UsuarioInexistenteException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
