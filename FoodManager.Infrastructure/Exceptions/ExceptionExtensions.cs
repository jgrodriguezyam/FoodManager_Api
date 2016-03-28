using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Strings;

namespace FoodManager.Infrastructure.Exceptions
{
    public static class ExceptionExtensions
    {
        public static void ThrowExceptionIfIsNull<TException>(this object objectValue, string message) where TException : Exception
        {
            if (objectValue.IsNull())
                ThrowException<TException>(message);
        }

        public static void ThrowExceptionIfIsNullOrEmpty<TException>(this string stringValue, string message) where TException : Exception
        {
            if (stringValue.IsNullOrEmpty())
                ThrowException<TException>(message);
        }

        public static void ThrowExceptionIfIsNotNull<TException>(this object objectValue, string message) where TException : Exception
        {
            if (objectValue.IsNotNull())
                ThrowException<TException>(message);
        }

        public static void ThrowExceptionIfIsNotNullOrEmpty<TException>(this string stringValue, string message) where TException : Exception
        {
            if (stringValue.IsNotNullOrEmpty())
                ThrowException<TException>(message);
        }

        private static void ThrowException<TException>(string message) where TException : Exception
        {
            var exceptionType = typeof(TException);
            var exceptionConstructor = exceptionType.GetConstructor(new[] { typeof(string) });
            var exception = (TException)exceptionConstructor.Invoke(new object[] { message });
            throw exception;
        }

        public static void ThrowExceptionIfIsSameValue<TException>(this object objectValue, object valueToCompare) where TException : Exception
        {
            if (objectValue.Equals(valueToCompare))
                ThrowException<TException>("SameValue");
        }

        public static void ThrowExceptionIfIsNull(this object objectValue, HttpStatusCode httpStatusCode, string message)
        {
            if (objectValue.IsNull())
                ThrowCustomException(httpStatusCode, message);
        }

        public static void ThrowCustomException(HttpStatusCode httpStatusCode, string message)
        {
            throw new HttpResponseException(new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                Content = new StringContent("{\"Message\": \"" + message + "\"}", Encoding.Default, "application/json")
            });
        }

        public static void ThrowStatusException(HttpStatusCode httpStatusCode, bool entityStatus)
        {
            throw new HttpResponseException(new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                Content = new StringContent("{\"Message\": \"El estado ya se encontraba " + (entityStatus ? "activado" : "desactivado") + "\"}", Encoding.Default, "application/json")
            });
        }

        public static void ThrowIsReferenceException(HttpStatusCode httpStatusCode)
        {
            throw new HttpResponseException(new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                Content = new StringContent("{\"Message\": \"Remover previamente las referencias\"}", Encoding.Default, "application/json")
            });
        }

        public static void ThrowExceptionIfIsNull(this object objectValue, string message)
        {
            if (objectValue.IsNull())
                ThrowCustomException(HttpStatusCode.NotFound, message);
        }
    }
}