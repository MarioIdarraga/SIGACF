using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SL.Helpers
{
    /// <summary>
    /// Proporciona métodos para cifrar y descifrar texto utilizando el algoritmo AES.
    /// Las claves se obtienen desde AppSettings.
    /// </summary>
    public static class AesEncryptionHelper
    {
        /// <summary>
        /// Clave AES obtenida desde AppSettings (AESKey).
        /// </summary>
        private static readonly string Key = ConfigurationManager.AppSettings["AESKey"];

        /// <summary>
        /// Vector de inicialización AES obtenido desde AppSettings (AESIV).
        /// </summary>
        private static readonly string IV = ConfigurationManager.AppSettings["AESIV"];

        /// <summary>
        /// Cifra un texto plano utilizando el algoritmo AES en modo CBC.
        /// </summary>
        /// <param name="plainText">Texto sin cifrar.</param>
        /// <returns>Texto cifrado en formato Base64.</returns>
        public static string Encrypt(string plainText)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor();

                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                    swEncrypt.Close();
                    encrypted = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Descifra un texto cifrado con AES en formato Base64.
        /// </summary>
        /// <param name="encryptedText">Texto cifrado.</param>
        /// <returns>Texto plano descifrado.</returns>
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedText);
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor();

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
            }

            return plaintext;
        }

        /// <summary>
        /// Indica si un texto parece estar cifrado con AES.
        /// Verifica si es Base64 válido y si puede descifrarse sin error.
        /// </summary>
        /// <param name="input">Texto a verificar.</param>
        /// <returns>
        /// true si el texto parece estar cifrado con AES; de lo contrario, false.
        /// </returns>
        public static bool IsEncryptedAES(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            try
            {
                // Verifica si es Base64 válido
                var base64Bytes = Convert.FromBase64String(input);

                // Intenta descifrar
                var decrypted = Decrypt(input);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
