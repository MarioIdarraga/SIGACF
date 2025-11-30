using System;
using System.Diagnostics.Tracing;
using BLL.BusinessException;
using DAL.Factory;
using SL.Helpers;

namespace SL.Service
{
    /// <summary>
    /// Service Layer encargado del flujo de recuperación de contraseña:
    /// validar el usuario, generar el token temporal y enviar el correo.
    /// Registra todo en logs y maneja excepciones de negocio y errores inesperados.
    /// </summary>
    public class PasswordRecoverySLService
    {
        public void GenerateRecoveryRequest(string userOrMail)
        {
            // Log inicial
            LoggerService.Log(
                "Inicio de recuperación de contraseña.",
                EventLevel.Informational,
                SL.Helpers.Session.CurrentUser?.LoginName // Fix: Use LoginName property of CurrentUser
            );

            try
            {
                if (string.IsNullOrWhiteSpace(userOrMail))
                    throw new BusinessException("Debe ingresar un usuario o correo.");

                var repo = global::DAL.Factory.Factory.Current.GetUserRepository();

                // Buscar por usuario o mail
                var user = repo.GetByUsernameOrEmail(userOrMail);

                if (user == null)
                    throw new BusinessException("No se encontró ningún usuario con ese dato.");

                // 1) Generar token
                string token = Guid.NewGuid().ToString("N");

                // 2) Definir expiración del token (1 hora)
                DateTime expiration = DateTime.Now.AddHours(1);

                // 3) Guardar token en DB
                repo.SavePasswordResetToken(user.UserId, token, expiration);

                // 4) Enviar correo
                string body =
                $"Hola {user.FirstName},\n\n" +
                "Hemos recibido una solicitud para restablecer su contraseña.\n\n" +
                $"Su código temporal es:\n\n{token}\n\n" +
                "Ingrese este código en la aplicación.\n\n" +
                "Si no solicitó esto, ignore el mensaje.";

                EmailHelper.SendMail(user.Mail, "Recuperación de contraseña", body);

                string actor = SL.Helpers.Session.CurrentUser?.LoginName ?? "Anonymous";

                // Log OK
                LoggerService.Log(
                    "Solicitud de recuperación de contraseña procesada correctamente.",
                    EventLevel.Informational,
                    actor
                );
            }
            catch (BusinessException ex)
            {
                string actor = SL.Helpers.Session.CurrentUser?.LoginName ?? "Anonymous";

                LoggerService.Log(
                    $"Error de negocio en recuperación de contraseña: {ex.Message}",
                    EventLevel.Warning,
                    actor
                );

                throw;
            }
            catch (Exception ex)
            {
                string actor = SL.Helpers.Session.CurrentUser?.LoginName ?? "Anonymous";

                LoggerService.Log(
                    $"Error inesperado en recuperación de contraseña: {ex}",
                    EventLevel.Error,
                    actor
                );

                throw new BusinessException("No se pudo generar la recuperación de contraseña. Intente nuevamente.");
            }
        }
    }
}
