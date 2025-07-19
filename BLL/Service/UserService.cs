using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DAL.Contracts;
using DAL.Factory;
using Domain;

namespace BLL.Service
{
    public class UserService
    {
        private readonly IUserRepository<User> _userRepo;

        // Constructor por defecto que obtiene el repositorio desde el Factory
        public UserService()
        {
            _userRepo = Factory.Current.GetUserRepository();
        }

        // Constructor alternativo (útil para inyección de dependencias o tests)
        public UserService(IUserRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        // LOGIN
        public bool Login(string loginName, string password, out User user, out string message)
        {
            user = null;
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(password))
            {
                message = "El usuario y la contraseña son requeridos.";
                return false;
            }

            user = _userRepo.GetByLoginName(loginName);

            if (user == null)
            {
                message = "Usuario o contraseña incorrectos";
                return false;
            }

            if (user.Password != password)
            {
                message = "Contraseña incorrecta.";
                user = null;
                return false;
            }

            if (user.State != 1)
            {
                message = "El usuario no está activo.";
                user = null;
                return false;
            }

            return true;
        }

        // REGISTRO
        public void RegisterUser(User user)
        {
            ValidateUser(user);

            var existingUser = _userRepo.GetByLoginName(user.LoginName);
            if (existingUser != null)
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");

            _userRepo.Insert(user);
        }

        // ACTUALIZACIÓN
        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");

            if (user.UserId == Guid.Empty)
                throw new ArgumentException("El ID del usuario es obligatorio para modificar.");

            ValidateUser(user); 

            var existingUser = _userRepo.GetOne(user.UserId);
            if (existingUser == null)
                throw new InvalidOperationException("No se encontró el usuario a modificar.");

            _userRepo.Update(user.UserId, user); // delegás el update a DAL
        }

        // VALIDACIONES
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

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public List<User> GetAll(int? nroDocumento, string firstName, string lastName, string telephone, string mail)
        {
            return _userRepo.GetAll(nroDocumento, firstName, lastName, telephone, mail).ToList();
        }
    }
}


