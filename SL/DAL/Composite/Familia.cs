using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Composite
{
    /// <summary>
    /// Representa una familia de permisos dentro del patrón Composite.
    /// Puede contener uno o varios componentes de permisos,
    /// permitiendo estructurar jerarquías entre familias y patentes.
    /// </summary>
    public class Familia : PermissionComponent
    {
        /// <summary>
        /// Lista interna de componentes que pertenecen a la familia.
        /// </summary>
        private List<PermissionComponent> childrens = new List<PermissionComponent>();

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Familia() { }

        /// <summary>
        /// Crea una nueva familia con un nombre y un componente inicial.
        /// </summary>
        /// <param name="nombre">Nombre de la familia.</param>
        /// <param name="component">Componente inicial que será agregado a la familia (opcional).</param>
        public Familia(string nombre, PermissionComponent component)
        {
            Name = nombre;

            if (component != null)
                childrens.Add(component);
        }

        /// <summary>
        /// Devuelve la lista de componentes hijos de la familia.
        /// </summary>
        /// <returns>Lista de objetos <see cref="PermissionComponent"/>.</returns>
        public List<PermissionComponent> GetChildren()
        {
            return childrens;
        }

        /// <summary>
        /// Devuelve la cantidad de componentes hijos que posee la familia.
        /// </summary>
        /// <returns>Número de elementos hijos.</returns>
        public override int GetChild()
        {
            return childrens.Count;
        }

        /// <summary>
        /// Agrega un componente a la familia.
        /// </summary>
        /// <param name="component">Componente a agregar.</param>
        public override void Add(PermissionComponent component)
        {
            childrens.Add(component);
        }

        /// <summary>
        /// Elimina un componente de la familia según el Id del componente.
        /// </summary>
        /// <param name="component">Componente a eliminar.</param>
        public override void Remove(PermissionComponent component)
        {
            childrens.RemoveAll(o => o.IdComponent == component.IdComponent);
        }
    }
}
