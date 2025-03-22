using System;
using System.Collections.Generic;
using System.Data;
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
            get => "INSERT INTO [dbo].[Promotions] (Name, ValidFrom, ValidTo, PromotionDescription) VALUES (@Name, @ValidFrom, @ValidTo, @PromotionDescription)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Promotions] SET (Name, ValidFrom, ValidTo, PromotionDescription) WHERE IdPromotion = @IdPromotion";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Promotions] WHERE IdPromotion = @IdPromotion";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdPromotion, Name, ValidFrom, ValidTo, PromotionDescription FROM [dbo].[Promotions] WHERE IdPromotion = @IdPromotion";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdPromotion, Name, ValidFrom, ValidTo, PromotionDescription FROM [dbo].[Promotions]";
        }
        #endregion



        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll()
        {
            var list = new List<Promotion>();
            using (var reader = SqlHelper.ExecuteReader(
                SelectAllStatement,
                CommandType.Text))
            {
                while (reader.Read())
                {
                    list.Add(new Promotion
                    {
                        IdPromotion = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        ValidFrom = reader.GetDateTime(2),
                        ValidTo = reader.GetDateTime(3),
                        PromotionDescripcion = reader.GetString(4),
                    });
                }
            }
            return list;
        }

        public IEnumerable<Promotion> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Promotion> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public Promotion GetOne(Guid Id)
        {
            throw new NotImplementedException();
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
