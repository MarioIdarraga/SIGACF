using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    /// <summary>
    /// Representa el estado de un pago.
    /// </summary>
    public class PayState
    {
        public int IdPayState { get; set; }
        public string Description { get; set; }
    }
}
