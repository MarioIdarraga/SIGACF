using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    /// <summary>
    /// Repositorio FILE para la entidad User.
    /// Gestiona operaciones CRUD y recuperación de contraseña usando archivo TXT.
    /// </summary>
    internal class UserRepository : IUserRepository<User>
    {
        private readonly string _filePath;

        /// <summary>
        /// Inicializa el repositorio y asegura la existencia del archivo.
        /// </summary>
        public UserRepository()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                _filePath = Path.Combine(folder, "Users.txt");

                if (!System.IO.File.Exists(_filePath))
                    System.IO.File.WriteAllText(_filePath, "");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el archivo de usuarios.", ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Convierte una línea del archivo a un objeto User.
        /// </summary>
        private User MapFromLine(string line)
        {
            var p = line.Split('|');

            return new User
            {
                UserId = Guid.Parse(p[0]),
                LoginName = p[1],
                Password = p[2],
                NroDocument = int.Parse(p[3]),
                FirstName = p[4],
                LastName = p[5],
                Position = p[6],
                Mail = p[7],
                Address = p[8],
                Telephone = p[9],
                State = int.Parse(p[10]),
                DVH = p[11],
                IsEmployee = bool.Parse(p[12]),
                ResetToken = string.IsNullOrWhiteSpace(p[13]) ? null : p[13],
                ResetTokenExpiration = string.IsNullOrWhiteSpace(p[14]) ? (DateTime?)null : DateTime.Parse(p[14]),
                FailedAttempts = int.Parse(p[15])
            };
        }

        /// <summary>
        /// Convierte un objeto User en una línea de texto.
        /// </summary>
        private string MapToLine(User u)
        {
            return string.Join("|",
                u.UserId,
                u.LoginName,
                u.Password,
                u.NroDocument,
                u.FirstName,
                u.LastName,
                u.Position,
                u.Mail,
                u.Address,
                u.Telephone,
                u.State,
                u.DVH,
                u.IsEmployee,
                u.ResetToken ?? "",
                u.ResetTokenExpiration?.ToString("o") ?? "",
                u.FailedAttempts
            );
        }

        /// <summary>
        /// Carga todos los usuarios desde el archivo.
        /// </summary>
        private List<User> LoadAll()
        {
            try
            {
                return System.IO.File.ReadAllLines(_filePath)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(MapFromLine)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los usuarios.", ex);
            }
        }

        /// <summary>
        /// Guarda todos los usuarios en el archivo.
        /// </summary>
        private void SaveAll(List<User> list)
        {
            try
            {
                var lines = list.Select(MapToLine);
                System.IO.File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los usuarios.", ex);
            }
        }

        #endregion


        #region CRUD

        /// <summary>
        /// Inserta un nuevo usuario.
        /// </summary>
        public void Insert(User Object)
        {
            try
            {
                var list = LoadAll();

                if (Object.UserId == Guid.Empty)
                    Object.UserId = Guid.NewGuid();

                list.Add(Object);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar usuario.", ex);
            }
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        public void Update(Guid Id, User Object)
        {
            try
            {
                var list = LoadAll();

                var existing = list.FirstOrDefault(x => x.UserId == Id);
                if (existing == null)
                    throw new Exception("El usuario no existe.");

                list.Remove(existing);
                Object.UserId = Id;

                list.Add(Object);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario.", ex);
            }
        }

        /// <summary>
        /// Elimina un usuario por su identificador.
        /// </summary>
        public void Delete(Guid Id)
        {
            try
            {
                var list = LoadAll();
                var item = list.FirstOrDefault(x => x.UserId == Id);

                if (item == null)
                    throw new Exception("El usuario no existe.");

                list.Remove(item);
                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario.", ex);
            }
        }

        #endregion


        #region SELECT

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            try
            {
                return LoadAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios.", ex);
            }
        }

        /// <summary>
        /// Búsqueda por filtros básicos.
        /// </summary>
        public IEnumerable<User> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            try
            {
                var list = LoadAll();

                if (nroDocument.HasValue)
                    list = list.Where(x => x.NroDocument == nroDocument.Value).ToList();

                if (!string.IsNullOrWhiteSpace(firstName))
                    list = list.Where(x => x.FirstName != null &&
                        x.FirstName.IndexOf(firstName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                if (!string.IsNullOrWhiteSpace(lastName))
                    list = list.Where(x => x.LastName != null &&
                        x.LastName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                if (!string.IsNullOrWhiteSpace(telephone))
                    list = list.Where(x => x.Telephone != null &&
                        x.Telephone.Contains(telephone)).ToList();

                if (!string.IsNullOrWhiteSpace(mail))
                    list = list.Where(x => x.Mail != null &&
                        x.Mail.Contains(mail)).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios filtrados.", ex);
            }
        }

        /// <summary>
        /// No se usan estos filtros en FILE, retorna todo.
        /// </summary>
        public IEnumerable<User> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            return GetAll();
        }

        /// <summary>
        /// Obtiene un usuario por ID.
        /// </summary>
        public User GetOne(Guid Id)
        {
            try
            {
                return LoadAll().FirstOrDefault(x => x.UserId == Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario.", ex);
            }
        }

        /// <summary>
        /// Obtiene un usuario por LoginName.
        /// </summary>
        public User GetByLoginName(string loginName)
        {
            try
            {
                return LoadAll().FirstOrDefault(x =>
                    x.LoginName.Equals(loginName, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por LoginName.", ex);
            }
        }

        /// <summary>
        /// Obtiene usuario por LoginName o Email.
        /// </summary>
        public User GetByUsernameOrEmail(string userOrMail)
        {
            try
            {
                return LoadAll().FirstOrDefault(x =>
                    x.LoginName.Equals(userOrMail, StringComparison.OrdinalIgnoreCase) ||
                    x.Mail.Equals(userOrMail, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por nombre o email.", ex);
            }
        }

        /// <summary>
        /// Obtiene usuario válido por token de recuperación.
        /// </summary>
        public User GetByPasswordResetToken(string token)
        {
            try
            {
                return LoadAll().FirstOrDefault(x =>
                    x.ResetToken != null &&
                    x.ResetToken == token &&
                    x.ResetTokenExpiration.HasValue &&
                    x.ResetTokenExpiration.Value > DateTime.Now);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por token de recuperación.", ex);
            }
        }

        #endregion


        #region Password Reset

        /// <summary>
        /// Guarda el token de recuperación y su vencimiento.
        /// </summary>
        public void SavePasswordResetToken(Guid userId, string token, DateTime expiration)
        {
            try
            {
                var list = LoadAll();

                var user = list.FirstOrDefault(x => x.UserId == userId);
                if (user == null)
                    throw new Exception("El usuario no existe.");

                user.ResetToken = token;
                user.ResetTokenExpiration = expiration;

                SaveAll(list);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar token de recuperación.", ex);
            }
        }

        #endregion
    }
}
