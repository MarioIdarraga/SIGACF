using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{

    /// <summary>
    /// Contrato para acceder a los estados de pago.
    /// </summary>
    public interface IPayStateRepository
    {
        IEnumerable<PayState> GetAll();
    }
}
