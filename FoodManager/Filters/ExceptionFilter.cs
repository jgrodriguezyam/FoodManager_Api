using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Filters;
using FoodManager.Helpers;
using FoodManager.Infrastructure.Exceptions;

namespace FoodManager.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, HttpStatusCode> _exceptions = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(EntityNotFoundException), HttpStatusCode.NotFound },
            { typeof(InvalidRequestException), HttpStatusCode.BadRequest }
        };

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var jsonMediaTypeFormatter = actionExecutedContext.ActionContext.ActionDescriptor.Configuration.Formatters.JsonFormatter;
            var exception = actionExecutedContext.Exception;
            var exceptionType = exception.GetType();
            actionExecutedContext.Response = _exceptions.ContainsKey(exceptionType)
                ? ErrorResponseFactory.New(_exceptions[exceptionType], exception.Message, jsonMediaTypeFormatter)
                : ErrorResponseFactory.New(HttpStatusCode.InternalServerError, "Ha ocurrido un error en la aplicación.", jsonMediaTypeFormatter);
        }
    }
}