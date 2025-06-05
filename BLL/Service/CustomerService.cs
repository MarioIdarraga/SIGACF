using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using Domain;

namespace BLL.Service
{
    internal class CustomerService : IGenericBusinessService<Customer>
    {
        public void Delete(Guid obj)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
