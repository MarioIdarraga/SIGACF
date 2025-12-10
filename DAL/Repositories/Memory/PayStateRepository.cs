using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para la entidad PayState.
    /// Devuelve la lista fija de estados de pago usada por el sistema.
    /// </summary>
    internal class PayStateRepository : IPayStateRepository
    {
        /// <summary>
        /// Obtiene todos los estados de pago disponibles en memoria.
        /// </summary>
        /// <returns>Lista de estados de pago.</returns>
        public IEnumerable<PayState> GetAll()
        {
            return new List<PayState>
            {
                new PayState { IdPayState = 1, Description = "Pagado" },
                new PayState { IdPayState = 2, Description = "Pendiente" },
                new PayState { IdPayState = 3, Description = "Anulado" }
            };
        }
    }
}
