using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Service;
using Domain;
using SL.BLL;
using SL.Domain;

namespace SL
{
    public class LoginService
    {
        private readonly UserService _userService = new UserService();

        public bool TryLogin(string loginName, string password, out User user, out string message)
        {
            return _userService.Login(loginName, password, out user, out message);
        }
    }
}
