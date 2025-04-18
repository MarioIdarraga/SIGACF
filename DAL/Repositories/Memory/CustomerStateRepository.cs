using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    class CustomerStateRepository : IGenericRepository<CustomerState>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerState> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public CustomerState GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(CustomerState Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, CustomerState Object)
        {
            throw new NotImplementedException();
        }
    }
}
