using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoNotifier.Common
{
    internal static class CryptoUtils
    {
        public static string GetSignature(string secret_key, string message)
        {
            var encoding = new ASCIIEncoding();

            var keyBytes = encoding.GetBytes(secret_key);
            var messageBytes = encoding.GetBytes(message);

            byte[] signature;

            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
 
                signature = hmacsha256.ComputeHash(messageBytes);
            }

            return BitConverter.ToString(signature).Replace("-", string.Empty).ToLower();
        }
    }
}
