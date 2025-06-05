using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    internal interface IGenericBusinessService<T>
    {
        void Insert(T obj);

        void Delete(Guid obj);


        void Update(T obj);

        T GetOne(Guid id);

        IEnumerable<T> GetAll();

    }
}
