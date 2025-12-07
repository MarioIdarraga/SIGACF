using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ICustomerStateRepository
    {
        List<CustomerState> GetAll();
        CustomerState GetById(int id);
    }
}
