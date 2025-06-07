using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    internal class PayRepository : IPayRepository

    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pay> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pay> GetAll(DateTime? registrationSincePay, DateTime? registrationUntilPay)
        {
            throw new NotImplementedException();
        }

        public Pay GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Pay obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Pay obj)
        {
            throw new NotImplementedException();
        }
    }
}
