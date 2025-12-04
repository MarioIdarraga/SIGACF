using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    public class CambioPasswordRequeridoException : BusinessException
    {
        public CambioPasswordRequeridoException(string message)
            : base(message) { }

        public CambioPasswordRequeridoException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
