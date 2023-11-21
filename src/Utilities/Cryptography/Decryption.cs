using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Portolo.Utility.Cryptography
{
    public static class Decryption
    {
        public static string Decrypt(string decryptedText, string pwd)
        {
            var bytesToBeDecrypted = Convert.FromBase64String(decryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(pwd);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var decryptedBytes = AESDecrypt(bytesToBeDecrypted, passwordBytes);
            var originalBytes = new byte[decryptedBytes.Length - Constants.SaltSize];
            for (var i = Constants.SaltSize; i < decryptedBytes.Length; i++)
            {
                originalBytes[i - Constants.SaltSize] = decryptedBytes[i];
            }

            return Encoding.UTF8.GetString(originalBytes);
        }

        private static byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
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
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                }

                decryptedBytes = ms.ToArray();
            }

            return decryptedBytes;
        }
    }
}