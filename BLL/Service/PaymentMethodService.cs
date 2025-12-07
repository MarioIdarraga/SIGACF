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
    /// Lógica de negocio para métodos de pago.
    /// </summary>
    public class PaymentMethodService
    {
        private readonly IPaymentMethodRepository _repo;

        public PaymentMethodService(IPaymentMethodRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Obtiene todos los métodos de pago.
        /// </summary>
        public List<PaymentMethod> GetAll()
        {
            try
            {
                return new List<PaymentMethod>(_repo.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception("Error obteniendo métodos de pago.", ex);
            }
        }

        /// <summary>
        /// Obtiene un método de pago por ID.
        /// </summary>
        public PaymentMethod GetOne(int id)
        {
            try
            {
                return _repo.GetOne(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error obteniendo método de pago.", ex);
            }
        }
    }
}

