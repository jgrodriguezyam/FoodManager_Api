using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Strings;
using FoodManager.Infrastructure.Utils;
using FoodManager.IoC.Configs;
using FoodManager.Model.IHmac;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.Sessions
{
    public class AuthorizeLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //var hmacHelper = SimpleInjectorModule.GetContainer().GetInstance<IHmacHelper>();
            //var headerTimespan = actionContext.Request.Headers.GetValues(GlobalConstants.Timespan).First();
            //var headerPublicKey = actionContext.Request.Headers.GetValues(GlobalConstants.PublicKey).First();
            //var headerPrivateKey = actionContext.Request.Headers.GetValues(GlobalConstants.PrivateKey).First();

            //var user = hmacHelper.FindUserByPublicKey(headerPublicKey);
            //user.ThrowExceptionIfIsNull(HttpStatusCode.ExpectationFailed, "Llave publica invalida");
            //TimeSpanValidation(headerTimespan, user.Time);
            //PrivateKeyValidation(headerPrivateKey, headerTimespan, user.UserName, user.Password);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var dataBaseSqlServerOrmLite = SimpleInjectorModule.GetContainer().GetInstance<IDataBaseSqlServerOrmLite>();
            if (actionExecutedContext.Exception.IsNull() && actionExecutedContext.Response.IsSuccessStatusCode)
            {
                //var hmacHelper = SimpleInjectorModule.GetContainer().GetInstance<IHmacHelper>();
                //var headerTimespan = actionExecutedContext.Request.Headers.GetValues(GlobalConstants.Timespan).First();
                //var headerPublicKey = actionExecutedContext.Request.Headers.GetValues(GlobalConstants.PublicKey).First();
                //var user = hmacHelper.FindUserByPublicKey(headerPublicKey);
                //if (user.IsNotNull())
                //{
                //    user.RefreshAuthenticationHmac(headerTimespan);
                //    hmacHelper.RefreshHmacOfUser(user);
                //    actionExecutedContext.Response.Headers.Add(GlobalConstants.PublicKey, user.PublicKey);
                //}

                dataBaseSqlServerOrmLite.Commit();
            }
            else
            {
                dataBaseSqlServerOrmLite.Rollback();
            }
        }

        #region Custom Methods

        private static void TimeSpanValidation(string headerTimespan, string time)
        {
            if (time == headerTimespan || time.IsNullOrEmpty() || headerTimespan.IsNullOrEmpty())
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.ExpectationFailed, "Espacio de tiempo invalido");
        }

        private void PrivateKeyValidation(string headerPrivateKey, string headerTimespan, string userName, string password)
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

        public string ByteToString(byte[] buff)
        {
            var sbinary = "";
            for (var i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2");
            return (sbinary);
        }

        #endregion
    }
}