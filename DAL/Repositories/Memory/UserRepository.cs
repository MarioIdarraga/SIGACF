using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    internal class UserRepository : IUserRepository<User>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }
        public User GetByLoginName(string loginName)
        {
            throw new NotImplementedException();
        }

        public User GetByPasswordResetToken(string token)
        {
            throw new NotImplementedException();
        }

        public User GetByUsernameOrEmail(string userOrMail)
        {
            throw new NotImplementedException();
        }

        public User GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }
        public void Insert(User Object)
        {
            throw new NotImplementedException();
        }

        public void SavePasswordResetToken(Guid userId, string token, DateTime expiration)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, User Object)
        {
            throw new NotImplementedException();
        }
    }
}
