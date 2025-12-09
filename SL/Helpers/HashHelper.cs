using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SL.Helpers
{
    /// <summary>
    /// Proporciona métodos utilitarios para calcular valores hash
    /// utilizando el algoritmo criptográfico SHA-256.
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// Calcula el hash SHA-256 de una cadena de texto y lo devuelve en formato Base64.
        /// </summary>
        /// <param name="input">Texto a partir del cual se generará el hash.</param>
        /// <returns>Cadena en Base64 correspondiente al hash SHA-256.</returns>
        public static string CalculateSHA256(string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
