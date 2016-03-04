using System.Net;
using System.Security.Cryptography;
using System.Text;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Strings;
using FoodManager.Infrastructure.Utils;

namespace FoodManager.Sessions
{
    public static class LoginValidator
    {
        public static void TimeSpanValidation(string headerTimespan, string time)
        {
            if (time == headerTimespan || time.IsNullOrEmpty() || headerTimespan.IsNullOrEmpty())
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.ExpectationFailed, "Espacio de tiempo invalido");
        }

        public static void PrivateKeyValidation(string headerPrivateKey, string headerTimespan, string userName, string password)
        {
            var key = GlobalConstants.CryptographyKey;
            var message = userName + Cryptography.Decrypt(password) + headerTimespan;
            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(key);
            var messageBytes = encoding.GetBytes(message);
            var hmacsha256 = new HMACSHA256(keyByte);
            var hash256Message = hmacsha256.ComputeHash(messageBytes);
            var privateKey = ByteToString(hash256Message);
            if (headerPrivateKey != privateKey)
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.ExpectationFailed, "Llave privada invalida");
        }

        private static string ByteToString(byte[] buff)
        {
            var sbinary = "";
            for (var i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2");
            return (sbinary);
        }
    }
}