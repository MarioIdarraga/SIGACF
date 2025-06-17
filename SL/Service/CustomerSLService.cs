using System;
using System.Diagnostics.Tracing;
using BLL.Service;
using Domain;

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
            LoggerService.Log("Inicio de registro de cliente.");

            try
            {
                _customerService.RegisterCustomer(customer);
                LoggerService.Log("Cliente registrado correctamente.");
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al registrar cliente: {ex.Message}", EventLevel.Error);
                throw;
            }
        }

        public void Update(Guid idCustomer, Customer customer)
        {
            LoggerService.Log("Inicio de modificación de cliente.");

            try
            {
                _customerService.UpdateCustomer(customer);
                LoggerService.Log("Cliente modificado correctamente.");
            }
            catch (Exception ex)
            {
                LoggerService.Log($"Error al modificar cliente: {ex.Message}", EventLevel.Error);
                throw;
            }
        }
    }
}

