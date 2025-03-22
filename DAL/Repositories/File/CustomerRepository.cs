using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Contracts;
using Domain;
using System.Configuration;
using System.Text.Json;

namespace DAL.Repositories.File
{
    internal class CustomerRepository : IGenericRepository<Customer>
    {
        private readonly string _filePath;
        private readonly List<Customer> _customers;

        public CustomerRepository()
        {
            _filePath = ConfigurationManager.AppSettings["PathFile"] ?? "customer.json";
        }

        private List<Customer> LoadCustomers()
        {
            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Customer>>(json) ?? new List<Customer>();
            }
            return new List<Customer>();
        }

        private void SaveCustomers()
        {
            var json = JsonSerializer.Serialize(_customers, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_filePath, json);
        }

        public void Delete(Guid Id)
        {
            var customer = _customers.FirstOrDefault(c => c.IdCustomer == Id);
            if (customer != null)
            {
                _customers.Remove(customer);
                SaveCustomers();
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetOne(Guid Id)
        {
            return _customers.FirstOrDefault(c => c.IdCustomer == Id);
        }

        public void Insert(Customer Object)
        {
            _customers.Add(Object);
            SaveCustomers();
        }

        public void Update(Guid Id, Customer Object)
        {
            var customer = _customers.FirstOrDefault(c => c.IdCustomer == Id);
            if (customer != null)
            {
                customer.NroDocument = Object.NroDocument;
                customer.FirstName = Object.FirstName;
                customer.LastName = Object.LastName;
                customer.State = Object.State;
                customer.Comment = Object.Comment;
                customer.Telephone = Object.Telephone;
                customer.Mail = Object.Mail;
                customer.Address = Object.Address;
                SaveCustomers();
            }
        }

        public IEnumerable<Customer> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }
    }
}
