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

        public IUserRepository<User> GetUserRepository()
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

        public IUserRepository<User> GetEmployeeRepository()
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

        public IGenericRepository<BookingState> GetBookingStateRepository()
        {

            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.BookingStateRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.BookingStateRepository();
            }
            else
            {
                return new Repositories.Memory.BookingStateRepository();
            }
        }

        public IGenericRepository<Promotion> GetPromotionRepository()
        {

            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.PromotionRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.PromotionRepository();
            }
            else
            {
                return new Repositories.Memory.PromotionRepository();
            }
        }

        public IGenericRepository<Field> GetFieldRepository()
        {

            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.FieldRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.FieldRepository();
            }
            else
            {
                return new Repositories.Memory.FieldRepository();
            }
        }

        public IGenericRepository<FieldState> GetFieldStateRepository()
        {

            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.FieldStateRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.FieldStateRepository();
            }
            else
            {
                return new Repositories.Memory.FieldStateRepository();
            }
        }

        public IGenericRepository<CustomerState> GetCustomerStateRepository()
        {
            if (backend == "SqlServer")
            {
                return new Repositories.SqlServer.CustomerStateRepository();
            }
            if (backend == "File")
            {
                return new Repositories.File.CustomerStateRepository();
            }
            else
            {
                return new Repositories.Memory.CustomerStateRepository();
            }
        }
    }
}
