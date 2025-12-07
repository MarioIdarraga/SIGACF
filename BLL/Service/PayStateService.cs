using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    /// <summary>
    /// Servicio de negocio para obtener estados de pago.
    /// </summary>
    public class PayStateService
    {
        private readonly IPayStateRepository _repository;

        public PayStateService(IPayStateRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Devuelve todos los estados de pago.
        /// </summary>
        public List<PayState> GetAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}