using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.Memory
{
    class FieldRepository : IGenericRepository<Field>
    {

        #region Statements
        private string InsertStatement
        {
            get => "INSERT INTO [dbo].[] () VALUES ()";
        }

        private string UpdateStatement
        {
            get => "UPDATE [dbo].[] SET () WHERE  = @";
        }

        private string DeleteStatement
        {
            get => "DELETE FROM [dbo].[] WHERE  = @";
        }

        private string SelectOneStatement
        {
            get => "SELECT ,  FROM [dbo].[] WHERE  = @";
        }

        private string SelectAllStatement
        {
            get => "SELECT ,  FROM [dbo].[]";
        }
        #endregion

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Field> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Field> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public Field GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Field Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Field Object)
        {
            throw new NotImplementedException();
        }
    }
}
