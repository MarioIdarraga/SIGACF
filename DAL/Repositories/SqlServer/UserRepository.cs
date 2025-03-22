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
    internal class UserRepository : IGenericRepository<User>
    {
        List<User> user = new List<User>();

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Users] (LoginName, Password, NroDocument, FirstName, LastName, Position, Mail, Address, Telephone, State, IsEmployee) VALUES (@LoginName, @Password, @NroDocument, @FirstName, @LastName, @Position, @Email, @Address, @Telephone, @State, @IsEmployee)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Users] SET LoginName = @LoginName, Password = @Password, NroDocument = @NroDocument, FirstName = @firstName, LastName = @LastName, Position = @Position, Mail = @Mail, Address = @Address, Telephone = @Telephone, State = @State, IsEmployee = @IsEmployee WHERE UserId = @UserId";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Users] WHERE UserId = @UserId";
        }

        private string SelectOneStatement
        {
            get => "SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position, Mail, Address, Telephone, State, IsEmployee FROM [dbo].[Users] WHERE UserId = @UserId";
        }

        private string SelectAllStatement
        {
            get => "SELECT UserId, LoginName, Password, NroDocument, FirstName, LastName, Position, IsEmployee, Telephone, Mail, Address, State FROM [dbo].[Users]";
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
                        IsEmployee = !reader.IsDBNull(7) && reader.GetBoolean(7), 
                        Telephone = reader.IsDBNull(8) ? string.Empty : reader.GetString(8), 
                        Mail = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),   
                        Address = reader.IsDBNull(10) ? string.Empty : reader.GetString(10), 
                        State = reader.GetInt32(11)
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

            using (var dr = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text, new SqlParameter("@UserId", UserId), null))
            {
                if (dr.Read())
                {
                    user = new User
                    {
                        
                        UserId = dr.GetGuid(dr.GetOrdinal("IdUser")), 
                        //LoginName = dr.GetString(dr.GetOrdinal("Name")), 
                                                                    
                    };
                }
            }

            return user;
        }

        public void Insert(User Object)
        {
            
            SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text,
                new SqlParameter[] {    new SqlParameter("@LoginName", Object.LoginName),
                                        new SqlParameter("@Password", Object.Password),
                                        new SqlParameter("@NroDocument", Object.NroDocument),
                                        new SqlParameter("@FirstName", Object.FirstName),
                                        new SqlParameter("@LastName", Object.LastName),
                                        new SqlParameter("@Position", Object.Position),
                                        new SqlParameter("@Mail", Object.Mail),
                                        new SqlParameter("@Address", Object.Address),
                                        new SqlParameter("@Telephone", Object.Telephone),
                                        new SqlParameter("@State", Object.State),
                                        new SqlParameter("@IsEmployee", Object.IsEmployee ? 1 : 0)
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
                                        new SqlParameter("@IsEmployee", Object.IsEmployee ? 1 : 0)
                                    });
        }

        IEnumerable<User> IGenericRepository<User>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
