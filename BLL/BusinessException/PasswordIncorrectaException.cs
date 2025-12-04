using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    public class PasswordIncorrectaException : BusinessException
    {
        public PasswordIncorrectaException(string message)
            : base(message) { }

        public PasswordIncorrectaException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
