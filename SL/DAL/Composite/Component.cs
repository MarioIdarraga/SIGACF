using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.DAL.Composite
{
    public abstract class Component
    {
        public Guid IdComponent { get; set; }

        public Component()
        {
        }

        public abstract int GetChild();

        public abstract void Add(Component component);

        public abstract void Remove(Component component);

    }
}
