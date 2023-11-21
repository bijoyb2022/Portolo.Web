using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Portolo.Utility.Cryptography
{
    public static class Encryption
    {
        public static string Encrypt(string text, string pwd)
        {
            var originalBytes = Encoding.UTF8.GetBytes(text);
            var passwordBytes = Encoding.UTF8.GetBytes(pwd);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var saltBytes = GetRandomBytes();
            var bytesToBeEncrypted = new byte[saltBytes.Length + originalBytes.Length];
            for (var i = 0; i < saltBytes.Length; i++)
            {
                bytesToBeEncrypted[i] = saltBytes[i];
            }

            for (var i = 0; i < originalBytes.Length; i++)
            {
                bytesToBeEncrypted[i + saltBytes.Length] = originalBytes[i];
            }

            var encryptedBytes = AESEncrypt(bytesToBeEncrypted, passwordBytes);
            return Convert.ToBase64String(encryptedBytes);
        }

        private static byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (var ms = new MemoryStream())
            using (var aes = new RijndaelManaged())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);
                aes.Mode = CipherMode.CBC;
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                }

                encryptedBytes = ms.ToArray();
            }

            return encryptedBytes;
        }

        private static byte[] GetRandomBytes()
        {
            var ba = new byte[Constants.SaltSize];
            RandomNumberGenerator.Create().GetBytes(ba);
            return ba;
        }
    }
}