using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    internal class CustomerRepository : IGenericRepository<Customer>
    {
        private readonly List<Customer> _customers = new List<Customer>
        {
            new Customer()
            {
                IdCustomer = Guid.NewGuid(),
                NroDocument = 19113731,
                FirstName = "Mario",
                LastName = "Idarraga",
                Mail = "Mario@gmail.com",
                Address = "Balcarce 800",
                Telephone = "+5491164377169",
                Comment = "Le gusta jugar los lunes",
                State = 1
            }
        };

        public void Delete(Guid Id)
        {
            var customer = _customers.FirstOrDefault(c => c.IdCustomer == Id);
            if (customer != null)
            {
                _customers.Remove(customer);
            }
        }

        public IEnumerable<Customer> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            throw new NotImplementedException();
        }

        public Customer GetOne(Guid Id)
        {
            return _customers.FirstOrDefault(c => c.IdCustomer == Id);
        }

        public void Insert(Customer Object)
        {
            _customers.Add(Object);
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
            }
        }
    }
}
