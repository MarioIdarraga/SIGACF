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
        private string InsertStatement => "INSERT INTO [dbo].[Customer] (NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address) VALUES (@NroDocument, @FirstName, @LastName, @State, @Comment, @Telephone, @Mail, @Address)";
        private string UpdateStatement => "UPDATE [dbo].[Customer] SET NroDocument = @NroDocument, FirstName = @FirstName, LastName = @LastName, State = @State, Comment = @Comment, Telephone = @Telephone, Mail = @Mail, Address = @Address WHERE IdCustomer = @IdCustomer";
        private string DeleteStatement => "DELETE FROM [dbo].[Customer] WHERE IdCustomer = @IdCustomer";
        private string SelectOneStatement => "SELECT IdCustomer, NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address FROM [dbo].[Customer] WHERE IdCustomer = @IdCustomer";
        private string SelectAllStatement => "SELECT IdCustomer, NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address FROM [dbo].[Customer]";
        #endregion

        #region Methods
        public void Delete(Guid Id)
        {
            SqlHelper.ExecuteNonQuery(DeleteStatement, System.Data.CommandType.Text,
                new SqlParameter[] { new SqlParameter("@IdCustomer", Id) });
        }

        public Customer GetOne(Guid Id)
        {
            using (var reader = SqlHelper.ExecuteReader(SelectOneStatement, System.Data.CommandType.Text,
                new SqlParameter[] { new SqlParameter("@IdCustomer", Id) }))
            {
                if (reader.Read())
                {
                    return new Customer
                    {
                        IdCustomer = reader.GetGuid(0),
                        NroDocument = reader.GetInt32(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        State = reader.GetInt32(4),
                        Comment = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Telephone = reader.GetString(6),
                        Mail = reader.GetString(7),
                        Address = reader.GetString(8)
                    };
                }
            }
            return null;
        }

        public void Insert(Customer Object)
        {
            SqlHelper.ExecuteNonQuery(InsertStatement, System.Data.CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@NroDocument", Object.NroDocument),
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
            SqlHelper.ExecuteNonQuery(UpdateStatement, System.Data.CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@IdCustomer", Id),
                    new SqlParameter("@NroDocument", Object.NroDocument),
                    new SqlParameter("@FirstName", Object.FirstName),
                    new SqlParameter("@LastName", Object.LastName),
                    new SqlParameter("@State", Object.State),
                    new SqlParameter("@Comment", Object.Comment),
                    new SqlParameter("@Telephone", Object.Telephone),
                    new SqlParameter("@Mail", Object.Mail),
                    new SqlParameter("@Address", Object.Address)
                });
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = new List<Customer>();
            using (var reader = SqlHelper.ExecuteReader(SelectAllStatement, System.Data.CommandType.Text))
            {
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        IdCustomer = reader.GetGuid(0),
                        NroDocument = reader.GetInt32(1),
                        FirstName = reader.GetString(2),
                        LastName = reader.GetString(3),
                        State = reader.GetInt32(4),
                        Comment = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Telephone = reader.GetString(6),
                        Mail = reader.GetString(7),
                        Address = reader.GetString(8)
                    });
                }
            }
            return customers;
        }
        #endregion Metodos
    }
}
