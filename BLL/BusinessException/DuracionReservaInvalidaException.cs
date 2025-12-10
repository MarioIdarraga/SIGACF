using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Excepción que se lanza cuando la duración de una reserva 
    /// no cumple con las reglas de negocio establecidas.
    /// </summary>
    public class DuracionReservaInvalidaException : BusinessException
    {
        public DuracionReservaInvalidaException(string message)
            : base(message) { }
    }
}
