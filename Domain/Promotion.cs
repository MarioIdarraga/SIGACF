using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Promotion
    {

        public Guid IdPromotion { get; set; }

        public string Name { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public string PromotionDescripcion { get; set; }
        public int DiscountPercentage { get; set; }
    }
}
