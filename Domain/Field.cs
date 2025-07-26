using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Field : IDVH
    {
        public Guid IdField { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int FieldType { get; set; }

        public decimal HourlyCost { get; set; }

        public int IdFieldState { get; set; }
        public string DVH { get; set; }
    }

    public enum FieldType
    {
        Futbol5 = 1,
        Futbol7 = 2,
        Futbol11 = 3
    }
}
