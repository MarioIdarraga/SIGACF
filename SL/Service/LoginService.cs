using BLL.Service;
using Domain;
using SL.Helpers;
using SL.Service.Extension;
using System.Diagnostics.Tracing;

namespace SL
{
    public class LoginService
    {
        private readonly UserSLService _userSLService;

        public LoginService()
        {
            _userSLService = new UserSLService(new UserService());
        }

        public bool TryLogin(string loginName, string password, out User user, out string message)
        {
            bool success = _userSLService.TryLogin(loginName, password, out user, out message);

            if (success)
            {
                Session.CurrentUser = user;
                LoggerService.Log($"Usuario {user.LoginName} inició sesión", EventLevel.Informational, user.LoginName);
            }
            else
            {
                LoggerService.Log($"Fallo intento de login con usuario: {loginName}", EventLevel.Warning, loginName);
            }

            return success;
        }
    }
}


