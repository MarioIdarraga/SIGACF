using DAL.Contracts;
using DAL.Tools;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL Server para la entidad <see cref="CustomerState"/>.
    /// Se encarga de obtener los estados de cliente desde la base de datos,
    /// utilizando consultas SQL seguras y estructuradas.
    /// </summary>
    internal class CustomerStateRepository : ICustomerStateRepository
    {

        #region Statements

        /// <summary>
        /// Sentencia SQL para obtener todos los estados de cliente.
        /// </summary>
        private string SelectAllStatement =>
            @"SELECT IdCustomerState, Description
              FROM [dbo].[CustomerStates]
              ORDER BY IdCustomerState";

        /// <summary>
        /// Sentencia SQL para obtener un estado de cliente por su ID.
        /// </summary>
        private string SelectOneStatement =>
            @"SELECT IdCustomerState, Description
              FROM [dbo].[CustomerStates]
              WHERE IdCustomerState = @IdCustomerState";

        #endregion


        /// <summary>
        /// Obtiene todos los estados de cliente almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de <see cref="CustomerState"/>.</returns>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al consultar los estados.
        /// </exception>
        public List<CustomerState> GetAll()
        {
            var list = new List<CustomerState>();

            try
            {
                using (var reader = SqlHelper.ExecuteReader(
                    SelectAllStatement,
                    CommandType.Text,
                    new SqlParameter[] { }))
                {
                    while (reader.Read())
                    {
                        list.Add(new CustomerState
                        {
                            IdCustomerState = reader.GetInt32(0),
                            Description = reader.GetString(1)
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener los estados de cliente.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al obtener los estados de cliente.", ex);
            }

            return list;
        }


        /// <summary>
        /// Obtiene un estado de cliente según su identificador.
        /// </summary>
        /// <param name="id">ID del estado a buscar.</param>
        /// <returns>
        /// Instancia de <see cref="CustomerState"/> o <c>null</c> si no existe.
        /// </returns>
        /// <exception cref="Exception">
        /// Se lanza cuando ocurre un error al consultar el estado.
        /// </exception>
        public CustomerState GetById(int id)
        {
            CustomerState state = null;

            try
            {
                using (var reader = SqlHelper.ExecuteReader(
                    SelectOneStatement,
                    CommandType.Text,
                    new SqlParameter[]
                    {
                        new SqlParameter("@IdCustomerState", id)
                    }))
                {
                    if (reader.Read())
                    {
                        state = new CustomerState
                        {
                            IdCustomerState = reader.GetInt32(0),
                            Description = reader.GetString(1)
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al obtener el estado de cliente por ID.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error inesperado al obtener el estado de cliente por ID.", ex);
            }

            return state;
        }
    }
}
