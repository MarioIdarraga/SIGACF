using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    public class DuracionReservaInvalidaException : BusinessException
    {
        public DuracionReservaInvalidaException(string message)
            : base(message) { }
    }
}
