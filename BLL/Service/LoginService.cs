using BLL.BusinessException;
using Domain;
using System;

namespace BLL.Service
{
    public class LoginService
    {
        private readonly UserService _userService;

        public LoginService()
        {
            _userService = new UserService();
        }

        public bool TryLogin(string loginName, string password, out User user, out string message)
        {
            message = string.Empty;
            user = _userService.GetByLogin(loginName);

            if (user == null)
            {
                message = "Usuario inexistente.";
                return false;
            }

            // Usuario bloqueado
            if (user.State == 3)
            {
                message = "Su usuario está bloqueado. Solicite un nuevo código mediante '¿Olvidó su contraseña?'.";
                return false;
            }

            // Validar contraseña
            bool passOk = _userService.ValidatePassword(user, password);

            if (!passOk)
            {
                user.FailedAttempts++;

                if (user.FailedAttempts >= 3)
                {
                    user.State = 3;
                    message = "Usuario bloqueado por múltiples intentos fallidos.";
                }
                else
                {
                    message = "Contraseña incorrecta.";
                }

                _userService.Update(user);
                return false;
            }

            // Login correcto → resetear intentos
            user.FailedAttempts = 0;
            _userService.Update(user);

            // Si requiere cambio de contraseña
            if (user.State == 0)
            {
                message = "Debe cambiar su contraseña.";
                return false;
            }

            message = "Login exitoso.";
            return true;
        }
    }
}
