using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para la entidad Customer.
    /// </summary>
    internal class CustomerRepository : IGenericRepository<Customer>
    {
        /// <summary>
        /// Lista estática que almacena clientes precargados.
        /// </summary>
        private static readonly List<Customer> _customers = new List<Customer>
        {
            new Customer
            {
                IdCustomer = Guid.NewGuid(),
                NroDocument = 19113731,
                FirstName = "Mario",
                LastName = "Idarraga",
                Mail = "Mario@gmail.com",
                Address = "Balcarce 800",
                Telephone = "+5491164377169",
                Comment = "Le gusta jugar los lunes",
                State = 1 // Activo
            }
        };

        #region CRUD

        /// <summary>
        /// Inserta un nuevo cliente en memoria.
        /// </summary>
        public void Insert(Customer Object)
        {
            try
            {
                if (Object.IdCustomer == Guid.Empty)
                    Object.IdCustomer = Guid.NewGuid();

                _customers.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el cliente en memoria.", ex);
            }
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        public void Update(Guid Id, Customer Object)
        {
            try
            {
                var customer = _customers.FirstOrDefault(c => c.IdCustomer == Id);

                if (customer == null)
                    throw new Exception("El cliente no existe.");

                customer.NroDocument = Object.NroDocument;
                customer.FirstName = Object.FirstName;
                customer.LastName = Object.LastName;
                customer.State = Object.State;
                customer.Comment = Object.Comment;
                customer.Telephone = Object.Telephone;
                customer.Mail = Object.Mail;
                customer.Address = Object.Address;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente en memoria.", ex);
            }
        }

        /// <summary>
        /// Elimina un cliente según su identificador.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var customer = _customers.FirstOrDefault(c => c.IdCustomer == Id);

                if (customer == null)
                    throw new Exception("El cliente no existe.");

                _customers.Remove(customer);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente en memoria.", ex);
            }
        }

        #endregion

        #region SELECT

        /// <summary>
        /// Obtiene un cliente por su ID.
        /// </summary>
        public Customer GetOne(Guid Id)
        {
            try
            {
                return _customers.FirstOrDefault(c => c.IdCustomer == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el cliente en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene la lista completa de clientes.
        /// </summary>
        public IEnumerable<Customer> GetAll()
        {
            try
            {
                return _customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los clientes en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene clientes filtrados por documento, nombre, apellido, teléfono y mail.
        /// </summary>
        public IEnumerable<Customer> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            try
            {
                var list = _customers.AsEnumerable();

                if (nroDocument.HasValue)
                    list = list.Where(c => c.NroDocument == nroDocument.Value);

                if (!string.IsNullOrWhiteSpace(firstName))
                    list = list.Where(c =>
                        c.FirstName != null &&
                        c.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0);

                if (!string.IsNullOrWhiteSpace(lastName))
                    list = list.Where(c =>
                        c.LastName != null &&
                        c.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0);

                if (!string.IsNullOrWhiteSpace(telephone))
                    list = list.Where(c =>
                        !string.IsNullOrEmpty(c.Telephone) &&
                        c.Telephone.IndexOf(telephone, StringComparison.OrdinalIgnoreCase) >= 0);

                if (!string.IsNullOrWhiteSpace(mail))
                    list = list.Where(c =>
                        !string.IsNullOrEmpty(c.Mail) &&
                        c.Mail.IndexOf(mail, StringComparison.OrdinalIgnoreCase) >= 0);

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes filtrados en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene clientes filtrados por estado adicionalmente.
        /// </summary>
        public IEnumerable<Customer> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            try
            {
                return GetAll(nroDocument, firstName, lastName, telephone, mail)
                    .Where(c => c.State == state)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes filtrados por estado en memoria.", ex);
            }
        }

        /// <summary>
        /// Sobrecarga no aplicable para clientes. Devuelve todos.
        /// </summary>
        public IEnumerable<Customer> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return GetAll(nroDocument, null, null, null, null);
        }

        /// <summary>
        /// Sobrecarga no aplicable para clientes. Devuelve todos.
        /// </summary>
        public IEnumerable<Customer> GetAll(DateTime? from, DateTime? to, int state)
        {
            return GetAll();
        }

        #endregion
    }
}
