using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Factory;
using DAL.Repositories.SqlServer;
using Domain;

namespace BLL.Service
{
    public class UserService
    {
        IUserRepository<User> repositoryUser = Factory.Current.GetUserRepository();

        public bool Login(string loginName, string password, out User user, out string message)
        {
            user = null;
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(password))
            {
                message = "El usuario y la contraseña son requeridos.";
                return false;
            }

            user = repositoryUser.GetByLoginName(loginName);

            if (user == null)
            {
                message = "Usuario no encontrado.";
                return false;
            }

            if (user.Password != password) 
            {
                message = "Contraseña incorrecta.";
                user = null;
                return false;
            }

            if (user.State != 1)
            {
                message = "El usuario no está activo.";
                user = null;
                return false;
            }

            return true;
        }
    }
}
