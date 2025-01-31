using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Reserva
    {

        public int id_Reserva { get; set; }

        public DateTime fecha_Registro { get; set; }

        public DateTime fecha_Reserva { get; set; }

        public  DateTime hora_Inicio{ get; set; }

        public int promocion { get; set; }

        public int estado_Reserva { get; set; }

    }
}
