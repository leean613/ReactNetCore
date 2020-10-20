using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services.Implementations.Helpers
{
    public static class LoginHelper
    {
        public static string EncryptPassword(string passsword)
        {
            return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(passsword)));
        }

        public static bool CheckPassword(string passwordToVerify, string encryptedPassword)
        {
            return EncryptPassword(passwordToVerify).Equals(encryptedPassword, StringComparison.InvariantCulture);
        }
    }
}
