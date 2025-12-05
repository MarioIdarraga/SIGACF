using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para métodos de pago.
    /// </summary>
    internal class PaymentMethodRepository : IPaymentMethodRepository
    {
        /// <summary>
        /// Métodos de pago precargados según la base de datos real.
        /// </summary>
        private static readonly List<PaymentMethod> _methods = new List<PaymentMethod>
        {
            new PaymentMethod { IdPayMethod = 1, Description = "Efectivo" },
            new PaymentMethod { IdPayMethod = 2, Description = "Transferencia Bancaria" },
            new PaymentMethod { IdPayMethod = 3, Description = "Tarjeta de Débito" },
            new PaymentMethod { IdPayMethod = 4, Description = "Tarjeta de Crédito" },
            new PaymentMethod { IdPayMethod = 5, Description = "Mercado Pago" }
        };

        /// <summary>
        /// Obtiene todos los métodos de pago.
        /// </summary>
        public IEnumerable<PaymentMethod> GetAll()
        {
            try
            {
                return _methods.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los métodos de pago en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene un método de pago por su identificador.
        /// </summary>
        public PaymentMethod GetOne(int id)
        {
            try
            {
                return _methods.FirstOrDefault(x => x.IdPayMethod == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el método de pago en memoria.", ex);
            }
        }
    }
}
