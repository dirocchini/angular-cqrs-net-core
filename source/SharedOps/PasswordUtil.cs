using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedOps
{
    public static class PasswordUtil
    {
        public static (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string requestPassword)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestPassword));

            return (passwordSalt, passwordHash);
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != passwordHash[i]) 
                    return false;

            return true;
        }
    }
}
