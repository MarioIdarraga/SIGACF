using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CustomerStateService
    {
        private readonly ICustomerStateRepository _repo;

        public CustomerStateService(ICustomerStateRepository repo)
        {
            _repo = repo;
        }

        public List<CustomerState> GetAll() => _repo.GetAll();

        public CustomerState GetById(int id) => _repo.GetById(id);
    }
}
