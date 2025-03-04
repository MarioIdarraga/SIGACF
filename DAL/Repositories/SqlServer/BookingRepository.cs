﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.SqlServer
{
    internal class BookingRepository : IGenericRepository<Booking>
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

        public IEnumerable<Booking> GetAll(int? nroDocument, string firstName, string lastName, string telephone, string mail)
        {
            throw new NotImplementedException();
        }

        public Booking GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Booking Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Booking Object)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Booking> IGenericRepository<Booking>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
