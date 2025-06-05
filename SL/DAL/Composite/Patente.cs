using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.DAL.Composite;

namespace SL.DAL.Composite
{
    public class Patente : Component
    {

        public string Name { get; set; }

        public string formName { get; set; }


        public Patente()
        {
        }

        public override int GetChild()
        {
            return 0;
        }

        public override void Add(Component component)
        {
            throw new Exception("No se puede agregar un componente");
        }

        public override void Remove(Component component)
        {
            throw new Exception("No se puede eliminar un componente");
        }
    }
}
