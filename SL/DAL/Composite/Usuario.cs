using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Patente> GetPatentes()
        {

        }

        public static void RecorrerComposite(List<Patente> patentes, List<Component> components)
        {
            foreach (var item in components)
            {
                if (item is Patente)
                {
                    patentes.Add((Patente) item);
                }
                else if (item is Familia)
                {
                    RecorrerComposite(patentes, ((Familia) item).childrens);
                }
     }  
}
            
        
    

