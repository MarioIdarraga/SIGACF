using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Contracts
{
    public interface IGenericRepository<T> //interfaz generica
    {
        void Insert(T Object);

        void Update(Guid Id, T Object);

        void Delete(Guid Id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate);

        T GetOne(Guid Id);

        IEnumerable<T> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state);

        IEnumerable<T> GetAll(DateTime? from, DateTime? to, int state);
    }
}

