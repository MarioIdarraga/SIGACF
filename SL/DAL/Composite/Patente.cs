using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Composite
{
    public class Patente : PermissionComponent
    {
        public string Name { get; set; }
        public string formName { get; set; }

        public Patente() { }

        public override int GetChild()
        {
            return 0;
        }

        public override void Add(PermissionComponent component)
        {
            throw new Exception("No se puede agregar un componente");
        }

        public override void Remove(PermissionComponent component)
        {
            throw new Exception("No se puede eliminar un componente");
        }
    }
}
