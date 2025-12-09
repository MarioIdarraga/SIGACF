using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SL.Composite
{
    /// <summary>
    /// Clase base abstracta para la implementación del patrón Composite de permisos.
    /// Representa tanto patentes (hojas) como familias (nodos compuestos),
    /// y proporciona la estructura común para todos los componentes.
    /// </summary>
    public abstract class PermissionComponent : IDVH
    {
        /// <summary>
        /// Identificador único del componente de permiso.
        /// </summary>
        public Guid IdComponent { get; set; }

        /// <summary>
        /// Nombre del componente (patente o familia).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nombre del formulario asociado a la patente.
        /// Para familias puede ser nulo.
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// Tipo del componente (por ejemplo: "Patente" o "Familia").
        /// </summary>
        public string ComponentType { get; set; }

        /// <summary>
        /// Dígito Verificador Horizontal utilizado para control de integridad.
        /// </summary>
        public string DVH { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public PermissionComponent()
        {
        }

        /// <summary>
        /// Obtiene la cantidad de componentes hijos.
        /// Para patentes devuelve siempre 0.
        /// </summary>
        /// <returns>Cantidad de hijos.</returns>
        public abstract int GetChild();

        /// <summary>
        /// Agrega un componente hijo a la estructura Composite.
        /// Las patentes no deben implementar esta operación.
        /// </summary>
        /// <param name="component">Componente a agregar.</param>
        public abstract void Add(PermissionComponent component);

        /// <summary>
        /// Elimina un componente hijo de la estructura Composite.
        /// Las patentes no deben implementar esta operación.
        /// </summary>
        /// <param name="component">Componente a eliminar.</param>
        public abstract void Remove(PermissionComponent component);
    }
}
