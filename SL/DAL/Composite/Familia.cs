using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Composite
{
    public class Familia : PermissionComponent
    {
        private List<PermissionComponent> childrens = new List<PermissionComponent>();

        public Familia() { }
        public Familia(string nombre, PermissionComponent component)
        {
            Name = nombre;

            if (component != null)
                childrens.Add(component);
        }

        public List<PermissionComponent> GetChildren()
        {
            return childrens;
        }

        public override int GetChild()
        {
            return childrens.Count;
        }

        public override void Add(PermissionComponent component)
        {
            childrens.Add(component);
        }

        public override void Remove(PermissionComponent component)
        {
            childrens.RemoveAll(o => o.IdComponent == component.IdComponent);
        }
    }
}
