using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.DAL.Composite
{
    public class Familia : Component
    {
        private List<Patrones.Component> childrens = new List<Patrones.Component>();

        public string Name { get; set; }

        public Familia(string Nombre, Component component)
        {
            if (Component != null)
            {
                childrens.Add(component);
                Name = Nombre;
            }
            else
            {
                throw new Exception("No se puede crear una familia sin un componente");
            }
        }

        public override int GetChild()
        {
            return childrens.Count;
        }

        public override void Add(Component component)
        {
            throw new Exception("No se puede agregar un componente");
        }


        public override void Remove(Component component)
        {

            childrens.RemoveAll(o => o.IdComponent == component.IdComponent);

        }
    }
}
