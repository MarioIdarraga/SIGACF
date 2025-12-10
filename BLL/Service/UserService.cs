using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DAL.Contracts;
using DAL.Factory;
using Domain;

namespace BLL.Service
{
    /// <summary>
    /// Servicio de negocio encargado de gestionar usuarios del sistema.
    /// Proporciona validación, registro, modificación y consultas de usuarios.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Repositorio utilizado para acceder y manipular datos de usuarios.
        /// </summary>
        private readonly IUserRepository<User> _userRepo;

        /// <summary>
        /// Constructor por defecto que obtiene el repositorio desde la fábrica.
        /// </summary>
        public UserService()
        {
            _userRepo = Factory.Current.GetUserRepository();
        }

        /// <summary>
        /// Constructor que permite inyectar un repositorio de usuario específico.
        /// </summary>
        /// <param name="userRepo">Repositorio que implementa <see cref="IUserRepository{User}"/>.</param>
        public UserService(IUserRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        /// <summary>
        /// Obtiene un usuario según su nombre de login.
        /// </summary>
        /// <param name="loginName">Login del usuario.</param>
        /// <returns>Instancia de <see cref="User"/> o null si no existe.</returns>
        public User GetByLoginName(string loginName)
        {
            return _userRepo.GetByLoginName(loginName);
        }

        /// <summary>
        /// Registra un nuevo usuario validando duplicados y consistencia de datos.
        /// </summary>
        /// <param name="user">Entidad usuario a registrar.</param>
        /// <exception cref="ArgumentException">Si los datos obligatorios no son válidos.</exception>
        /// <exception cref="BusinessException">Si el usuario o documento ya existe.</exception>
        public void RegisterUser(User user)
        {
            ValidateUser(user);

            var existingUser = _userRepo.GetByLoginName(user.LoginName);
            if (existingUser != null)
                throw new BLL.BusinessException.BusinessException("El nombre de usuario ya está en uso.");

            var existingByDoc = _userRepo.GetByDocument(user.NroDocument);
            if (existingByDoc != null)
                throw new BLL.BusinessException.BusinessException($"El número de documento {user.NroDocument} ya está registrado.");

            _userRepo.Insert(user);
        }

        /// <summary>
        /// Actualiza un usuario existente, verificando que exista y que sus datos sean válidos.
        /// </summary>
        /// <param name="user">Entidad usuario con los datos a modificar.</param>
        /// <exception cref="ArgumentNullException">Si el usuario es null.</exception>
        /// <exception cref="ArgumentException">Si el ID es inválido o los datos no son correctos.</exception>
        /// <exception cref="InvalidOperationException">Si el usuario no existe.</exception>
        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (user.UserId == Guid.Empty)
                throw new ArgumentException("El ID del usuario es obligatorio.");

            ValidateUser(user);

            var existingUser = _userRepo.GetOne(user.UserId);
            if (existingUser == null)
                throw new InvalidOperationException("No se encontró el usuario a modificar.");

            _userRepo.Update(user.UserId, user);
        }

        /// <summary>
        /// Obtiene la lista completa de usuarios filtrada por documento, nombre, apellido, teléfono o email.
        /// </summary>
        public List<User> GetAll(int? nroDocumento, string firstName, string lastName, string telephone, string mail)
        {
            return _userRepo.GetAll(nroDocumento, firstName, lastName, telephone, mail).ToList();
        }

        /// <summary>
        /// Valida la integridad de los datos de un usuario previo a su registro o modificación.
        /// </summary>
        /// <param name="user">Usuario a validar.</param>
        /// <exception cref="ArgumentException">Si alguno de los campos obligatorios es inválido.</exception>
        private void ValidateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LoginName))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("La contraseña es obligatoria.");

            if (user.NroDocument <= 0)
                throw new ArgumentException("El número de documento debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(user.FirstName))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.LastName))
                throw new ArgumentException("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(user.Mail) || !IsValidEmail(user.Mail))
                throw new ArgumentException("El email no es válido.");

            if (user.State < 0)
                throw new ArgumentException("El estado debe ser un valor positivo.");
        }

        /// <summary>
        /// Valida si un email tiene un formato correcto.
        /// </summary>
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// No implementado. No se utiliza.
        /// </summary>
        public object GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Alias de GetByLoginName.
        /// </summary>
        public User GetByLogin(string loginName)
        {
            return GetByLoginName(loginName);
        }

        /// <summary>
        /// Valida la contraseña comparando la contraseña ingresada con la almacenada en AES.
        /// </summary>
        /// <param name="user">Usuario autenticado.</param>
        /// <param name="password">Contraseña ingresada.</param>
        /// <returns>true si coincide; otherwise false.</returns>
        public bool ValidatePassword(User user, string password)
        {
            if (user == null)
                return false;

            string encryptedInput = AesEncryptionHelper.Encrypt(password);
            return user.Password == encryptedInput;
        }

        /// <summary>
        /// Alias de UpdateUser.
        /// </summary>
        public void Update(User user)
        {
            UpdateUser(user);
        }

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="UserId">Identificador del usuario.</param>
        public User GetOne(Guid UserId)
        {
            return _userRepo.GetOne(UserId);
        }
    }
}




