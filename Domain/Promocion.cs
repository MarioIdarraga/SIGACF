using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Promocion
    {

        public int id_Promocion { get; set; }

        public string nombre { get; set; }

        public DateTime vigencia_Desde { get; set; }

        public DateTime vigencia_Hasta { get; set; }

        public string descripcion { get; set; }


    }
}
