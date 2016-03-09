using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Objects;
using FoodManager.IoC.Configs;
using FoodManager.Model.IHmac;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.Sessions
{
    public class AuthorizeLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //if (LoginValidator.ActionValidationHmac(actionContext))
            //{
            //    var hmacHelper = SimpleInjectorModule.GetContainer().GetInstance<IHmacHelper>();
            //    var headerTimespan = actionContext.Request.Headers.GetValues(GlobalConstants.Timespan).First();
            //    var headerPublicKey = actionContext.Request.Headers.GetValues(GlobalConstants.PublicKey).First();
            //    var headerPrivateKey = actionContext.Request.Headers.GetValues(GlobalConstants.PrivateKey).First();

            //    var user = hmacHelper.FindUserByPublicKey(headerPublicKey);
            //    user.ThrowExceptionIfIsNull(HttpStatusCode.ExpectationFailed, "Llave publica invalida");
            //    LoginValidator.TimeSpanValidation(headerTimespan, user.Time);
            //    LoginValidator.PrivateKeyValidation(headerPrivateKey, headerTimespan, user.UserName, user.Password);
            //}
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //if (LoginValidator.ActionValidationHmac(actionExecutedContext.ActionContext) && actionExecutedContext.Exception.IsNull() && actionExecutedContext.Response.IsSuccessStatusCode)
            //{
            //    var hmacHelper = SimpleInjectorModule.GetContainer().GetInstance<IHmacHelper>();
            //    var headerTimespan = actionExecutedContext.Request.Headers.GetValues(GlobalConstants.Timespan).First();
            //    var headerPublicKey = actionExecutedContext.Request.Headers.GetValues(GlobalConstants.PublicKey).First();
            //    var user = hmacHelper.FindUserByPublicKey(headerPublicKey);
            //    if (user.IsNotNull())
            //    {
            //        user.RefreshAuthenticationHmac(headerTimespan);
            //        hmacHelper.RefreshHmacOfUser(user);
            //        actionExecutedContext.Response.Headers.Add(GlobalConstants.PublicKey, user.PublicKey);
            //    }
            //}

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
    }
}