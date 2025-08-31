using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;
using BLL.Service;
using Domain;
using SL.Helpers;

namespace SL
{
    public class CustomerSLService
    {
        private readonly CustomerService _customerService;

        public CustomerSLService(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public void Insert(Customer customer)
        {
            LoggerService.Log("Inicio de registro de cliente.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                ValidateCustomer(customer);

                if (!AesEncryptionHelper.IsEncryptedAES(customer.Mail))
                    customer.Mail = AesEncryptionHelper.Encrypt(customer.Mail);

                if (!AesEncryptionHelper.IsEncryptedAES(customer.Telephone))
                    customer.Telephone = AesEncryptionHelper.Encrypt(customer.Telephone);

                _customerService.RegisterCustomer(customer);

                LoggerService.Log("Cliente registrado correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar cliente: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public void Update(Guid idCustomer, Customer customer)
        {
            LoggerService.Log("Inicio de modificación de cliente.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                ValidateCustomer(customer);

                if (!AesEncryptionHelper.IsEncryptedAES(customer.Mail))
                    customer.Mail = AesEncryptionHelper.Encrypt(customer.Mail);

                if (!AesEncryptionHelper.IsEncryptedAES(customer.Telephone))
                    customer.Telephone = AesEncryptionHelper.Encrypt(customer.Telephone);

                _customerService.UpdateCustomer(customer);

                LoggerService.Log("Cliente modificado correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar cliente: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public List<Customer> GetAll(int? nroDocumento, string firstName, string lastName, string telephone, string mail, int state)
        {
            LoggerService.Log("Inicio búsqueda de clientes.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                // Encriptar filtros si vienen como texto plano
                if (!string.IsNullOrWhiteSpace(mail) && !AesEncryptionHelper.IsEncryptedAES(mail))
                    mail = AesEncryptionHelper.Encrypt(mail);

                if (!string.IsNullOrWhiteSpace(telephone) && !AesEncryptionHelper.IsEncryptedAES(telephone))
                    telephone = AesEncryptionHelper.Encrypt(telephone);

                var result = _customerService.GetAll(nroDocumento, firstName, lastName, telephone, mail, state);

                foreach (var customer in result)
                {
                    if (!string.IsNullOrWhiteSpace(customer.Mail))
                        customer.Mail = AesEncryptionHelper.Decrypt(customer.Mail);

                    if (!string.IsNullOrWhiteSpace(customer.Telephone))
                        customer.Telephone = AesEncryptionHelper.Decrypt(customer.Telephone);
                }

                LoggerService.Log($"Fin búsqueda de clientes. Resultados: {result.Count}", EventLevel.Informational, Session.CurrentUser?.LoginName);
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar clientes: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        // 🔍 Validación previa a encriptación
        private void ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Mail) || !Regex.IsMatch(customer.Mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("El email no es válido.");

            if (string.IsNullOrWhiteSpace(customer.Telephone))
                throw new ArgumentException("El teléfono no puede estar vacío.");
        }
    }
}


