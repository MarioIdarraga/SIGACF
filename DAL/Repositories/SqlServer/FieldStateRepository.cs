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
    class FieldStateRepository : IGenericRepository<FieldState>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[FieldStates] (FieldStateDescription) VALUES (StateDescription)";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[FieldStates] SET (FieldStateDescription) WHERE IdFieldState = @IdFieldState";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[FieldStates] WHERE IdFieldState = @IdFieldState";
        }

        private string SelectOneStatement
        {
            get => "SELECT IdFieldState, FieldStateDescription FROM [dbo].[FieldStates] WHERE IdFieldState = @IdFieldState";
        }

        private string SelectAllStatement
        {
            get => "SELECT IdFieldState, FieldStateDescription FROM [dbo].[FieldStates]";
        }
        #endregion

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

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

        public IEnumerable<FieldState> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FieldState> GetAll(int? nroDocument, DateTime? registrationBooking, DateTime? registrationDate)
        {
            throw new NotImplementedException();
        }

        public FieldState GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(FieldState Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, FieldState Object)
        {
            throw new NotImplementedException();
        }
    }
}
