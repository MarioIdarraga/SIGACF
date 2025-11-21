using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    class FieldStateRepository : IGenericRepository<FieldState>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldState> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldState> GetAll(DateTime? from, DateTime? to, int state)
        {
            throw new NotImplementedException();
        }

        public FieldState GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(FieldState Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, FieldState Object)
        {
            throw new NotImplementedException();
        }
    }
}
