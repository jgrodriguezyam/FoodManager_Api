using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.Helpers
{
    public static class ErrorResponseFactory
    {
        public static HttpResponseMessage New(HttpStatusCode statusCode, string message, JsonMediaTypeFormatter jsonMediaTypeFormatter)
        {
            return new HttpResponseMessage
                       {
                           StatusCode = statusCode,
                           Content = new ObjectContent<ErrorResponse>(new ErrorResponse(message), jsonMediaTypeFormatter)
                       };
        }
    }
}