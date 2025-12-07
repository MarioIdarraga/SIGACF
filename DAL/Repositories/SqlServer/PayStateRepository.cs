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
    /// Repositorio SQL para la entidad PayState.
    /// Administra la obtención de estados de pago desde la base de datos.
    /// </summary>
    internal class PayStateRepository : IPayStateRepository
    {
        #region Statements

        private string SelectAllStatement =>
            "SELECT [IdPayState], [Description] FROM [dbo].[PayStates]";

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene todos los estados de pago.
        /// </summary>
        public IEnumerable<PayState> GetAll()
        {
            var list = new List<PayState>();

            try
            {
                using (var reader = SqlHelper.ExecuteReader(
                    SelectAllStatement,
                    CommandType.Text))
                {
                    while (reader.Read())
                        list.Add(MapPayState(reader));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los estados de pago.", ex);
            }
        }

        /// <summary>
        /// Mapea un SqlDataReader a la entidad PayState.
        /// </summary>
        private PayState MapPayState(SqlDataReader reader)
        {
            return new PayState
            {
                IdPayState = reader.GetInt32(0),
                Description = reader.GetString(1)
            };
        }

        #endregion
    }
}
