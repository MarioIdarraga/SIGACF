using System;
using System.Collections.Generic;
using System.Linq;
using BLL.BusinessException;
using Domain;

namespace BLL.Service
{
    /// <summary>
    /// Gestiona la lógica de autenticación de usuarios.
    /// </summary>
    public class LoginService
    {
        private readonly UserService _userService;

        public LoginService()
        {
            _userService = new UserService();
        }

        /// <summary>
        /// Intenta autenticar un usuario aplicando reglas de negocio
        /// y gestionando los intentos fallidos.
        /// </summary>
        public User Login(string loginName, string password)
        {
            // Validaciones generales → ArgumentException
            if (string.IsNullOrWhiteSpace(loginName))
                throw new ArgumentException("Debe ingresar un nombre de usuario.", nameof(loginName));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Debe ingresar una contraseña.", nameof(password));

            // Buscar usuario
            var user = _userService.GetByLogin(loginName);
            if (user == null)
                throw new UsuarioInexistenteException("El usuario ingresado no existe.");

            // Usuario ya bloqueado
            if (user.State == 3)
                throw new UsuarioBloqueadoException("Su usuario está bloqueado. Solicite recuperación de contraseña.");

            // Validar contraseña
            bool passOk = _userService.ValidatePassword(user, password);
            if (!passOk)
            {
                user.FailedAttempts++;

                // Límite de intentos → bloquear usuario
                if (user.FailedAttempts >= 3)
                {
                    user.State = 3;
                    _userService.Update(user);
                    throw new UsuarioBloqueadoException("Usuario bloqueado por múltiples intentos fallidos.");
                }

                _userService.Update(user);
                throw new PasswordIncorrectaException("Contraseña incorrecta.");
            }

            // Login correcto → resetear intentos
            user.FailedAttempts = 0;
            _userService.Update(user);

            // Requiere cambio de contraseña
            if (user.State == 0)
                return user;

            // Usuario válido
            return user;
        }
    }
}
