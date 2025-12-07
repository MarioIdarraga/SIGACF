using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    /// <summary>
    /// Repositorio en memoria para la entidad User.
    /// Usado como fallback sin SQL ni FILE, ideal para pruebas y modo demo.
    /// </summary>
    internal class UserRepository : IUserRepository<User>
    {
        /// <summary>
        /// Lista estática que simula el almacenamiento de usuarios.
        /// </summary>
        private static readonly List<User> _users = new List<User>
        {
            new User
            {
                UserId = Guid.Parse("d3b7a8c1-2c44-4ae1-a8b7-9fd91a1bb001"),
                LoginName = "admin",
                Password = "gs1QXUuS9s2PEmGqS7Zn3Q==", 
                NroDocument = 99999999,
                FirstName = "Administrador",
                LastName = "Sistema",
                Position = "Admin",
                Mail = "admin@mail.com",
                Address = "N/A",
                Telephone = "0000-0000",
                State = 1,
                DVH = "",
                IsEmployee = true,
                ResetToken = null,
                ResetTokenExpiration = null,
                FailedAttempts = 0
            }
        };

        #region CRUD

        /// <summary>
        /// Inserta un nuevo usuario en memoria.
        /// </summary>
        public void Insert(User Object)
        {
            try
            {
                if (Object.UserId == Guid.Empty)
                    Object.UserId = Guid.NewGuid();

                _users.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar usuario en memoria.", ex);
            }
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        public void Update(Guid Id, User Object)
        {
            try
            {
                var existing = _users.FirstOrDefault(x => x.UserId == Id);

                if (existing == null)
                    throw new Exception("El usuario no existe.");

                _users.Remove(existing);

                Object.UserId = Id;
                _users.Add(Object);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario en memoria.", ex);
            }
        }

        /// <summary>
        /// Elimina un usuario por su identificador.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var existing = _users.FirstOrDefault(x => x.UserId == Id);

                if (existing == null)
                    throw new Exception("El usuario no existe.");

                _users.Remove(existing);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario en memoria.", ex);
            }
        }

        #endregion

        #region SELECT

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        public User GetOne(Guid Id)
        {
            try
            {
                return _users.FirstOrDefault(x => x.UserId == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            try
            {
                return _users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios en memoria.", ex);
            }
        }

        /// <summary>
        /// Búsqueda filtrada, igual que FILE.
        /// </summary>
        public IEnumerable<User> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            try
            {
                var list = _users.AsEnumerable();

                if (nroDocument.HasValue)
                    list = list.Where(x => x.NroDocument == nroDocument.Value);

                if (!string.IsNullOrWhiteSpace(firstName))
                    list = list.Where(x => x.FirstName != null &&
                        x.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0);

                if (!string.IsNullOrWhiteSpace(lastName))
                    list = list.Where(x => x.LastName != null &&
                        x.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0);

                if (!string.IsNullOrWhiteSpace(telephone))
                    list = list.Where(x => x.Telephone != null &&
                        x.Telephone.Contains(telephone));

                if (!string.IsNullOrWhiteSpace(mail))
                    list = list.Where(x => x.Mail != null &&
                        x.Mail.Contains(mail));

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios filtrados en memoria.", ex);
            }
        }

        /// <summary>
        /// Sobrecarga no usada → devuelve todos.
        /// </summary>
        public IEnumerable<User> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
            => GetAll();

        /// <summary>
        /// Obtiene usuario por LoginName.
        /// </summary>
        public User GetByLoginName(string loginName)
        {
            try
            {
                return _users.FirstOrDefault(x =>
                    x.LoginName.Equals(loginName, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por LoginName en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene usuario por login o email.
        /// </summary>
        public User GetByUsernameOrEmail(string userOrMail)
        {
            try
            {
                return _users.FirstOrDefault(x =>
                    x.LoginName.Equals(userOrMail, StringComparison.OrdinalIgnoreCase) ||
                    x.Mail.Equals(userOrMail, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por LoginName o Email en memoria.", ex);
            }
        }

        /// <summary>
        /// Obtiene usuario válido por token de recuperación.
        /// </summary>
        public User GetByPasswordResetToken(string token)
        {
            try
            {
                return _users.FirstOrDefault(x =>
                    x.ResetToken != null &&
                    x.ResetToken == token &&
                    x.ResetTokenExpiration.HasValue &&
                    x.ResetTokenExpiration.Value > DateTime.Now);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por token de recuperación en memoria.", ex);
            }
        }

        #endregion

        #region Password Reset

        /// <summary>
        /// Guarda el token de recuperación y su fecha de vencimiento.
        /// </summary>
        public void SavePasswordResetToken(Guid userId, string token, DateTime expiration)
        {
            try
            {
                var existing = _users.FirstOrDefault(x => x.UserId == userId);

                if (existing == null)
                    throw new Exception("El usuario no existe.");

                existing.ResetToken = token;
                existing.ResetTokenExpiration = expiration;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar token de recuperación en memoria.", ex);
            }
        }

        public object GetByDocument(int nroDocument)
        {
            throw new NotImplementedException();
        }

        User IUserRepository<User>.GetByDocument(int nroDocument)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
