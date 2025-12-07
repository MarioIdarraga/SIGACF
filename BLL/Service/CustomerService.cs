using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;
using BLL.BusinessException;

namespace BLL.Service
{
    /// <summary>
    /// Servicio que gestiona la lógica de negocio relacionada con clientes.
    /// </summary>
    public class CustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepo;

        public CustomerService(IGenericRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        /// <summary>
        /// Registra un nuevo cliente aplicando validaciones de negocio.
        /// </summary>
        public void RegisterCustomer(Customer customer)
        {
            ValidateCustomer(customer);

            // Regla importante → usar BusinessException
            ValidateDuplicateDni(customer);

            _customerRepo.Insert(customer);
        }

        /// <summary>
        /// Modifica un cliente existente.
        /// </summary>
        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "El cliente no puede ser nulo.");

            if (customer.IdCustomer == Guid.Empty)
                throw new ArgumentException("El ID del cliente es obligatorio para modificar.");

            ValidateCustomer(customer);

            // Regla importante → usar BusinessException
            ValidateDuplicateDni(customer, isUpdate: true);

            var existingCustomer = _customerRepo.GetOne(customer.IdCustomer);
            if (existingCustomer == null)
                throw new InvalidOperationException("No se encontró el cliente a modificar.");

            _customerRepo.Update(customer.IdCustomer, customer);
        }

        /// <summary>
        /// Obtiene clientes según filtros aplicados.
        /// </summary>
        public List<Customer> GetAll(int? nroDocumento, string firstName, string lastName,
                             string telephone, string mail, int state)
        {
            return _customerRepo
                .GetAll(nroDocumento, firstName, lastName, telephone, mail, state)
                .ToList();
        }

        // ============================
        // VALIDACIONES
        // ============================

        private void ValidateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("El apellido es obligatorio.");

            if (customer.NroDocument <= 0)
                throw new ArgumentException("El número de documento debe ser mayor que cero.");

            if (customer.State < 0)
                throw new ArgumentException("El estado del cliente debe ser válido.");
        }

        /// <summary>
        /// Regla de negocio que prohíbe dos clientes con el mismo DNI.
        /// </summary>
        private void ValidateDuplicateDni(Customer customer, bool isUpdate = false)
        {
            var list = _customerRepo
                .GetAll(customer.NroDocument, null, null, null, null, -1)
                .ToList();

            if (!list.Any())
                return;

            if (isUpdate)
            {
                if (list.Any(c => c.IdCustomer != customer.IdCustomer))
                    throw new ClienteDniDuplicadoException(
                        $"Ya existe un cliente con el DNI {customer.NroDocument}.");
            }
            else
            {
                throw new ClienteDniDuplicadoException(
                    $"Ya existe un cliente con el DNI {customer.NroDocument}.");
            }
        }
    }
}
