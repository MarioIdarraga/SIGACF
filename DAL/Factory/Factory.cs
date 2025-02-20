using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
    public sealed class Factory
    {
        private readonly static Factory _intance = new Factory();

        private string backend;

        public static Factory Current
        {
            get
            {
                return _intance;
            }

        }
        private Factory()
        {
            backend = ConfigurationManager.AppSettings["backend"];
        }

        public IGenericRepository<User> GetUserRepository()
        {

            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.UserRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.UserRepository();
            }
            else
            {
                return new Repositories.Memory.UserRepository();
            }
        }

        public IGenericRepository<Customer> GetCustomerRepository()
        {

            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.CustomerRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.CustomerRepository();
            }
            else
            {
                return new Repositories.Memory.CustomerRepository();
            }
        }

        public IGenericRepository<Employee> GetEmployeeRepository()
        {
 
            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.EmployeeRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.EmployeeRepository();
            }
            else
            {
                return new Repositories.Memory.EmployeeRepository();
            }
        }
        public IGenericRepository<Booking> GetBookingRepository()
        {

            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.BookingRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.BookingRepository();
            }
            else
            {
                return new Repositories.Memory.BookingRepository();
            }
        }
    }
}
