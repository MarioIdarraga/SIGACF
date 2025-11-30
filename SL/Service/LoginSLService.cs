using BLL.Service;
using Domain;
using SL.Helpers;
using System;
using System.Diagnostics.Tracing;
using DAL.Factory;
using SL.Services;
using BLL.BusinessException;

namespace SL
{
    /// <summary>
    /// Servicio de Login ubicado en la capa SL.
    /// Se encarga de validar credenciales, auditar el proceso de login
    /// y aplicar reglas de negocio relacionadas al estado del usuario.
    /// Retorna un bool como el resto de tus servicios, manteniendo el patrón
    /// ya usado en el sistema.
    /// </summary>
    public class LoginSLService
    {
        private readonly UserSLService _userSLService;

        /// <summary>
        /// Constructor del servicio de login.
        /// Inicializa UserSLService para el manejo de usuarios.
        /// </summary>
        public LoginSLService()
        {
            _userSLService = new UserSLService(new UserService());
        }

        /// <summary>
        /// Valida las credenciales del usuario, registra auditoría
        /// y verifica el estado del usuario (activo, bloqueado, cambio de contraseña, etc.).
        /// Devuelve un booleano y un mensaje como el resto de tus servicios.
        /// </summary>
        /// <param name="loginName">Nombre de usuario ingresado.</param>
        /// <param name="password">Contraseña ingresada.</param>
        /// <param name="user">Usuario autenticado si el login es exitoso.</param>
        /// <param name="message">Mensaje de resultado para la UI.</param>
        /// <returns>True si el login es válido; false en caso contrario.</returns>
        public bool TryLogin(string loginName, string password, out User user, out string message)
        {
            string actor = loginName ?? "Anonymous";
            user = null;
            message = string.Empty;

            try
            {
                // 1) Validar usuario + contraseña usando BLL (maneja intentos y bloqueo)
                var loginBLL = new LoginService();
                bool loginOk = loginBLL.TryLogin(loginName, password, out user, out message);

                if (!loginOk)
                {
                    LoggerService.Log(
                        $"Fallo intento de login con usuario: {loginName}",
                        EventLevel.Warning,
                        actor);

                    return false;
                }

                // 2) Validar estado del usuario
                if (user.State == 0)
                {
                    message = "Debe cambiar su contraseña.";
                    LoggerService.Log(
                        $"Usuario {user.LoginName} debe cambiar su contraseña (Estado = 0).",
                        EventLevel.Informational,
                        user.LoginName);

                    return false; // La UI abrirá el form de cambio
                }

                if (user.State == 3)
                {
                    message = "Su usuario está bloqueado. Solicite un nuevo código mediante '¿Olvidó su contraseña?'.";
                    LoggerService.Log(
                        $"Usuario {user.LoginName} bloqueado (Estado = 3).",
                        EventLevel.Warning,
                        user.LoginName);

                    return false; // La UI mostrará el mensaje
                }

                // 3) Usuario activo
                Session.CurrentUser = user;

                LoggerService.Log(
                    $"Usuario {user.LoginName} inició sesión correctamente.",
                    EventLevel.Informational,
                    user.LoginName);

                return true;
            }
            catch (BusinessException ex)
            {
                LoggerService.Log(
                    $"Error de negocio en login: {ex.Message}",
                    EventLevel.Warning,
                    actor);

                message = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado en LoginSLService.TryLogin: {ex}",
                    EventLevel.Error,
                    actor);

                message = "Ocurrió un error inesperado al intentar iniciar sesión. Intente nuevamente.";
                return false;
            }
        }

        public bool ResetPassword(string token, string newPassword, out string message)
        {
            message = string.Empty;

            try
            {

                var userRepo = global::DAL.Factory.Factory.Current.GetUserRepository();
                var user = userRepo.GetByPasswordResetToken(token);

                if (user == null)
                {
                    message = "El código ingresado no es válido o ha expirado.";
                    return false;
                }

                // Encriptar nueva contraseña
                user.Password = AesEncryptionHelper.Encrypt(newPassword);

                // Resetear estado de seguridad
                user.FailedAttempts = 0;
                user.State = 1; // Activo

                // Eliminar token
                user.ResetToken = null;
                user.ResetTokenExpiration = null;

                // Recalcular DVH
                user.DVH = DVHHelper.CalcularDVH(user);

                // Actualizar usuario
                userRepo.Update(user.UserId, user);

                // Recalcular DVV
                var usuarios = userRepo.GetAll();
                new DVVService().RecalcularDVV(usuarios, "Users");

                message = "La contraseña fue actualizada correctamente. Ya puede iniciar sesión.";
                return true;
            }
            catch (Exception ex)
            {
                message = "Ocurrió un error inesperado al intentar actualizar la contraseña.";
                LoggerService.Log($"Error en ResetPassword: {ex.Message}",
                                   System.Diagnostics.Tracing.EventLevel.Error);
                return false;
            }
        }
    }
}
