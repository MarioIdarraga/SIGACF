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
    internal class UserRepository : IUserRepository<User>
    {
        List<User> user = new List<User>();

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Users] (UserId, LoginName, Password, NroDocument, FirstName, LastName, Position, Mail, Address, Telephone, State, DVH) VALUES (@UserId, @LoginName, @Password, @NroDocument, @FirstName, @LastName, @Position, @Mail, @Address, @Telephone, @State, @DVH)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Users] SET LoginName = @LoginName, Password = @Password, NroDocument = @NroDocument, FirstName = @firstName, LastName = @LastName, Position = @Position, Mail = @Mail, Address = @Address, Telephone = @Telephone, State = @State, DVH = @DVH WHERE UserId = @UserId";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Users] WHERE UserId = @UserId";
        }

        private string SelectOneStatement
        {
            get => "SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position, Mail, Address, Telephone, State, DVH FROM [dbo].[Users] WHERE UserId = @UserId";
        }

        private string SelectAllStatement
        {
            get => "SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position, Telephone, Mail, Address, State, DVH FROM [dbo].[Users]";
        }
        #endregion


        #region Constructors
        public void Delete(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            var users = new List<User>();
            string query = SelectAllStatement + " WHERE 1=1"; // Se usa WHERE 1=1 para facilitar concatenación de condiciones
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

            using (var reader = SqlHelper.ExecuteReader(query, System.Data.CommandType.Text, parameters.ToArray()))
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("No hay datos disponibles.");
                    return users;  // Retorna una lista vacía
                }

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserId = reader.GetGuid(0),   
                        LoginName = reader.GetString(1), 
                        Password = reader.GetString(2),
                        NroDocument = reader.GetInt32(3),
                        FirstName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),   
                        LastName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),  
                        Position = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),   
                        Telephone = reader.IsDBNull(7) ? string.Empty : reader.GetString(7), 
                        Mail = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),   
                        Address = reader.IsDBNull(9) ? string.Empty : reader.GetString(9), 
                        State = reader.GetInt32(10),
                        DVH = reader.GetString(11) 
                    });
                }
            }
            return users;
        }

        public IEnumerable<User> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public User GetOne(Guid UserId)
        {
            User user = null;

            using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text, new SqlParameter("@UserId", UserId)))
            {
                if (dr.Read())
                {
                    user = new User
                    {
                        
                        UserId = dr.GetGuid(dr.GetOrdinal("UserId")),  
                                                                    
                    };
                }
            }

            return user;
        }

        public void Insert(User Object)
        {
            
            SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text,
                new SqlParameter[] {    new SqlParameter("@UserId", Object.UserId),
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
                                        new SqlParameter("@DVH", Object.DVH)
                                    });
        }

        public void Update(Guid UserId, User Object)
        {

            SqlHelper.ExecuteNonQuery(UpdateStatement, System.Data.CommandType.Text,
                new SqlParameter[] {    new SqlParameter("@UserId", UserId),
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
                                        new SqlParameter("@DVH", Object.DVH)
                                    });
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();

            using (var reader = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
            {
                while (reader.Read())
                {
                    users.Add(new User
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
                        DVH = reader.GetString(11)
                    });
                }
            }

            return users;
        }
        public User GetByLoginName(string loginName)
        {
            const string query = "SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position, Mail, Address, Telephone, State, DVH " +
                                 "FROM [dbo].[Users] WHERE LoginName = @LoginName";

            var parameters = new SqlParameter[] { new SqlParameter("@LoginName", loginName) };

            using (var reader = SqlHelper.ExecuteReader(query, CommandType.Text, parameters))
            {
                if (reader.Read())
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
                        DVH = reader.GetString(11)
                    };
                }
            }

            return null;
        }

        IEnumerable<User> IUserRepository<User>.GetAll()
        {
            return GetAll();
        }
    }
    #endregion
}
