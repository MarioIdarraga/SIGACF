using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Contracts
{
    public interface IPayRepository 
    {
        void Insert(Pay obj);
        void Update(int id, Pay obj);
        void Delete(int id);
        IEnumerable<Pay> GetAll();
        IEnumerable<Pay> GetAll(DateTime? registrationSincePay, DateTime? registrationUntilPay);
        Pay GetOne(int id);

    }
}
