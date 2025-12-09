using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Define la estructura para entidades que utilizan
    /// un Dígito Verificador Horizontal (DVH) como mecanismo
    /// de control de integridad en el sistema SIGACF.
    /// </summary>
    public interface IDVH
    {
        /// <summary>
        /// Valor del Dígito Verificador Horizontal asociado a la entidad.
        /// </summary>
        string DVH { get; set; }
    }
}
