using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Representa una cancha dentro del sistema SIGACF.
    /// Contiene información básica como nombre, capacidad, tipo, costo por hora
    /// y el estado actual de la cancha. Incluye además el DVH para control de integridad.
    /// </summary>
    public class Field : IDVH
    {
        /// <summary>
        /// Identificador único de la cancha.
        /// </summary>
        public Guid IdField { get; set; }

        /// <summary>
        /// Nombre asignado a la cancha.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Capacidad máxima de jugadores para esta cancha.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Tipo de cancha (Fútbol 5, 7 o 11).
        /// </summary>
        public int FieldType { get; set; }

        /// <summary>
        /// Costo por hora de alquiler de la cancha.
        /// </summary>
        public decimal HourlyCost { get; set; }

        /// <summary>
        /// Identificador del estado actual de la cancha.
        /// </summary>
        public int IdFieldState { get; set; }

        /// <summary>
        /// Dígito Verificador Horizontal utilizado para control de integridad.
        /// </summary>
        public string DVH { get; set; }
    }

    /// <summary>
    /// Enum que representa los tipos posibles de cancha en el sistema SIGACF.
    /// </summary>
    public enum FieldType
    {
        /// <summary>
        /// Cancha de Fútbol 5.
        /// </summary>
        Futbol5 = 1,

        /// <summary>
        /// Cancha de Fútbol 7.
        /// </summary>
        Futbol7 = 2,

        /// <summary>
        /// Cancha de Fútbol 11.
        /// </summary>
        Futbol11 = 3
    }
}
