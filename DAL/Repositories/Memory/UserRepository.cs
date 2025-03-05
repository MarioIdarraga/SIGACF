using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    internal class UserRepository : IGenericRepository<User>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            users.Add(new User() { UserId = Guid.NewGuid(), FirstName = "Mario", LastName = "Idarraga", LoginName = "Batmor", Password = "123456", Email = "Mario.Idarraga@itau.com.ar", Position = "Administrator" });
            users.Add(new User() { UserId = Guid.NewGuid(), FirstName = "Felipe", LastName = "Idarraga", LoginName = "Felipe", Password = "123456", Email = "Felipe.Idarraga@itau.com.ar", Position = "Administrator" });
            users.Add(new User() { UserId = Guid.NewGuid(), FirstName = "ANngelica", LastName = "Barrientos", LoginName = "Angelica", Password = "123456", Email = "Angelica.Barrientos@itau.com.ar", Position = "Administrator" });
            return users;
        }

        public IEnumerable<User> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
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

        public void Update(Guid Id, User Object)
        {
            throw new NotImplementedException();
        }
    }
}
