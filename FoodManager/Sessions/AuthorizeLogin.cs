using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Strings;
using FoodManager.Infrastructure.Validators.Enums;
using FoodManager.IoC.Configs;
using FoodManager.Model.IHmac;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.Sessions
{
    public class AuthorizeLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (LoginValidator.ActionValidationHmac(actionContext))
            {
                var headerTimespan = actionContext.Request.Headers.GetValues(GlobalConstants.Timespan).First();
                var headerPublicKey = actionContext.Request.Headers.GetValues(GlobalConstants.PublicKey).First();
                var headerPrivateKey = actionContext.Request.Headers.GetValues(GlobalConstants.PrivateKey).First();
                var headerLoginType = actionContext.Request.Headers.GetValues(GlobalConstants.LoginType).First();

                var loginType = new LoginType().ConvertToCollection().FirstOrDefault(loginTp => loginTp.Value == int.Parse(headerLoginType));
                if (loginType.IsNull())
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.ExpectationFailed, "El tipo de login no existe");

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
                var headerTimespan = actionExecutedContext.Request.Headers.GetValues(GlobalConstants.Timespan).First();
                var headerPublicKey = actionExecutedContext.Request.Headers.GetValues(GlobalConstants.PublicKey).First();
                var headerLoginType = actionExecutedContext.Request.Headers.GetValues(GlobalConstants.LoginType).First();

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
    }
}