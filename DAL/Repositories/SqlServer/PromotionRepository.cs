using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    class PromotionRepository : IGenericRepository<Promotion>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Promotions] (Name, ValidFrom, ValidTo, PromotionDescription, DiscountPercentage) VALUES (@Name, @ValidFrom, @ValidTo, @PromotionDescription, @DiscountPercentage)";
        }
        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Bookings] SET IdCustomer = @IdCustomer, NroDocument = @NroDocument, RegistrationDate = @RegistrationDate, RegistrationBooking = @RegistrationBooking, StartTime = @StartTime, EndTime = @EndTime, Field = @Field, Promotion = @Promotion, State = @State WHERE IdBooking = @IdBooking";
        }


        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Promotions] WHERE IdPromotion = @IdPromotion";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdPromotion, Name, ValidFrom, ValidTo, PromotionDescription, DiscountPercentage FROM [dbo].[Promotions] WHERE IdPromotion = @IdPromotion";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdPromotion, Name, ValidFrom, ValidTo, PromotionDescription, DiscountPercentage FROM [dbo].[Promotions]";
        }
        #endregion



        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todas las promociones.
        /// </summary>
        /// <returns>Lista de promociones.</returns>
        public IEnumerable<Promotion> GetAll()
        {
            try
            {
                var list = new List<Promotion>();

                using (var reader = SqlHelper.ExecuteReader(SelectAllStatement, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        list.Add(new Promotion
                        {
                            IdPromotion = reader.GetGuid(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            ValidFrom = reader.GetDateTime(2),
                            ValidTo = reader.GetDateTime(3),
                            PromotionDescripcion = reader.IsDBNull(4) ? null : reader.GetString(4),
                            DiscountPercentage = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                        });
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las promociones.", ex);
            }
        }


        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll(DateTime? from, DateTime? to, int state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una promoción por su identificador.
        /// </summary>
        /// <param name="Id">Identificador de la promoción.</param>
        /// <returns>Instancia de la promoción o null si no existe.</returns>
        public Promotion GetOne(Guid Id)
        {
            try
            {
                using (var reader = SqlHelper.ExecuteReader(
                    SelectOneStatement,
                    CommandType.Text,
                    new SqlParameter[]
                    {
                new SqlParameter("@IdPromotion", Id)
                    }))
                {
                    if (reader.Read())
                    {
                        return new Promotion
                        {
                            IdPromotion = reader.GetGuid(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            ValidFrom = reader.GetDateTime(2),
                            ValidTo = reader.GetDateTime(3),
                            PromotionDescripcion = reader.IsDBNull(4) ? null : reader.GetString(4),
                            DiscountPercentage = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                        };
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la promoción.", ex);
            }
        }


        public void Insert(Promotion Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Promotion Object)
        {
            throw new NotImplementedException();
        }
    }
}
