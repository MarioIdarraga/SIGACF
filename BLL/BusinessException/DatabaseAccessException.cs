using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    public class DatabaseAccessException : BusinessException
    {
        public DatabaseAccessException(string message)
            : base(message)
        {
        }

        public DatabaseAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
