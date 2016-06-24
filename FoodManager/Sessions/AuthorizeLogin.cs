using System;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Strings;
using FoodManager.Infrastructure.Utils;
using FoodManager.Infrastructure.Validators.Enums;
using FoodManager.Infrastructure.Validators.Serials;
using FoodManager.IoC.Configs;
using FoodManager.Model.IHmac;
using FoodManager.OrmLite.DataBase;
using FoodManager.Infrastructure.Http;

namespace FoodManager.Sessions
{
    public class AuthorizeLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            SerialValidator();
            if (LoginValidator.ActionValidationHmac(actionContext))
            {
                var headerTimespan = actionContext.GetValue(GlobalConstants.Timespan);
                var headerPublicKey = actionContext.GetValue(GlobalConstants.PublicKey);
                var headerPrivateKey = actionContext.GetValue(GlobalConstants.PrivateKey);
                var headerLoginType = actionContext.GetValue(GlobalConstants.LoginType);

                var loginType = new LoginType().ConvertToCollection().FirstOrDefault(loginTp => loginTp.Value == int.Parse(headerLoginType));
                loginType.ThrowExceptionIfIsNull(HttpStatusCode.ExpectationFailed, "El tipo de login no existe");

                if (loginType.Value == LoginType.User.GetValue())
                    LoginValidator.UserHeaderValidation(headerTimespan, headerPublicKey, headerPrivateKey, actionContext);

                if (loginType.Value == LoginType.Worker.GetValue())
                    LoginValidator.WorkerHeaderValidation(headerTimespan, headerPublicKey, headerPrivateKey, actionContext);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (LoginValidator.ActionValidationHmac(actionExecutedContext.ActionContext) && actionExecutedContext.Exception.IsNull() && actionExecutedContext.Response.IsSuccessStatusCode)
            {
                var headerTimespan = actionExecutedContext.GetValue(GlobalConstants.Timespan);
                var headerPublicKey = actionExecutedContext.GetValue(GlobalConstants.PublicKey);
                var headerLoginType = actionExecutedContext.GetValue(GlobalConstants.LoginType);

                var newPublicKey = RefreshPublicKey(headerTimespan, headerPublicKey, headerLoginType);
                if (newPublicKey.IsNotNullOrEmpty())
                    actionExecutedContext.Response.Headers.Add(GlobalConstants.PublicKey, newPublicKey);
            }

            var dataBaseSqlServerOrmLite = SimpleInjectorModule.GetContainer().GetInstance<IDataBaseSqlServerOrmLite>();
            if (actionExecutedContext.Exception.IsNull() && actionExecutedContext.Response.IsSuccessStatusCode)
            {
                dataBaseSqlServerOrmLite.Commit();
            }
            else
            {
                dataBaseSqlServerOrmLite.Rollback();
            }
        }

        private string RefreshPublicKey(string headerTimespan, string headerPublicKey, string headerLoginType)
        {
            var loginType = new LoginType().ConvertToCollection().FirstOrDefault(loginTp => loginTp.Value == int.Parse(headerLoginType));
            var hmacHelper = SimpleInjectorModule.GetContainer().GetInstance<IHmacHelper>();

            if (loginType.Value == LoginType.User.GetValue())
            {
                var user = hmacHelper.FindUserByPublicKey(headerPublicKey);
                if (user.IsNotNull())
                {
                    user.RefreshAuthenticationHmac(headerTimespan);
                    hmacHelper.UpdateHmacOfUser(user);
                    return user.PublicKey;
                }
            }

            if (loginType.Value == LoginType.Worker.GetValue())
            {
                var worker = hmacHelper.FindWorkerByPublicKey(headerPublicKey);
                if (worker.IsNotNull())
                {
                    worker.RefreshAuthenticationHmac(headerTimespan);
                    hmacHelper.UpdateHmacOfWorker(worker);
                    return worker.PublicKey;
                }
            }

            return string.Empty;
        }

        private void SerialValidator()
        {
            var serialDecrypt = Cryptography.Decrypt(SerialSettings.Serial).Split(' ');
            var date = serialDecrypt[0].DateStringToDateTime();
            var company = serialDecrypt[1];
            if (date < DateTime.Now.Date || company.IsNotEqualTo(GlobalConstants.CompanyKey))
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.NotAcceptable, "El serial no es valido favor de renovarlo");
        }
    }
}