using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BusinessException
{
    /// <summary>
    /// Excepción que se lanza cuando se intenta registrar una reserva
    /// que ya existe para el mismo cliente, fecha u horario.
    /// </summary>
    public class ReservaDuplicadaException : BusinessException
    {
        public ReservaDuplicadaException(string message)
            : base(message) { }
    }
}
