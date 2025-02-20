using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;
using DAL.Tools;

namespace DAL.Repositories.File
{
    internal class CustomerRepository : IGenericRepository<Customer>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Customer Object)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Customer> IGenericRepository<Customer>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
