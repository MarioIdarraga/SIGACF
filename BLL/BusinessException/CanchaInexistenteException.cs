using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    public class CanchaInexistenteException : BusinessException
    {
        public CanchaInexistenteException(string message)
            : base(message) { }
    }
}
