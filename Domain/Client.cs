using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Client
    {
        public int nro_Document { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public int state { get; set; }

        public string comment { get; set; }

        public int telephone { get; set; }

        public int mail { get; set; }

        public int address { get; set; }
    }
}
