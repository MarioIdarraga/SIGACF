using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Pay
    {
        public int id_Pay { get; set; }

        public DateTime date { get; set; }

        public int method_Pay { get; set; }

        public float amount { get; set; }

        public int state_Pay { get; set; }


    }
}
