using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Portolo.Utility.Cryptography
{
    public class GenerateRandomNumber
    {
        public static string CreateRandomnumber(int length, string gen)
        {
            char[] chars = gen.ToCharArray();
            byte[] data = new byte[length];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString().ToUpper();
        }

        public static int CreateRandomnumber(int min, int max)
        {
            var random = System.Security.Cryptography.RandomNumberGenerator.Create();
            var bytes = new byte[sizeof(int)];
            random.GetNonZeroBytes(bytes);
            var val = BitConverter.ToInt32(bytes, 0);
            var result = ((((val - min) % (max - min + 1)) + (max - min + 1)) % (max - min + 1)) + min;
            return result;
        }

        public static double CreateRandomnumberDouble()
        {
            var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[8];
            rng.GetBytes(bytes);
            // Step 2: bit-shift 11 and 53 based on double's mantissa bits
            var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            return ul / (double)(1UL << 53);
        }

        public static int CreateRandomnumber()
        {
            var crypto = new RNGCryptoServiceProvider();
            byte[] val = new byte[6];
            crypto.GetBytes(val);
            return BitConverter.ToInt32(val, 1);
        }

        public static int CreateRandomnumber(int max)
        {
            double mAX_RANGE = (double)ulong.MaxValue + 1;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[8];

                rng.GetBytes(randomNumber);
                double baseNum = BitConverter.ToUInt64(randomNumber, 0) / mAX_RANGE;

                var range = (ulong)max - 0;

                return (int)((baseNum * range) + 0);
            }
        }
    }
}
