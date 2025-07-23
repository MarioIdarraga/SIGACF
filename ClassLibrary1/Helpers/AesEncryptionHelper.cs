using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


public static class AesEncryptionHelper
{
    private static readonly string Key = ConfigurationManager.AppSettings["AESKey"];
    private static readonly string IV = ConfigurationManager.AppSettings["AESIV"];

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
}
