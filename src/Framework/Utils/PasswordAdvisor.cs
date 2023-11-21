using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Portolo.Framework.Utils
{
    public enum PasswordScore
    {
        [Description("Blank")]
        Blank,

        [Description("VeryWeak")]
        VeryWeak,

        [Description("Weak")]
        Weak,

        [Description("Moderate")]
        Moderate,

        [Description("Good")]
        Good,

        [Description("Strong")]
        Strong
    }

    public enum PasswordPolicy
    {
        Invalid = 0,
        Number = 1,
        LowerCase = 2,
        UpperCase = 3,
        Special = 4,
        MinimumLength = 5,
    }

    public class PasswordAdvisor
    {
        public static PasswordScore CheckStrength(string password)
        {
            var score = 1;

            if (password.IsNullOrEmpty())
            {
                return PasswordScore.Blank;
            }

            if (password.Length < 6)
            {
                return PasswordScore.VeryWeak;
            }

            if (password.Length >= 6)
            {
                score++;
            }

            if (password.Length >= 10)
            {
                score++;
            }

            if (Regex.IsMatch(password, @"\d+/", RegexOptions.ECMAScript))
            {
                score++;
            }

            if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript))
            {
                score++;
            }

            if (Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
            {
                score++;
            }

            return (PasswordScore)score;
        }

        public static bool IsPasswordPolicyValidate(string password)
        {
            return ValidatePasswordPolicy(password).Count == 5;
        }

        public static List<string> ValidatePasswordPolicy(string password)
        {
            List<string> result = new List<string>();

            if (password.Length >= 12)
            {
                result.Add(PasswordPolicy.MinimumLength.ToString());
            }

            //number
            if (Regex.IsMatch(password, @"[0-9]", RegexOptions.ECMAScript))
            {
                result.Add(PasswordPolicy.Number.ToString());
            }

            //small letter
            if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript))
            {
                result.Add(PasswordPolicy.LowerCase.ToString());
            }

            //capital letter
            if (Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
            {
                result.Add(PasswordPolicy.UpperCase.ToString());
            }

            //special charector
            if (Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]}""';:<>|./?,-]", RegexOptions.ECMAScript))
            {
                result.Add(PasswordPolicy.Special.ToString());
            }

            return result;
        }

        public static string GenerateRandomPassword()
        {
            int r, k;
            int passwordLength = 12;
            string password = string.Empty;
            char[] upperCase = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] lowerCase = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] special = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=', '[', '{', ']', '}', '"', ';', ':', '<', '>', '|', '.', '?', ',', '-' };

            bool lowercaseDone = false;
            bool uppercaseDone = false;
            bool numberDone = false;
            bool specialDone = false;
            for (int i = 0; i < passwordLength; i++)
            {
                r = Portolo.Utility.Cryptography.GenerateRandomNumber.CreateRandomnumber(4);
                if (i > passwordLength - 3)
                {
                    if (!lowercaseDone)
                    {
                        r = 0;
                    }

                    if (!uppercaseDone)
                    {
                        r = 1;
                    }

                    if (!numberDone)
                    {
                        r = 2;
                    }

                    if (!specialDone)
                    {
                        r = 3;
                    }
                }

                if (r == 0)
                {
                    k = Portolo.Utility.Cryptography.GenerateRandomNumber.CreateRandomnumber(0, 25);
                    password += upperCase[k];
                    lowercaseDone = true;
                }
                else if (r == 1)
                {
                    k = Portolo.Utility.Cryptography.GenerateRandomNumber.CreateRandomnumber(0, 25);
                    password += lowerCase[k];
                    uppercaseDone = true;
                }
                else if (r == 2)
                {
                    k = Portolo.Utility.Cryptography.GenerateRandomNumber.CreateRandomnumber(0, 9);
                    password += numbers[k];
                    numberDone = true;
                }
                else if (r == 3)
                {
                    k = Portolo.Utility.Cryptography.GenerateRandomNumber.CreateRandomnumber(0, 27);
                    password += special[k];
                    specialDone = true;
                }
            }

            return password;
        }

        public static string GenerateOTP(int length = 8)
        {
            string otp = string.Empty;
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int k;
            for (int i = 0; i < length; i++)
            {
                k = Portolo.Utility.Cryptography.GenerateRandomNumber.CreateRandomnumber(0, 9);
                otp += numbers[k];
            }

            return otp;
        }
    }
}