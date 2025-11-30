using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPaymentMethodRepository
    {
        IEnumerable<PaymentMethod> GetAll();
        PaymentMethod GetOne(int id);
    }
}
