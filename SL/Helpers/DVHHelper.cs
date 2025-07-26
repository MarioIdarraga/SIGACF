using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SL.Helpers
{
    public static class DVHHelper
    {
        public static string CalcularDVH(Field field)
        {
            string data = $"{field.IdField}{field.Name}{field.Capacity}{field.FieldType}{field.HourlyCost}{field.IdFieldState}";
            return HashHelper.CalculateSHA256(data);
        }
    }
}
