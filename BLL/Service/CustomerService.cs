using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace BLL.Service
{
    public class CustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepo;

        public CustomerService(IGenericRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public void RegisterCustomer(Customer customer)
        {
            // Validaciones de negocio
            ValidateCustomer(customer);

            // Insertar si todo está correcto
            _customerRepo.Insert(customer);
        }

        private void ValidateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "El cliente no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("El apellido es obligatorio.");

            if (customer.NroDocument <= 0)
                throw new ArgumentException("El número de documento debe ser válido y mayor que cero.");

            if (string.IsNullOrWhiteSpace(customer.Mail) || !IsValidEmail(customer.Mail))
                throw new ArgumentException("El email no es válido.");

            if (customer.State < 0)
                throw new ArgumentException("El estado del cliente debe ser un valor válido.");

        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }
    }
}