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

        public UserService()
        {
            _userRepo = Factory.Current.GetUserRepository();
        }

        public UserService(IUserRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public User GetByLoginName(string loginName)
        {
            return _userRepo.GetByLoginName(loginName);
        }

        public void RegisterUser(User user)
        {
            ValidateUser(user);

            var existingUser = _userRepo.GetByLoginName(user.LoginName);
            if (existingUser != null)
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");

            _userRepo.Insert(user);
        }

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

        public List<User> GetAll(int? nroDocumento, string firstName, string lastName, string telephone, string mail)
        {
            return _userRepo.GetAll(nroDocumento, firstName, lastName, telephone, mail).ToList();
        }

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

        public object GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByLogin(string loginName)
        {
            return GetByLoginName(loginName);
        }

        public bool ValidatePassword(User user, string password)
        {
            if (user == null)
                return false;

            // La contraseña almacenada está encriptada con AES
            string encryptedInput = AesEncryptionHelper.Encrypt(password);

            return user.Password == encryptedInput;
        }

        public void Update(User user)
        {
            UpdateUser(user);
        }
    }
}




