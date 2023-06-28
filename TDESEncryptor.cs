using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class TDESEncryptor
{
    public static string Encrypt(string plainText, string key, string iv)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] keyBytes = Convert.FromBase64String(key);
        byte[] ivBytes = Convert.FromBase64String(iv);

        using (var tdes = new TripleDESCryptoServiceProvider())
        {
            tdes.Key = keyBytes;
            tdes.IV = ivBytes;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;

            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, tdes.CreateEncryptor(), CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();
                return Convert.ToBase64String(cipherTextBytes);
            }
        }
    }
    public static string Decrypt(string cipherText, string key, string iv)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        byte[] keyBytes = Convert.FromBase64String(key);
        byte[] ivBytes = Convert.FromBase64String(iv);

        using (var tdes = new TripleDESCryptoServiceProvider())
        {
            tdes.Key = keyBytes;
            tdes.IV = ivBytes;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;

            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, tdes.CreateDecryptor(), CryptoStreamMode.Write);

                cryptoStream.Write(cipherTextBytes, 0, cipherTextBytes.Length);
                cryptoStream.FlushFinalBlock();

                byte[] plainTextBytes = memoryStream.ToArray();
                return Encoding.UTF8.GetString(plainTextBytes);
            }
        }
    }
}