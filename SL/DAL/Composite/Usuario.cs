using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SL.DAL.Composite;

namespace SL.DAL.Composite
{
    class Usuario
    {

        public string Nombre { get; set; }

        public string Password { get; set; }

        public List<Component> permisos { get; set; }

        public Usuario()
        {
            permisos = new List<Component>();
        }

        //public List<Patente> GetPatentes()
        //{

        //}

        public static void RecorrerComposite(List<Patente> patentes, List<Component> components, string tab)
        {
            foreach (var item in components)
            {
                if (item.GetChild() == 0)
                {
                    Patente patente = item as Patente;
                    if (!patentes.Exists(o => o.Name == patente.Name))
                        patentes.Add(patente);
                }
                else
                {
                    Familia familia = item as Familia;  
                    RecorrerComposite(patentes, familia.GetChildrens(), tab);
                }
            }
        }
    }
}
        
    

