using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Booking
    {

        public Guid Id { get; set; }

        public Guid IdCustomer { get; set; }

        public string NroDocument { get; set; }

        public DateTime RegistrationDate { get; set; }  ///No quiero que se vea en la carga de ka reserva

        public DateTime RegistrationBooking { get; set; }

        public  DateTime StartTime{ get; set; }

        public DateTime EndTime { get; set; }

        public int Field { get; set; }

        public int Promotion { get; set; }

        public int State { get; set; } ///No se ve en la carga de la reserva

    }
}
