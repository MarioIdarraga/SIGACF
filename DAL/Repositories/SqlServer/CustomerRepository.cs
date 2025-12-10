using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL Server para la entidad Customer.
    /// Implementa operaciones CRUD y búsquedas filtradas.
    /// </summary>
    public class CustomerRepository : IGenericRepository<Customer>
    {

        #region Statements
        /// <summary>Sentencia SQL para insertar un nuevo cliente.</summary>
        private string InsertStatement =>
            "INSERT INTO [dbo].[Customers] (NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address) " +
            "VALUES (@NroDocument, @FirstName, @LastName, @State, @Comment, @Telephone, @Mail, @Address)";

        /// <summary>Sentencia SQL para actualizar un cliente existente.</summary>
        private string UpdateStatement =>
            "UPDATE [dbo].[Customers] SET NroDocument = @NroDocument, FirstName = @FirstName, LastName = @LastName, " +
            "State = @State, Comment = @Comment, Telephone = @Telephone, Mail = @Mail, Address = @Address " +
            "WHERE IdCustomer = @IdCustomer";

        /// <summary>Sentencia SQL para eliminar un cliente por Id.</summary>
        private string DeleteStatement =>
            "DELETE FROM [dbo].[Customers] WHERE IdCustomer = @IdCustomer";

        /// <summary>Sentencia SQL para obtener un cliente por Id.</summary>
        private string SelectOneStatement =>
            "SELECT IdCustomer, NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address " +
            "FROM [dbo].[Customers] WHERE IdCustomer = @IdCustomer";

        /// <summary>Sentencia SQL para obtener todos los clientes.</summary>
        private string SelectAllStatement =>
            "SELECT IdCustomer, NroDocument, FirstName, LastName, State, Comment, Telephone, Mail, Address FROM [dbo].[Customers]";
        #endregion

        #region Methods

        /// <summary>
        /// Elimina un cliente según su identificador.
        /// </summary>
        /// <param name="Id">Identificador del cliente.</param>
        public void Delete(Guid Id)
        {
            SqlHelper.ExecuteNonQuery(DeleteStatement, CommandType.Text,
                new SqlParameter[] { new SqlParameter("@IdCustomer", Id) });
        }

        /// <summary>
        /// Obtiene un cliente por identificador único.
        /// </summary>
        /// <param name="Id">ID del cliente.</param>
        /// <returns>Instancia de Customer o null si no existe.</returns>
        public Customer GetOne(Guid Id)
        {
            using (var reader = SqlHelper.ExecuteReader(SelectOneStatement, CommandType.Text,
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

        /// <summary>
        /// Inserta un nuevo cliente en la base de datos.
        /// </summary>
        /// <param name="Object">Objeto Customer a insertar.</param>
        public void Insert(Customer Object)
        {
            SqlHelper.ExecuteNonQuery(InsertStatement, CommandType.Text,
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

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        /// <param name="Id">Id del cliente.</param>
        /// <param name="Object">Datos actualizados.</param>
        public void Update(Guid Id, Customer Object)
        {
            SqlHelper.ExecuteNonQuery(UpdateStatement, CommandType.Text,
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

        /// <summary>
        /// Obtiene todos los clientes aplicando filtros opcionales.
        /// </summary>
        public IEnumerable<Customer> GetAll(int? nroDocument = null, string firstName = null, string lastName = null, string telephone = null, string mail = null)
        {
            var customers = new List<Customer>();
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

        /// <summary>
        /// No implementado para este repositorio.
        /// </summary>
        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// No implementado para este repositorio.
        /// </summary>
        public IEnumerable<Customer> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todos los clientes según filtros y estado.
        /// </summary>
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

        /// <summary>
        /// No implementado para este repositorio.
        /// </summary>
        public IEnumerable<Customer> GetAll(DateTime? from, DateTime? to, int state)
        {
            throw new NotImplementedException();
        }

        #endregion Metodos
    }
}
