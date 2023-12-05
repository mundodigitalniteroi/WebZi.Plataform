using System.Security.Cryptography;

namespace WebZi.Plataform.CrossCutting.Secutity
{
    public static class CryptographyHelper
    {
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = Convert.FromBase64String(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        //public static string EncryptString(byte[] key, string plainText)
        //{
        //    byte[] iv = new byte[16];
        //    byte[] array;

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.KeySize = 256;
        //        aes.Key = Convert.FromBase64String(key);
        //        aes.IV = iv;

        //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        //        using MemoryStream memoryStream = new();
        //        using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
        //        using (StreamWriter streamWriter = new(cryptoStream))
        //        {
        //            streamWriter.Write(plainText);
        //        }

        //        array = memoryStream.ToArray();
        //    }

        //    return Convert.ToBase64String(array);
        //}

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Convert.FromBase64String(key);
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            
            return streamReader.ReadToEnd();
        }
    }
}