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
    /// Repositorio SQL Server para la gestión de entidades Field.
    /// Permite realizar operaciones CRUD y obtener listados.
    /// </summary>
    class FieldRepository : IGenericRepository<Field>
    {

        #region Statements

        /// <summary>Sentencia SQL para insertar un nuevo registro de cancha.</summary>
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[Fields] (Name, Capacity, FieldType, HourlyCost, IdFieldState, DVH ) VALUES (@Name, @Capacity, @FieldType, @HourlyCost, @IdFieldState, @DVH)";
        }

        /// <summary>Sentencia SQL para actualizar una cancha existente.</summary>
        private string UpdateStatement
        {
            get => "UPDATE [dbo].[Fields] SET Name = @Name, Capacity = @Capacity, FieldType = @FieldType, HourlyCost = @HourlyCost, IdFieldState = @IdFieldState, DVH = @DVH WHERE IdField = @IdField";
        }

        /// <summary>Sentencia SQL para eliminar una cancha.</summary>
        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[Fields] WHERE IdField = @IdField";
        }

        /// <summary>Sentencia SQL para obtener una cancha por Id.</summary>
        private string SelectOneStatement
        {
            get => "SELECT IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState, DVH FROM [dbo].[Fields] WHERE IdField = @IdField";
        }

        /// <summary>Sentencia SQL para obtener todas las canchas.</summary>
        private string SelectAllStatement
        {
            get => "SELECT IdField, Name, Capacity, FieldType, HourlyCost, IdFieldState, DVH  FROM [dbo].[Fields]";
        }
        #endregion

        #region Methods

        /// <summary>
        /// Elimina una cancha según su identificador.
        /// </summary>
        /// <param name="Id">Identificador de la cancha.</param>
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
        /// Obtiene una cancha por Id.
        /// </summary>
        /// <param name="Id">Identificador de la cancha.</param>
        /// <returns>Instancia de Field o null si no existe.</returns>
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

        /// <summary>
        /// Inserta una nueva cancha en la base de datos.
        /// </summary>
        /// <param name="Object">Instancia de Field a insertar.</param>
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
                    new SqlParameter("@IdFieldState", Object.IdFieldState),
                    new SqlParameter("@DVH", Object.DVH)
                }
            );
        }

        /// <summary>
        /// Actualiza los datos de una cancha existente.
        /// </summary>
        /// <param name="Id">Identificador de la cancha.</param>
        /// <param name="Object">Instancia con los datos actualizados.</param>
        public void Update(Guid Id, Field Object)
        {
            SqlHelper.ExecuteNonQuery(
                UpdateStatement,
                CommandType.Text,
                new SqlParameter[]
                {
                    new SqlParameter("@IdField", Id),
                    new SqlParameter("@Name", Object.Name),
                    new SqlParameter("@Capacity", Object.Capacity),
                    new SqlParameter("@FieldType", Object.FieldType),
                    new SqlParameter("@HourlyCost", Object.HourlyCost),
                    new SqlParameter("@IdFieldState", Object.IdFieldState),
                    new SqlParameter("@DVH", Object.DVH),
                }
            );
        }

        /// <summary>
        /// Obtiene todas las canchas almacenadas.
        /// </summary>
        /// <returns>Listado de Field.</returns>
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

        /// <summary>
        /// No implementado para esta entidad.
        /// </summary>
        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// No implementado para esta entidad.
        /// </summary>
        public IEnumerable<Field> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// No implementado para esta entidad.
        /// </summary>
        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// No implementado para esta entidad.
        /// </summary>
        public IEnumerable<Field> GetAll(DateTime? from, DateTime? to, int state)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}


