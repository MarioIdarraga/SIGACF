using DAL.Contracts;
using DAL.Tools;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL Server para la entidad PaymentMethod.
    /// Provee acceso a la consulta de métodos de pago.
    /// </summary>
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        /// <summary>
        /// Sentencia SQL para obtener todos los métodos de pago.
        /// </summary>
        private string SelectAllStatement =>
            "SELECT IdPayMethod, Description FROM PaymentMethods";

        /// <summary>
        /// Sentencia SQL para obtener un método de pago por su identificador.
        /// </summary>
        private string SelectOneStatement =>
            "SELECT IdPayMethod, Description FROM PaymentMethods WHERE IdPayMethod = @Id";

        /// <summary>
        /// Obtiene todos los métodos de pago disponibles en la base de datos.
        /// </summary>
        /// <returns>Colección de métodos de pago.</returns>
        public IEnumerable<PaymentMethod> GetAll()
        {
            try
            {
                var list = new List<PaymentMethod>();

                using (var reader = SqlHelper.ExecuteReader(
                    SelectAllStatement,
                    CommandType.Text))
                {
                    while (reader.Read())
                    {
                        list.Add(new PaymentMethod
                        {
                            IdPayMethod = reader.GetInt32(0),
                            Description = reader.GetString(1)
                        });
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener métodos de pago.", ex);
            }
        }

        /// <summary>
        /// Obtiene un método de pago por su identificador.
        /// </summary>
        /// <param name="id">Identificador del método de pago.</param>
        /// <returns>Instancia de PaymentMethod o null si no existe.</returns>
        public PaymentMethod GetOne(int id)
        {
            try
            {
                using (var reader = SqlHelper.ExecuteReader(
                    SelectOneStatement,
                    CommandType.Text,
                    new SqlParameter("@Id", id)))
                {
                    if (reader.Read())
                    {
                        return new PaymentMethod
                        {
                            IdPayMethod = reader.GetInt32(0),
                            Description = reader.GetString(1)
                        };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el método de pago.", ex);
            }
        }
    }
}

