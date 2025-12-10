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
    /// <summary>
    /// Fábrica central para la creación de repositorios según el backend configurado
    /// (SqlServer, File o Memory). Implementa el patrón Singleton.
    /// </summary>
    public sealed class Factory
    {
        private readonly static Factory _intance = new Factory();
        private string backend;

        /// <summary>
        /// Instancia actual de la fábrica. (Singleton)
        /// </summary>
        public static Factory Current => _intance;

        /// <summary>
        /// Constructor privado. Inicializa el backend leyendo AppSettings["backend"].
        /// </summary>
        private Factory()
        {
            backend = ConfigurationManager.AppSettings["backend"];
        }

        /// <summary>
        /// Obtiene el repositorio de usuarios según el backend configurado.
        /// </summary>
        public IUserRepository<User> GetUserRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.UserRepository();
            if (backend == "File")
                return new Repositories.File.UserRepository();

            return new Repositories.Memory.UserRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de clientes según el backend configurado.
        /// </summary>
        public IGenericRepository<Customer> GetCustomerRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.CustomerRepository();
            if (backend == "File")
                return new Repositories.File.CustomerRepository();

            return new Repositories.Memory.CustomerRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de empleados (mismo repositorio que usuarios en este diseño).
        /// </summary>
        public IUserRepository<User> GetEmployeeRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.UserRepository();
            if (backend == "File")
                return new Repositories.File.UserRepository();

            return new Repositories.Memory.UserRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de reservas según el backend configurado.
        /// </summary>
        public IGenericRepository<Booking> GetBookingRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.BookingRepository();
            if (backend == "File")
                return new Repositories.File.BookingRepository();

            return new Repositories.Memory.BookingRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de estados de reserva.
        /// </summary>
        public IGenericRepository<BookingState> GetBookingStateRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.BookingStateRepository();
            if (backend == "File")
                return new Repositories.File.BookingStateRepository();

            return new Repositories.Memory.BookingStateRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de promociones.
        /// </summary>
        public IGenericRepository<Promotion> GetPromotionRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.PromotionRepository();
            if (backend == "File")
                return new Repositories.File.PromotionRepository();

            return new Repositories.Memory.PromotionRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de canchas.
        /// </summary>
        public IGenericRepository<Field> GetFieldRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.FieldRepository();
            if (backend == "File")
                return new Repositories.File.FieldRepository();

            return new Repositories.Memory.FieldRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de estados de cancha.
        /// </summary>
        public IGenericRepository<FieldState> GetFieldStateRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.FieldStateRepository();
            if (backend == "File")
                return new Repositories.File.FieldStateRepository();

            return new Repositories.Memory.FieldStateRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de estados de cliente.
        /// </summary>
        public ICustomerStateRepository GetCustomerStateRepository()
        {
            if (backend == "SqlServer")
                return new DAL.Repositories.SqlServer.CustomerStateRepository();
            if (backend == "File")
                return new DAL.Repositories.File.CustomerStateRepository();

            return new DAL.Repositories.Memory.CustomerStateRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de pagos.
        /// </summary>
        public IPayRepository GetPayRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.PayRepository();
            if (backend == "File")
                return new Repositories.File.PayRepository();

            return new Repositories.Memory.PayRepository();
        }

        /// <summary>
        /// Obtiene el repositorio de métodos de pago.
        /// </summary>
        /// <exception cref="NotSupportedException">Si el backend no está soportado.</exception>
        public IPaymentMethodRepository GetPaymentMethodRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.PaymentMethodRepository();

            if (backend == "File")
                return new Repositories.File.PaymentMethodRepository();

            throw new NotSupportedException($"El backendSL '{backend}' no es soportado.");
        }

        /// <summary>
        /// Obtiene el repositorio de estados de pago.
        /// </summary>
        /// <exception cref="NotSupportedException">Si el backend no está soportado.</exception>
        public IPayStateRepository GetPayStateRepository()
        {
            if (backend == "SqlServer")
                return new Repositories.SqlServer.PayStateRepository();

            if (backend == "File")
                return new Repositories.File.PayStateRepository();

            throw new NotSupportedException($"El backendSL '{backend}' no es soportado.");
        }
    }
}


