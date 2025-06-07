using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DAL.Contracts;


namespace BLL.Service
{
    public class PayService
    {
        private readonly IPayRepository _repo;

        public PayService(IPayRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Pay> GetPayments(DateTime? registrationSince, DateTime? registrationUntil)
        {
            return _repo.GetAll(registrationSince, registrationUntil);
        }
    }
}
