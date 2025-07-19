using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
                _customerService.UpdateCustomer(customer);
                LoggerService.Log("Cliente modificado correctamente.", EventLevel.Informational, Session.CurrentUser?.LoginName);
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar cliente: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }

        public List<Customer> GetAll(int? nroDocumento, string firstName, string lastName, string telephone, string mail)
        {
            LoggerService.Log("Inicio búsqueda de clientes.", EventLevel.Informational, Session.CurrentUser?.LoginName);

            try
            {
                var result = _customerService.GetAll(nroDocumento, firstName, lastName, telephone, mail);
                LoggerService.Log($"Fin búsqueda de clientes. Resultados: {result.Count}", EventLevel.Informational, Session.CurrentUser?.LoginName);
                return result;
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al buscar clientes: {ex.Message}", EventLevel.Error, Session.CurrentUser?.LoginName);
                throw;
            }
        }
    }
}


