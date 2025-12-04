using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    public class UsuarioInexistenteException : BusinessException
    {
        public UsuarioInexistenteException(string message)
            : base(message) { }

        public UsuarioInexistenteException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
