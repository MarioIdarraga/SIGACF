using System.Collections.Generic;
using System.Linq;
using SL.Composite;

namespace SL.Helpers
{
    /// <summary>
    /// Proporciona métodos utilitarios para procesar estructuras de permisos,
    /// permitiendo obtener una lista plana de patentes a partir de componentes
    /// compuestos por familias y patentes.
    /// </summary>
    public static class PermissionHelper
    {
        /// <summary>
        /// Devuelve una lista plana de patentes a partir de una lista de componentes
        /// de permisos (patentes y familias), recorriendo recursivamente la jerarquía.
        /// </summary>
        /// <param name="components">Lista de componentes de permisos.</param>
        /// <returns>Lista de patentes sin elementos duplicados.</returns>
        public static List<Patente> FlattenPermissions(List<PermissionComponent> components)
        {
            var patentes = new List<Patente>();
            Recorrer(patentes, components);
            return patentes;
        }

        /// <summary>
        /// Recorre recursivamente la estructura jerárquica de permisos,
        /// agregando a la lista resultante únicamente las patentes finales (hojas).
        /// </summary>
        /// <param name="result">Lista donde se almacenarán las patentes finales.</param>
        /// <param name="components">Componentes de permisos a evaluar.</param>
        private static void Recorrer(List<Patente> result, List<PermissionComponent> components)
        {
            foreach (var item in components)
            {
                if (item is Patente p && p.GetChild() == 0)
                {
                    if (!result.Any(x => x.Name == p.Name))
                        result.Add(p);
                }
                else if (item is Familia f)
                {
                    Recorrer(result, f.GetChildren());
                }
            }
        }
    }
}


