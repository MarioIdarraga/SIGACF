using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cancha
    {
        public int idCancha { get; set; }

        public string nombre { get; set; }

        public int capacidad { get; set; }

        public int tipo_Cancha { get; set; }

        public float costo_Hora { get; set; }

        public int estado_Cancha { get; set; }

    }
}
