using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FoodManager.Infrastructure.Files
{
    public static class StreamReply
    {
        public static HttpResponseMessage ConvertToHttpResponse(Stream stream)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}