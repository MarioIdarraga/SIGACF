using DAL.Contracts;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.File
{
    internal class PayStateRepository : IPayStateRepository
    {
        public IEnumerable<PayState> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
