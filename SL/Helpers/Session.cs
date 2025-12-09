using System;
using System.Collections.Generic;
using Domain;

namespace SL.Helpers
{
    /// <summary>
    /// Maneja la información de sesión del usuario autenticado dentro del sistema.
    /// Pertenece a la capa transversal (SL), accesible por las demás capas.
    /// </summary>
    public static class Session
    {
        /// <summary>
        /// Usuario actualmente autenticado en el sistema.
        /// </summary>
        public static User CurrentUser { get; set; }

        /// <summary>
        /// Conjunto de formularios permitidos para el usuario autenticado,
        /// determinado según sus patentes y permisos asignados.
        /// </summary>
        public static HashSet<string> AllowedForms { get; set; } =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }
}

