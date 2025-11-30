using DAL.Contracts;
using DAL;
using Domain;
using System.Data.SqlClient;
using DAL.Tools;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL Server responsable del acceso a datos de la entidad User.
    /// Incluye operaciones CRUD, recuperación de contraseña y manejo de intentos fallidos.
    /// </summary>
    internal class UserRepository : IUserRepository<User>
    {
        List<User> user = new List<User>();

        #region Statements

        private string InsertStatement
        {
            get =>
                @"INSERT INTO [dbo].[Users]
                (UserId, LoginName, Password, NroDocument, FirstName, LastName, Position,
                 Mail, Address, Telephone, State, DVH, ResetToken, ResetTokenExpiration, FailedAttempts)
                VALUES
                (@UserId, @LoginName, @Password, @NroDocument, @FirstName, @LastName, @Position,
                 @Mail, @Address, @Telephone, @State, @DVH, @ResetToken, @ResetTokenExpiration, @FailedAttempts)";
        }

        private string UpdateStatement
        {
            get =>
                @"UPDATE [dbo].[Users] SET
                    LoginName = @LoginName,
                    Password = @Password,
                    NroDocument = @NroDocument,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Position = @Position,
                    Mail = @Mail,
                    Address = @Address,
                    Telephone = @Telephone,
                    State = @State,
                    DVH = @DVH,
                    ResetToken = @ResetToken,
                    ResetTokenExpiration = @ResetTokenExpiration,
                    FailedAttempts = @FailedAttempts
                  WHERE UserId = @UserId";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Users] WHERE UserId = @UserId";
        }

        private string SelectOneStatement
        {
            get =>
                @"SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position,
                         Mail, Address, Telephone, State, DVH, ResetToken, ResetTokenExpiration, FailedAttempts
                  FROM [dbo].[Users]
                  WHERE UserId = @UserId";
        }

        private string SelectAllStatement
        {
            get =>
                @"SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position,
                         Telephone, Mail, Address, State, DVH, ResetToken, ResetTokenExpiration, FailedAttempts
                  FROM [dbo].[Users]";
        }

        private string SelectByLoginNameStatement
        {
            get =>
                @"SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position,
                         Mail, Address, Telephone, State, DVH, ResetToken, ResetTokenExpiration, FailedAttempts
                  FROM [dbo].[Users]
                  WHERE LoginName = @LoginName";
        }

        private string SelectByUserOrMailStatement
        {
            get =>
                @"SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position,
                         Mail, Address, Telephone, State, DVH, ResetToken, ResetTokenExpiration, FailedAttempts
                  FROM [dbo].[Users]
                  WHERE LoginName = @Value OR Mail = @Value";
        }

        private string UpdateResetTokenStatement
        {
            get =>
                @"UPDATE [dbo].[Users]
                  SET ResetToken = @Token, ResetTokenExpiration = @Expiration
                  WHERE UserId = @UserId";
        }

        private string SelectByResetTokenStatement
        {
            get =>
                @"SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position,
                         Mail, Address, Telephone, State, DVH, ResetToken, ResetTokenExpiration, FailedAttempts
                  FROM [dbo].[Users]
                  WHERE ResetToken = @Token AND ResetTokenExpiration > GETDATE()";
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        public void Insert(User Object)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(InsertStatement, CommandType.Text,
                    new SqlParameter[]
                    {
                        new SqlParameter("@UserId", Object.UserId),
                        new SqlParameter("@LoginName", Object.LoginName),
                        new SqlParameter("@Password", Object.Password),
                        new SqlParameter("@NroDocument", Object.NroDocument),
                        new SqlParameter("@FirstName", Object.FirstName),
                        new SqlParameter("@LastName", Object.LastName),
                        new SqlParameter("@Position", Object.Position),
                        new SqlParameter("@Mail", Object.Mail),
                        new SqlParameter("@Address", Object.Address),
                        new SqlParameter("@Telephone", Object.Telephone),
                        new SqlParameter("@State", Object.State),
                        new SqlParameter("@DVH", Object.DVH),
                        new SqlParameter("@ResetToken", (object)Object.ResetToken ?? DBNull.Value),
                        new SqlParameter("@ResetTokenExpiration", (object)Object.ResetTokenExpiration ?? DBNull.Value),
                        new SqlParameter("@FailedAttempts", Object.FailedAttempts)
                    });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza un usuario existente en la base de datos.
        /// </summary>
        public void Update(Guid UserId, User Object)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text,
                    new SqlParameter[]
                    {
                        new SqlParameter("@UserId", UserId),
                        new SqlParameter("@LoginName", Object.LoginName),
                        new SqlParameter("@Password", Object.Password),
                        new SqlParameter("@NroDocument", Object.NroDocument),
                        new SqlParameter("@FirstName", Object.FirstName),
                        new SqlParameter("@LastName", Object.LastName),
                        new SqlParameter("@Position", Object.Position),
                        new SqlParameter("@Mail", Object.Mail),
                        new SqlParameter("@Address", Object.Address),
                        new SqlParameter("@Telephone", Object.Telephone),
                        new SqlParameter("@State", Object.State),
                        new SqlParameter("@DVH", Object.DVH),
                        new SqlParameter("@ResetToken", (object)Object.ResetToken ?? DBNull.Value),
                        new SqlParameter("@ResetTokenExpiration", (object)Object.ResetTokenExpiration ?? DBNull.Value),
                        new SqlParameter("@FailedAttempts", Object.FailedAttempts)
                    });
            }
            catch
            {
                throw;
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
                var users = new List<User>();

                using (var reader = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        users.Add(MapUser(reader));
                    }
                }

                return users;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<User> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene usuarios aplicando filtros.
        /// </summary>
        public IEnumerable<User> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            var users = new List<User>();
            string query = SelectAllStatement + " WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (nroDocument.HasValue)
            {
                query += " AND NroDocument = @NroDocument";
                parameters.Add(new SqlParameter("@NroDocument", nroDocument.Value));
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                query += " AND FirstName LIKE @FirstName";
                parameters.Add(new SqlParameter("@FirstName", "%" + firstName + "%"));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query += " AND LastName LIKE @LastName";
                parameters.Add(new SqlParameter("@LastName", "%" + lastName + "%"));
            }

            if (!string.IsNullOrEmpty(telephone))
            {
                query += " AND Telephone LIKE @Telephone";
                parameters.Add(new SqlParameter("@Telephone", "%" + telephone + "%"));
            }

            if (!string.IsNullOrEmpty(mail))
            {
                query += " AND Mail LIKE @Mail";
                parameters.Add(new SqlParameter("@Mail", "%" + mail + "%"));
            }

            using (var reader = SqlHelper.ExecuteReader(query, CommandType.Text, parameters.ToArray()))
            {
                while (reader.Read())
                {
                    users.Add(MapUser(reader));
                }
            }

            return users;
        }

        /// <summary>
        /// Obtiene un usuario por su UserId.
        /// </summary>
        public User GetOne(Guid UserId)
        {
            try
            {
                User user = null;

                using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
                         new SqlParameter("@UserId", UserId)))
                {
                    if (dr.Read())
                    {
                        user = MapUser(dr);
                    }
                }

                return user;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene un usuario por su LoginName.
        /// </summary>
        public User GetByLoginName(string loginName)
        {
            try
            {
                using (var reader = SqlHelper.ExecuteReader(SelectByLoginNameStatement,
                        CommandType.Text,
                        new SqlParameter("@LoginName", loginName)))
                {
                    if (reader.Read())
                    {
                        return MapUser(reader);
                    }
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Recuperación de contraseña

        /// <summary>
        /// Obtiene un usuario por LoginName o Email.
        /// </summary>
        public User GetByUsernameOrEmail(string userOrMail)
        {
            try
            {
                using (var reader = SqlHelper.ExecuteReader(SelectByUserOrMailStatement,
                        CommandType.Text,
                        new SqlParameter("@Value", userOrMail)))
                {
                    if (reader.Read())
                    {
                        return MapUser(reader);
                    }
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda el token de recuperación de contraseña.
        /// </summary>
        public void SavePasswordResetToken(Guid userId, string token, DateTime expiration)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(UpdateResetTokenStatement,
                    CommandType.Text,
                    new SqlParameter[]
                    {
                        new SqlParameter("@UserId", userId),
                        new SqlParameter("@Token", token),
                        new SqlParameter("@Expiration", expiration)
                    });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene un usuario válido mediante token de recuperación.
        /// </summary>
        public User GetByPasswordResetToken(string token)
        {
            try
            {
                using (var reader = SqlHelper.ExecuteReader(SelectByResetTokenStatement,
                        CommandType.Text,
                        new SqlParameter("@Token", token)))
                {
                    if (reader.Read())
                    {
                        return MapUser(reader);
                    }
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Mapea una fila de SQL a un objeto User.
        /// </summary>
        private User MapUser(SqlDataReader reader)
        {
            return new User
            {
                UserId = reader.GetGuid(0),
                LoginName = reader.GetString(1),
                Password = reader.GetString(2),
                NroDocument = reader.GetInt32(3),
                FirstName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                LastName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                Position = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                Mail = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                Address = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                Telephone = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                State = reader.GetInt32(10),
                DVH = reader.GetString(11),
                ResetToken = reader.IsDBNull(12) ? null : reader.GetString(12),
                ResetTokenExpiration = reader.IsDBNull(13) ? (DateTime?)null : reader.GetDateTime(13),
                FailedAttempts = reader.IsDBNull(14) ? 0 : reader.GetInt32(14)
            };
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
