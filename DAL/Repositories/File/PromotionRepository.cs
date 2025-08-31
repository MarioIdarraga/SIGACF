using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    class PromotionRepository : IGenericRepository<Promotion>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            throw new NotImplementedException();
        }

        public Promotion GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Promotion Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Promotion Object)
        {
            throw new NotImplementedException();
        }
    }
}
