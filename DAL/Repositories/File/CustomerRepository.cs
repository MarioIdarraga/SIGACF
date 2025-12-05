using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para Customer.
    /// Realiza operaciones CRUD utilizando almacenamiento JSON.
    /// </summary>
    internal class CustomerRepository : IGenericRepository<Customer>
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio asegurando la existencia del archivo.
        /// </summary>
        public CustomerRepository()
        {
            try
            {
                _filePath = ConfigurationManager.AppSettings["PathFile"]
                            ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Customers.json");

                string folder = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "[]");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el repositorio de Customers.", ex);
            }
        }

        #region Helpers
        /// <summary>
        /// Carga todos los clientes desde el archivo JSON.
        /// </summary>
        private List<Customer> LoadCustomers()
        {
            try
            {
                if (!System.IO.File.Exists(_filePath))
                    return new List<Customer>();

                string json = System.IO.File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los datos de clientes.", ex);
            }
        }

        /// <summary>
        /// Guarda todos los clientes en el archivo JSON.
        /// </summary>
        private void SaveCustomers(List<Customer> list)
        {
            try
            {
                string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los datos de clientes.", ex);
            }
        }
        #endregion

        #region CRUD

        /// <summary>
        /// Elimina un cliente según su ID.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var customers = LoadCustomers();
                var cust = customers.FirstOrDefault(c => c.IdCustomer == Id);

                if (cust == null)
                    throw new Exception("El cliente no existe.");

                customers.Remove(cust);
                SaveCustomers(customers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente.", ex);
            }
        }

        /// <summary>
        /// Obtiene un cliente por su identificador.
        /// </summary>
        public Customer GetOne(Guid Id)
        {
            try
            {
                var customers = LoadCustomers();
                return customers.FirstOrDefault(c => c.IdCustomer == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el cliente.", ex);
            }
        }

        /// <summary>
        /// Inserta un nuevo cliente.
        /// </summary>
        public void Insert(Customer Object)
        {
            try
            {
                var customers = LoadCustomers();
                customers.Add(Object);
                SaveCustomers(customers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el cliente.", ex);
            }
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        public void Update(Guid Id, Customer Object)
        {
            try
            {
                var customers = LoadCustomers();
                var cust = customers.FirstOrDefault(c => c.IdCustomer == Id);

                if (cust == null)
                    throw new Exception("El cliente no existe.");

                cust.NroDocument = Object.NroDocument;
                cust.FirstName = Object.FirstName;
                cust.LastName = Object.LastName;
                cust.State = Object.State;
                cust.Comment = Object.Comment;
                cust.Telephone = Object.Telephone;
                cust.Mail = Object.Mail;
                cust.Address = Object.Address;

                SaveCustomers(customers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente.", ex);
            }
        }

        #endregion

        #region GetAll

        /// <summary>
        /// Obtiene la lista completa de clientes.
        /// </summary>
        public IEnumerable<Customer> GetAll()
        {
            try
            {
                return LoadCustomers();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los clientes.", ex);
            }
        }

        /// <summary>
        /// Obtiene clientes filtrados por documento, nombre, apellido, teléfono y mail.
        /// </summary>
        /// <summary>
        /// Obtiene clientes filtrados por documento, nombre, apellido, teléfono y mail.
        /// </summary>
        public IEnumerable<Customer> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            try
            {
                var list = LoadCustomers();

                if (nroDocument.HasValue)
                    list = list.Where(c => c.NroDocument == nroDocument.Value).ToList();

                if (!string.IsNullOrWhiteSpace(firstName))
                    list = list.Where(c =>
                        c.FirstName != null &&
                        c.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0
                    ).ToList();

                if (!string.IsNullOrWhiteSpace(lastName))
                    list = list.Where(c =>
                        c.LastName != null &&
                        c.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0
                    ).ToList();

                if (!string.IsNullOrWhiteSpace(telephone))
                    list = list.Where(c =>
                        !string.IsNullOrEmpty(c.Telephone) &&
                        c.Telephone.IndexOf(telephone, StringComparison.OrdinalIgnoreCase) >= 0
                    ).ToList();

                if (!string.IsNullOrWhiteSpace(mail))
                    list = list.Where(c =>
                        !string.IsNullOrEmpty(c.Mail) &&
                        c.Mail.IndexOf(mail, StringComparison.OrdinalIgnoreCase) >= 0
                    ).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes filtrados.", ex);
            }
        }


        /// <summary>
        /// Método requerido por la interfaz, no aplica a Customers.
        /// </summary>
        public IEnumerable<Customer> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return GetAll(nroDocument, null, null, null, null);
        }

        /// <summary>
        /// Obtiene clientes filtrados por estado adicionalmente.
        /// </summary>
        public IEnumerable<Customer> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            try
            {
                return GetAll(nroDocument, firstName, lastName, telephone, mail)
                    .Where(c => c.State == state);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes filtrados por estado.", ex);
            }
        }

        /// <summary>
        /// No aplica a Customers. Devuelve todos.
        /// </summary>
        public IEnumerable<Customer> GetAll(DateTime? from, DateTime? to, int state)
        {
            return GetAll();
        }

        #endregion
    }
}

