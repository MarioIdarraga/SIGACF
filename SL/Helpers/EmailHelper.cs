using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using BLL.BusinessException;

namespace SL.Helpers
{
    /// <summary>
    /// Proporciona funcionalidades para el envío de correos electrónicos
    /// utilizando la configuración definida en el archivo App.config.
    /// Maneja excepciones específicas del cliente SMTP y errores genéricos.
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Envía un correo electrónico utilizando los parámetros configurados en AppSettings.
        /// </summary>
        /// <param name="to">Dirección de correo del destinatario.</param>
        /// <param name="subject">Asunto del correo.</param>
        /// <param name="body">Contenido del mensaje.</param>
        /// <exception cref="BusinessException">
        /// Se lanza cuando ocurre un error SMTP o un error general al intentar enviar el correo.
        /// </exception>
        public static void SendMail(string to, string subject, string body)
        {
            try
            {
                string from = ConfigurationManager.AppSettings["MailSenderAddress"];
                string password = ConfigurationManager.AppSettings["MailSenderPassword"];
                string smtp = ConfigurationManager.AppSettings["MailSmtpServer"];
                int port = int.Parse(ConfigurationManager.AppSettings["MailSmtpPort"]);
                bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["MailEnableSsl"]);

                var client = new SmtpClient(smtp)
                {
                    Port = port,
                    Credentials = new NetworkCredential(from, password),
                    EnableSsl = enableSsl
                };

                var mail = new MailMessage(from, to, subject, body);

                client.Send(mail);
            }
            catch (SmtpException ex)
            {
                throw new BusinessException($"Error al enviar correo: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new BusinessException("No se pudo enviar el correo de recuperación.");
            }
        }
    }
}
