using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.File
{
    internal class PaymentMethodRepository : IPaymentMethodRepository
    {
        public IEnumerable<PaymentMethod> GetAll()
        {
            throw new NotImplementedException();
        }

        public PaymentMethod GetOne(int id)
        {
            throw new NotImplementedException();
        }
    }
}
