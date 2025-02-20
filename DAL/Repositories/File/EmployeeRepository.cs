using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using Domain;

namespace DAL.Repositories.File
{
    internal class EmployeeRepository : IGenericRepository<Employee>
    {
        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetOne(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Employee Object)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Employee Object)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Employee> IGenericRepository<Employee>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
