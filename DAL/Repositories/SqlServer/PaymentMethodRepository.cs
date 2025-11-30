using DAL.Contracts;
using DAL.Tools;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Repositories.SqlServer
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private string SelectAllStatement =>
            "SELECT IdPayMethod, Description FROM PaymentMethods";

        private string SelectOneStatement =>
            "SELECT IdPayMethod, Description FROM PaymentMethods WHERE IdPayMethod = @Id";

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

