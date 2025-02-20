using DAL.Contracts;
using Domain;
using System.Data.SqlClient;
using DAL.Tools;
using System;
using System.Collections.Generic;







namespace DAL.Repositories.SqlServer
{
    public class CustomerRepository : IGenericRepository<Customer>
    {


        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Customer] (NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address ) VALUES (@NroDocument, @FirstName, @LastName, @State, @Comment, @Telephone, @Mail, @Address)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Customer] SET NroDocument = @NroDocument, FirstName = @FirstName, LastName = @LastName, State = @State, Comment = @Comment, Telephone = @Telephone, Mail = @Mail, Address = @Address WHERE IdCustomer = @IdCustomer";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Customer] WHERE IdCustomer = @IdCustomer";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdCustomer, NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address FROM [dbo].[Customer] WHERE Id = @IdCustomer";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdCustomer, NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address FROM [dbo].[Customer]";
        }
        #endregion

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer Object)
        {
            SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text,
                new SqlParameter[] 
                {
                    new SqlParameter("@NroDocument",Object.NroDocument),
                    new SqlParameter("@FirstName", Object.FirstName),
                    new SqlParameter("@LastName", Object.LastName),
                    new SqlParameter("@State", Object.State),
                    new SqlParameter("@Comment", Object.Comment),
                    new SqlParameter("@Telephone", Object.Telephone),
                    new SqlParameter("@Mail", Object.Mail),
                    new SqlParameter("@Address", Object.Address) 
                });
        }

        public void Update(Guid Id, Customer Object)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Customer> IGenericRepository<Customer>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
