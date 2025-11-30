using System;
using System.Collections.Generic;
using Domain;

namespace SL.Helpers
{
    /// <summary>
    /// Maneja información de sesión del usuario autenticado.
    /// Capa transversal (SL).
    /// </summary>
    public static class Session
    {
        public static User CurrentUser { get; set; }

        /// <summary>
        /// Formularios permitidos según las patentes del usuario.
        /// </summary>
        public static HashSet<string> AllowedForms { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }
}

