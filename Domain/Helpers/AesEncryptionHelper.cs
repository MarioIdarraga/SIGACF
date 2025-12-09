using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Proporciona métodos de cifrado y descifrado AES para proteger información sensible.
/// Utiliza claves y vectores de inicialización definidos en el archivo de configuración.
/// </summary>
public static class AesEncryptionHelper
{
    /// <summary>
    /// Clave AES utilizada para el proceso de cifrado y descifrado.
    /// Se obtiene desde el archivo de configuración (app.config).
    /// </summary>
    private static readonly string Key = ConfigurationManager.AppSettings["AESKey"];

    /// <summary>
    /// Vector de inicialización (IV) utilizado para el cifrado AES.
    /// También se obtiene desde el archivo de configuración.
    /// </summary>
    private static readonly string IV = ConfigurationManager.AppSettings["AESIV"];

    /// <summary>
    /// Cifra un texto plano utilizando el algoritmo AES en modo CBC.
    /// </summary>
    /// <param name="plainText">Texto sin cifrar que se desea proteger.</param>
    /// <returns>Cadena en Base64 que representa el texto cifrado.</returns>
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
    /// Descifra un texto previamente cifrado con AES.
    /// </summary>
    /// <param name="encryptedText">Texto cifrado en formato Base64.</param>
    /// <returns>Texto plano resultante del proceso de descifrado.</returns>
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
    /// Verifica si un texto dado parece estar cifrado con AES.
    /// Intenta validar si es Base64 y si puede ser descifrado correctamente.
    /// </summary>
    /// <param name="input">Cadena a evaluar.</param>
    /// <returns>
    /// true si el formato es compatible con un cifrado AES válido; de lo contrario, false.
    /// </returns>
    public static bool IsEncryptedAES(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        try
        {
            // Verifica si es Base64 válido
            var base64Bytes = Convert.FromBase64String(input);

            // Intenta descifrar el contenido
            var decrypted = Decrypt(input);

            // Si no falla, se considera un texto AES válido
            return true;
        }
        catch
        {
            return false;
        }
    }
}

