using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    public class CustomerRepository : IGenericRepository<Customer>
    {

        #region Statements
        private string InsertStatement => "INSERT INTO [dbo].[Customers] (NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address) VALUES (@NroDocument, @FirstName, @LastName, @State, @Comment, @Telephone, @Mail, @Address)";
        private string UpdateStatement => "UPDATE [dbo].[Customers] SET NroDocument = @NroDocument, FirstName = @FirstName, LastName = @LastName, State = @State, Comment = @Comment, Telephone = @Telephone, Mail = @Mail, Address = @Address WHERE IdCustomer = @IdCustomer";
        private string DeleteStatement => "DELETE FROM [dbo].[Customers] WHERE IdCustomer = @IdCustomer";
        private string SelectOneStatement => "SELECT IdCustomer, NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address FROM [dbo].[Customers] WHERE IdCustomer = @IdCustomer";
        private string SelectAllStatement => "SELECT IdCustomer, NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address FROM [dbo].[Customers]";
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

        public IEnumerable<Customer> GetAll(int? nroDocument = null, string firstName = null, string lastName = null, string telephone = null, string mail = null)
        {
            var customers = new List<Customer>();
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

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            var customers = new List<Customer>();
            string query = SelectAllStatement + " WHERE 1=1";
            var parameters = new List<SqlParameter>();

            if (nroDocument.HasValue)
            {
                query += " AND NroDocument = @NroDocument";
                parameters.Add(new SqlParameter("@NroDocument", nroDocument.Value));
            }

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query += " AND FirstName LIKE @FirstName";
                parameters.Add(new SqlParameter("@FirstName", $"%{firstName}%"));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query += " AND LastName LIKE @LastName";
                parameters.Add(new SqlParameter("@LastName", $"%{lastName}%"));
            }

            if (!string.IsNullOrWhiteSpace(telephone))
            {
                query += " AND Telephone LIKE @Telephone";
                parameters.Add(new SqlParameter("@Telephone", $"%{telephone}%"));
            }

            if (!string.IsNullOrWhiteSpace(mail))
            {
                query += " AND Mail LIKE @Mail";
                parameters.Add(new SqlParameter("@Mail", $"%{mail}%"));
            }

            using (var reader = SqlHelper.ExecuteReader(query, CommandType.Text, parameters.ToArray()))
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
                        Telephone = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Mail = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Address = reader.IsDBNull(8) ? null : reader.GetString(8)
                    });
                }
            }

            return customers;
        }

        #endregion Metodos
    }
}
