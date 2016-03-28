using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Strings;
using FoodManager.Infrastructure.Utils;
using System.Web.Http.Controllers;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators.Enums;
using FoodManager.IoC.Configs;
using FoodManager.Model.IHmac;

namespace FoodManager.Sessions
{
    public static class LoginValidator
    {
        public static void UserHeaderValidation(string headerTimespan, string headerPublicKey, string headerPrivateKey)
        {
            var hmacHelper = SimpleInjectorModule.GetContainer().GetInstance<IHmacHelper>();
            var user = hmacHelper.FindUserByPublicKey(headerPublicKey);
            user.ThrowExceptionIfIsNull(HttpStatusCode.ExpectationFailed, "Llave publica invalida");
            TimeSpanValidation(headerTimespan, user.Time);
            var messageToValidate = user.UserName + Cryptography.Decrypt(user.Password) + headerTimespan;
            PrivateKeyValidation(headerPrivateKey, messageToValidate);
        }

        public static void WorkerHeaderValidation(string headerTimespan, string headerPublicKey, string headerPrivateKey)
        {
            var hmacHelper = SimpleInjectorModule.GetContainer().GetInstance<IHmacHelper>();
            var worker = hmacHelper.FindWorkerByPublicKey(headerPublicKey);
            worker.ThrowExceptionIfIsNull(HttpStatusCode.ExpectationFailed, "Llave publica invalida");
            TimeSpanValidation(headerTimespan, worker.Time);
            var messageToValidate = worker.Badge + headerTimespan;
            PrivateKeyValidation(headerPrivateKey, messageToValidate);
        }

        public static void TimeSpanValidation(string headerTimespan, string time)
        {
            if (time == headerTimespan || time.IsNullOrEmpty() || headerTimespan.IsNullOrEmpty())
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.ExpectationFailed, "Espacio de tiempo invalido");
        }

        private static void PrivateKeyValidation(string headerPrivateKey, string messageToValidate)
        {
            var key = GlobalConstants.CryptographyKey;
            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(key);
            var messageBytes = encoding.GetBytes(messageToValidate);
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

        public static bool ActionValidationHmac(HttpActionContext actionContext)
        {
            var disableAction = new DisableValidatorHmac().ConvertToCollection().FirstOrDefault(dvm => dvm.Name == actionContext.ActionDescriptor.ActionName);
            return disableAction.IsNull();
        }
    }
}