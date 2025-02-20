using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;
using DAL.Tools;

namespace DAL.Repositories.Memory
{
    internal class CustomerRepository : IGenericRepository<Customer>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer() { IdCustomer = Guid.NewGuid(), NroDocument = 19113731, FirstName = "Mario", LastName = "Idarraga", Mail = "Mario@gmail.com", Address = "Balcarce 800", Telephone = "+5491164377169", Comment = "Le gusta jugar los lunes", State = 1 });
                return customers;
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
    }
}
