using BLL.Service;
using Domain;
using SL.Helpers;
using System;
using System.Diagnostics.Tracing;
using SL.Services;
using BLL.BusinessException;

namespace SL
{
    /// <summary>
    /// Servicio de autenticación ubicado en la capa SL.
    /// Coordina el proceso de login entre la UI y la BLL,
    /// captura excepciones de negocio, registra auditoría
    /// e inicializa la sesión del usuario.
    /// </summary>
    public class LoginSLService
    {
        private readonly UserSLService _userSLService;

        /// <summary>
        /// Inicializa dependencias del servicio de login.
        /// </summary>
        public LoginSLService()
        {
            _userSLService = new UserSLService(new UserService());
        }

        /// <summary>
        /// Intenta autenticar un usuario utilizando la capa BLL.
        /// La SL no aplica reglas de negocio: solo captura errores,
        /// registra eventos y comunica el resultado a la UI.
        /// </summary>
        /// <param name="loginName">Nombre de usuario.</param>
        /// <param name="password">Contraseña ingresada.</param>
        /// <param name="user">Devuelve el usuario autenticado si el login fue exitoso.</param>
        /// <param name="message">Mensaje descriptivo para la UI.</param>
        /// <returns>true si el login fue exitoso; false en caso contrario.</returns>
        public bool TryLogin(string loginName, string password, out User user, out string message)
        {
            string actor = loginName ?? "Anonymous";
            user = null;
            message = string.Empty;

            try
            {
                var loginBLL = new LoginService();

                // BLL devuelve el usuario o lanza BusinessException
                user = loginBLL.Login(loginName, password);

                // Login correcto → setear sesión
                Session.CurrentUser = user;

                LoggerService.Log(
                    $"Usuario {user.LoginName} inició sesión correctamente.",
                    EventLevel.Informational,
                    user.LoginName
                );

                message = "Inicio de sesión exitoso.";
                return true;
            }
            catch (BusinessException ex)
            {
                // Error de negocio controlado por la BLL
                LoggerService.Log(
                    $"Error de negocio durante login: {ex.Message}",
                    EventLevel.Warning,
                    actor
                );

                message = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                // Error inesperado
                LoggerService.Log(
                    $"Error inesperado en LoginSLService.TryLogin: {ex}",
                    EventLevel.Error,
                    actor
                );

                message = "Ocurrió un error inesperado al intentar iniciar sesión.";
                return false;
            }
        }

        /// <summary>
        /// Restablece la contraseña mediante un token temporal.
        /// Incluye actualización de DVH y DVV y registro de auditoría.
        /// </summary>
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

                // Resetear seguridad
                user.FailedAttempts = 0;
                user.State = 1; // Activo

                // Eliminar token
                user.ResetToken = null;
                user.ResetTokenExpiration = null;

                // Recalcular DVH
                user.DVH = DVHHelper.CalcularDVH(user);

                // Guardar cambios
                userRepo.Update(user.UserId, user);

                // Actualizar DVV
                var usuarios = userRepo.GetAll();
                new DVVService().RecalcularDVV(usuarios, "Users");

                message = "La contraseña fue actualizada correctamente.";
                return true;
            }
            catch (Exception ex)
            {
                LoggerService.Log(
                    $"Error inesperado en ResetPassword: {ex.Message}",
                    EventLevel.Error
                );

                message = "Ocurrió un error al intentar actualizar la contraseña.";
                return false;
            }
        }
    }
}

