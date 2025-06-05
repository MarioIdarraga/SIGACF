using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace BLL.Service
{
    public class PayService
    {
        private readonly IPayRepository _repo;

        public PayService(IPayRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Pay> GetPayments(DateTime? registrationDate)
        {
            // Aquí podrías poner validaciones, formateos, etc.
            return _repo.GetAll(registrationDate);
        }
    }
}
