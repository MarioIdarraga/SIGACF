using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Excepción que se lanza cuando la contraseña ingresada 
    /// no coincide con la registrada para el usuario.
    /// </summary>
    public class PasswordIncorrectaException : BusinessException
    {
        public PasswordIncorrectaException(string message)
            : base(message) { }

        public PasswordIncorrectaException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
