using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text.RegularExpressions;
using BLL.Service;
using Domain;
using SL.Helpers;

namespace SL.Service
{
    /// <summary>
    /// Servicio de capa lógica (SL) encargado de gestionar operaciones de clientes.
    /// Realiza validaciones, encriptación/desencriptación de datos sensibles,
    /// registro de auditoría y delega la persistencia a los servicios de la BLL.
    /// </summary>
    public class CustomerSLService
    {
        private readonly CustomerService _customerService;
        private readonly CustomerStateService _customerStateService;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de clientes.
        /// </summary>
        /// <param name="customerService">Servicio de dominio encargado de la persistencia de clientes.</param>
        /// <param name="customerStateService">Servicio para obtener información de estados de cliente.</param>
        public CustomerSLService(CustomerService customerService, CustomerStateService customerStateService)
        {
            _customerService = customerService;
            _customerStateService = customerStateService;
        }

        /// <summary>
        /// Inserta un nuevo cliente después de validar y encriptar sus datos sensibles.
        /// Registra auditoría antes y después de la operación.
        /// </summary>
        /// <param name="customer">Entidad del cliente a registrar.</param>
        /// <exception cref="Exception">Propaga cualquier error ocurrido en validación, encriptación o persistencia.</exception>
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

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// Valida y encripta los campos sensibles antes de persistir los cambios.
        /// </summary>
        /// <param name="idCustomer">Identificador del cliente.</param>
        /// <param name="customer">Cliente con los datos actualizados.</param>
        /// <exception cref="Exception">Propaga cualquier error de negocio o persistencia.</exception>
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

        /// <summary>
        /// Obtiene clientes filtrados según los parámetros especificados,
        /// desencripta datos sensibles antes de devolverlos
        /// y asigna la descripción del estado correspondiente.
        /// </summary>
        /// <param name="nroDocumento">Número de documento.</param>
        /// <param name="firstName">Nombre.</param>
        /// <param name="lastName">Apellido.</param>
        /// <param name="telephone">Teléfono.</param>
        /// <param name="mail">Correo electrónico.</param>
        /// <param name="state">Estado del cliente.</param>
        /// <returns>Lista de clientes filtrados.</returns>
        /// <exception cref="Exception">Propaga errores de acceso a datos o encriptación.</exception>
        public List<Customer> GetAll(int? nroDocumento, string firstName, string lastName,
                                     string telephone, string mail, int state)
        {
            LoggerService.Log("Inicio búsqueda de clientes.",
                EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                // Encriptar filtros sensibles antes de consultar
                if (!string.IsNullOrWhiteSpace(mail) && !AesEncryptionHelper.IsEncryptedAES(mail))
                    mail = AesEncryptionHelper.Encrypt(mail);

                if (!string.IsNullOrWhiteSpace(telephone) && !AesEncryptionHelper.IsEncryptedAES(telephone))
                    telephone = AesEncryptionHelper.Encrypt(telephone);

                // Obtener resultados de la BLL
                var result = _customerService.GetAll(nroDocumento, firstName, lastName, telephone, mail, state);

                // Desencriptar datos antes de devolverlos
                foreach (var customer in result)
                {
                    if (!string.IsNullOrWhiteSpace(customer.Mail))
                        customer.Mail = AesEncryptionHelper.Decrypt(customer.Mail);

                    if (!string.IsNullOrWhiteSpace(customer.Telephone))
                        customer.Telephone = AesEncryptionHelper.Decrypt(customer.Telephone);
                }

                // Mapear estados
                var states = _customerStateService.GetAll();
                foreach (var cust in result)
                {
                    cust.StateDescription =
                        states.FirstOrDefault(s => s.IdCustomerState == cust.State)?.Description
                        ?? "Desconocido";
                }

                LoggerService.Log(
                    $"Fin búsqueda de clientes. Resultados: {result.Count}",
                    EventLevel.Informational,
                    Session.CurrentUser?.LoginName);

                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar clientes: {ex.Message}",
                    EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        /// <summary>
        /// Valida los datos mínimos requeridos para un cliente antes de procesarlo.
        /// </summary>
        /// <param name="customer">Cliente a validar.</param>
        /// <exception cref="ArgumentException">
        /// Se lanza si el email no es válido o si el teléfono está vacío.
        /// </exception>
        private void ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Mail) ||
                !Regex.IsMatch(customer.Mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("El email no es válido.");

            if (string.IsNullOrWhiteSpace(customer.Telephone))
                throw new ArgumentException("El teléfono no puede estar vacío.");
        }
    }
}


