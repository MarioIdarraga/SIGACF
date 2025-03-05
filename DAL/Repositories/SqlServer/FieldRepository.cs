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
            get => "INSERT INTO [dbo].[Field] (IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState ) VALUES (@Name, @Capacity, @FieldType, @HourlyCost, IdFieldState)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Field] SET (IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState ) WHERE @IdField = @IdField";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Field] WHERE @IdField = @IdField";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdField, IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState  FROM [dbo].[Field] WHERE @IdField = @IdField";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState  FROM [dbo].[Field]";
        }
        #endregion

        #region Methods

        /// <summary>
        /// Elimina un registro de la tabla [Field] según su ID.
        /// </summary>
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

        /// <summary>
        /// Devuelve un objeto Field según su ID (o null si no existe).
        /// </summary>
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
                        HourlyCost = (float)reader.GetDecimal(4),  // Se asume que es DECIMAL(10,2) en SQL
                        IdFieldState = reader.GetInt32(5)
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Inserta un nuevo registro en la tabla [Field].
        /// </summary>
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

        /// <summary>
        /// Actualiza un registro en la tabla [Field].
        /// </summary>
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

        /// <summary>
        /// Retorna todos los registros de la tabla [Field].
        /// </summary>
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
                        HourlyCost = (float)reader.GetDecimal(4),
                        IdFieldState = reader.GetInt32(5)
                    });
                }
            }
            return list;
        }

        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

