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
    /// Helper para envío de correos utilizando configuración del App.config.
    /// Maneja errores y encapsula fallas inesperadas.
    /// </summary>
    public static class EmailHelper
    {
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
