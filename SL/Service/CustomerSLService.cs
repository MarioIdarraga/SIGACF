using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void RegisterCustomer(Customer customer)
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
    }
}
