using System;
using System.Security.Cryptography;
using System.Text;

namespace SharedOps
{
    /// <summary>
    /// THIS APPROACH ISN`T SO SAFE!!! DO NOT USE IT IN PRODUCTION
    /// </summary>
    public static class PasswordUtil
    {
        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Crypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputBuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputBuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }
}
