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
            get => "INSERT INTO [dbo].[Users] (LoginName, Password, FirstName, LastName, Position, Email) VALUES (@LoginName, @Password, @FirstName, @LastName, @Position, @email)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Users] SET (LoginName, Password, FirstName, LastName, Position, Email) WHERE UserId = @UserId";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Users] WHERE UserId = @UserId";
        }

        private string SelectOneStatement
        {
            get => "SELECT UserId, LoginName, Password, FirstName, LastName, Position, Email FROM [dbo].[Users] WHERE UserId = @UserId";
        }

        private string SelectAllStatement
        {
            get => "SELECT UserId, LoginName, Password, FirstName, LastName, Position, Email FROM [dbo].[Users]";
        }
        #endregion

        public void Delete(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
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
                                        new SqlParameter("@FirstName", Object.FirstName),
                                        new SqlParameter("@LastName", Object.LastName),
                                        new SqlParameter("@Position", Object.Position),
                                        new SqlParameter("@Email", Object.Email)});
        
    }

       public void Update(Guid UserId, User Object)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IGenericRepository<User>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
