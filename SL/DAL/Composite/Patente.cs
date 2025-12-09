using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Composite
{
    /// <summary>
    /// Representa una patente dentro del patrón Composite de permisos.
    /// Es un componente hoja, por lo que no contiene elementos hijos.
    /// </summary>
    public class Patente : PermissionComponent
    {
        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Patente() { }

        /// <summary>
        /// Devuelve el nombre de la patente.
        /// </summary>
        /// <returns>Nombre de la patente.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Como es un componente hoja, no posee hijos.
        /// </summary>
        /// <returns>Siempre 0.</returns>
        public override int GetChild()
        {
            return 0;
        }

        /// <summary>
        /// Una patente no puede agregar componentes, al ser un nodo hoja.
        /// </summary>
        /// <param name="component">Componente que se intentó agregar.</param>
        /// <exception cref="Exception">Siempre se lanza ya que la operación no está permitida.</exception>
        public override void Add(PermissionComponent component)
        {
            throw new Exception("No se puede agregar un componente");
        }

        /// <summary>
        /// Una patente no puede eliminar componentes, al ser un nodo hoja.
        /// </summary>
        /// <param name="component">Componente que se intentó eliminar.</param>
        /// <exception cref="Exception">Siempre se lanza ya que la operación no está permitida.</exception>
        public override void Remove(PermissionComponent component)
        {
            throw new Exception("No se puede eliminar un componente");
        }
    }
}
