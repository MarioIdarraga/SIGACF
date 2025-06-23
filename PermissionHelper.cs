using SL.DAL.Composite;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Helpers
{
    public static class PermissionHelper
    {
        public static List<Patente> FlattenPermissions(List<Component> components)
        {
            var patentes = new List<Patente>();
            Recorrer(patentes, components);
            return patentes;
        }

        private static void Recorrer(List<Patente> result, List<Component> components)
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
                    Recorrer(result, f.GetChildrens());
                }
            }
        }
    }
}
