using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Contracts
{
    public interface IUserRepository<T>
    {
        void Insert(T Object);
        void Update(Guid Id, T Object);
        void Delete(Guid Id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate);
        T GetOne(Guid Id);
        IEnumerable<T> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail);

        User GetByLoginName(string loginName);

        // Recuperación de contraseña
        User GetByUsernameOrEmail(string userOrMail);
        void SavePasswordResetToken(Guid userId, string token, DateTime expiration);
        User GetByPasswordResetToken(string token);
    }
}
