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
            ValidateCustomer(customer);

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


            if (customer.State < 0)
                throw new ArgumentException("El estado del cliente debe ser un valor válido.");

        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "El cliente no puede ser nulo.");

            if (customer.IdCustomer == Guid.Empty)
                throw new ArgumentException("El ID del cliente es obligatorio para modificar.");

            ValidateCustomer(customer);

            var existingCustomer = _customerRepo.GetOne(customer.IdCustomer);
            if (existingCustomer == null)
                throw new InvalidOperationException("No se encontró el cliente a modificar.");

            _customerRepo.Update(customer.IdCustomer, customer);
        }

        public List<Customer> GetAll(int? nroDocumento, string firstName, string lastName, string telephone, string mail, int state)
        {
            return _customerRepo.GetAll(nroDocumento, firstName, lastName, telephone, mail, state).ToList();
        }
    }
}