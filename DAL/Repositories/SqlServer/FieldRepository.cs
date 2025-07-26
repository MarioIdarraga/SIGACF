using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    class FieldRepository : IGenericRepository<Field>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Fields] (Name, Capacity, FieldType, HourlyCost, IdFieldState, DVH ) VALUES (@Name, @Capacity, @FieldType, @HourlyCost, @IdFieldState, @DVH)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Fields] SET (IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState, DVH ) WHERE @IdField = @IdField";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Fields] WHERE @IdField = @IdField";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState, DVH FROM [dbo].[Fields] WHERE IdField = @IdField";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState, DVH  FROM [dbo].[Fields]";
        }
        #endregion

        #region Methods


        public void Delete(Guid Id)
        {
            SqlHelper.ExecuteNonQuery(
                DeleteStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@IdField", Id)
                }
            );
        }

        
        public Field GetOne(Guid Id)
        {
            using (var reader = SqlHelper.ExecuteReader(
                SelectOneStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@IdField", Id)
                }))
            {
                if (reader.Read())
                {
                    return new Field
                    {
                        IdField = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Capacity = reader.GetInt32(2),
                        FieldType = reader.GetInt32(3),
                        HourlyCost = reader.GetDecimal(4), 
                        IdFieldState = reader.GetInt32(5),
                        DVH = reader.GetString(6)
                    };
                }
            }
            return null;
        }

        public void Insert(Field Object)
        {
            SqlHelper.ExecuteNonQuery(
                InsertStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@Name", Object.Name),
                    new SqlParameter("@Capacity", Object.Capacity),
                    new SqlParameter("@FieldType", Object.FieldType),
                    new SqlParameter("@HourlyCost", Object.HourlyCost),
                    new SqlParameter("@IdFieldState", Object.IdFieldState)
                }
            );
        }

        public void Update(Guid Id, Field Object)
        {
            SqlHelper.ExecuteNonQuery(
                UpdateStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@Name", Object.Name),
                    new SqlParameter("@Capacity", Object.Capacity),
                    new SqlParameter("@FieldType", Object.FieldType),
                    new SqlParameter("@HourlyCost", Object.HourlyCost),
                    new SqlParameter("@IdFieldState", Object.IdFieldState)
                }
            );
        }

        public IEnumerable<Field> GetAll()
        {
            var list = new List<Field>();
            using (var reader = SqlHelper.ExecuteReader(
                SelectAllStatement,
                CommandType.Text))
            {
                while (reader.Read())
                {
                    list.Add(new Field
                    {
                        IdField = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Capacity = reader.GetInt32(2),
                        FieldType = reader.GetInt32(3),
                        HourlyCost = reader.GetDecimal(4),
                        IdFieldState = reader.GetInt32(5),
                        DVH = reader.GetString(6)
                    });
                }
            }
            return list;
        }

        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Field> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

