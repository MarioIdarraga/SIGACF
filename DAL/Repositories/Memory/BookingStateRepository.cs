using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    class BookingStateRepository : IGenericRepository<BookingState>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingState> GetAll()
        {
            throw new NotImplementedException();
        }

        public BookingState GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(BookingState Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, BookingState Object)
        {
            throw new NotImplementedException();
        }
    }
}
