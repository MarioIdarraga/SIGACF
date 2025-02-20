using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    internal class BookingRepository : IGenericRepository<Booking>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAll()
        {
            throw new NotImplementedException();
        }

        public Booking GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Booking Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Booking Object)
        {
            throw new NotImplementedException();
        }
    }
}
