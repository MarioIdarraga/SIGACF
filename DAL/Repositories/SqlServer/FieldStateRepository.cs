using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Tools;
using Domain;

namespace DAL.Repositories.SqlServer
{
    /// <summary>
    /// Repositorio SQL Server de estados de cancha (FieldState).
    /// Provee métodos CRUD y consultas correspondientes.
    /// </summary>
    class FieldStateRepository : IGenericRepository<FieldState>
    {

        #region Statements

        /// <summary>
        /// Sentencia SQL para insertar un nuevo registro de estado de cancha.
        /// </summary>
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[FieldStates] (FieldStateDescription) VALUES (@FieldStateDescription)";
        }

        /// <summary>
        /// Sentencia SQL para actualizar un estado de cancha existente.
        /// </summary>
        private string UpdateStatement
        {
            get => "UPDATE [dbo].[FieldStates] SET FieldStateDescription = @FieldStateDescription WHERE IdFieldState = @IdFieldState";
        }

        /// <summary>
        /// Sentencia SQL para eliminar un estado de cancha.
        /// </summary>
        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[FieldStates] WHERE IdFieldState = @IdFieldState";
        }

        /// <summary>
        /// Sentencia SQL para obtener un estado de cancha por Id.
        /// </summary>
        private string SelectOneStatement
        {
            get => "SELECT IdFieldState, FieldStateDescription FROM [dbo].[FieldStates] WHERE IdFieldState = @IdFieldState";
        }

        /// <summary>
        /// Sentencia SQL para obtener todos los estados de cancha.
        /// </summary>
        private string SelectAllStatement
        {
            get => "SELECT IdFieldState, FieldStateDescription FROM [dbo].[FieldStates]";
        }

        #endregion


        /// <summary>
        /// Elimina un estado de cancha por su identificador.
        /// </summary>
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene todos los estados de cancha disponibles.
        /// </summary>
        public IEnumerable<FieldState> GetAll()
        {
            var list = new List<FieldState>();

            using (var reader = SqlHelper.ExecuteReader(
                SelectAllStatement,
                CommandType.Text))
            {
                while (reader.Read())
                {
                    list.Add(new FieldState
                    {
                        IdFieldState = reader.GetInt32(0),
                        FieldStateDescription = reader.GetString(1),
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// No implementado para esta entidad.
        /// </summary>
        public IEnumerable<FieldState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// No implementado para esta entidad.
        /// </summary>
        public IEnumerable<FieldState> GetAll(DateTime? from, DateTime? to, int state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene un estado de cancha por Id.
        /// </summary>
        public FieldState GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserta un nuevo estado de cancha.
        /// </summary>
        public void Insert(FieldState Object)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Actualiza un estado de cancha existente.
        /// </summary>
        public void Update(Guid Id, FieldState Object)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// No implementado para esta entidad.
        /// </summary>
        IEnumerable<FieldState> IGenericRepository<FieldState>.GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail, int state)
        {
            throw new NotImplementedException();
        }
    }
}
