using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.DAL.Contracts
{
    internal interface IGenericBusinessService<T>
    {
        void Insert(T Object);

        void Update(Guid Id, T Object);

        void Delete(Guid Id);

        IEnumerable<T> GetAll();

        T GetOne(Guid Id);

    }
}
