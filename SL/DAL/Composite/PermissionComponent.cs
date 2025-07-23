using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SL.Composite
{
    public abstract class PermissionComponent : IDVH
    {
        public Guid IdComponent { get; set; }
        
        public PermissionComponent()
        {
        }

        public abstract int GetChild();

        public abstract void Add(PermissionComponent component);

        public abstract void Remove(PermissionComponent component);

        public string DVH { get; set; }

    }
}
