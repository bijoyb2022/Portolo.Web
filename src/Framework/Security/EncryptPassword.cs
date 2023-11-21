using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Portolo.Framework.Security
{
    public static class EncryptPassword
    {
        public static string PasswordEncryption(string text)
        {
            var inputBytes = Encoding.UTF8.GetBytes(text);
            SHA512 shaManager = new SHA512Managed();
            var hashedBytes = shaManager.ComputeHash(inputBytes);

            var encryptedString = Convert.ToBase64String(hashedBytes);
            return encryptedString;
        }
    }
}