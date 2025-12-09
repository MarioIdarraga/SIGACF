using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa una promoción disponible dentro del sistema SIGACF.
    /// Incluye su período de validez, nombre, descripción y porcentaje de descuento.
    /// </summary>
    public class Promotion
    {
        /// <summary>
        /// Identificador único de la promoción.
        /// </summary>
        public Guid IdPromotion { get; set; }

        /// <summary>
        /// Nombre de la promoción.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Fecha desde la cual la promoción es válida.
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Fecha hasta la cual la promoción es válida.
        /// </summary>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Descripción detallada de la promoción.
        /// </summary>
        public string PromotionDescripcion { get; set; }

        /// <summary>
        /// Porcentaje de descuento aplicado por la promoción.
        /// Puede ser nulo si no se define un valor específico.
        /// </summary>
        public int? DiscountPercentage { get; set; }
    }
}
