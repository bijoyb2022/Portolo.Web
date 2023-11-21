using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portolo.Security.Request;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Portolo.Security.Test
{
    [TestClass]
    public class SecurityServiceTest
    {
        private readonly SecurityService objService = new SecurityService();

        [TestMethod]
        public void ValidateUsersTest()
        {
            var obj = this.objService.ValidateUsers(new UserRequestDTO { UserName = "GGiri", Password = "123456" });
            if (obj != null)
            {
                Assert.Inconclusive("success");
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Decrypt()
        {
            var decryptedText = "yxWWITlpwP0Z49JqPKa12y5Q9rk29dzG5JhcHewKA6gssas96260aQ0kmemoTx07uMdO/m5hFPZnPakVFA3A+Q==";
            var pwd = "Asdf!234";

            var bytesToBeDecrypted = Convert.FromBase64String(decryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(pwd);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var decryptedBytes = AESDecrypt(bytesToBeDecrypted, passwordBytes);
            var originalBytes = new byte[decryptedBytes.Length - Constants.SaltSize];
            for (var i = Constants.SaltSize; i < decryptedBytes.Length; i++)
            {
                originalBytes[i - Constants.SaltSize] = decryptedBytes[i];
            }

            var stringDecrypt = Encoding.UTF8.GetString(originalBytes);
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

        [TestMethod]
        public void Encrypt()
        {
            var text = "182.73.216.90;Portolo_newDev;sa;Bea$Cf345!@#$6"; 
            var pwd = "Asdf!234";

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
            var stringEncrypt = Convert.ToBase64String(encryptedBytes);
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
