using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FoodManager.Infrastructure.Objects;
using File = FoodManager.Infrastructure.Files.File;

namespace FoodManager.Helpers
{
    public static class WebApiExtensions
    {

        //TODO: Add File Validations here. ReadAsMultipartAsync method CANNOT read files that exceeds  the request max length configured in web.config.
        public static File GetFile(this HttpRequestMessage httpRequestMessage)
        {
            return InnerGetFile(httpRequestMessage).Result;
        }
        private static async Task<File> InnerGetFile(HttpRequestMessage httpRequestMessage)
        {
            if (httpRequestMessage.Content.IsMimeMultipartContent())
            {
                var httpContent = httpRequestMessage.Content;
                var multipartMemoryStreamProvider = new MultipartMemoryStreamProvider();
                await httpContent.ReadAsMultipartAsync(multipartMemoryStreamProvider);
                var part = multipartMemoryStreamProvider.Contents.FirstOrDefault(content => content.Headers.ContentType.IsNull() || !Regex.IsMatch(content.Headers.ContentType.MediaType, @"^application/json$"));
                if (part.IsNotNull())
                {
                    var contentDisposition = part.Headers.ContentDisposition;
                    var contentType = part.Headers.ContentType.MediaType;
                    string fileName = (contentDisposition.IsNotNull()) ? contentDisposition.FileName.Replace("\"", string.Empty) : null;
                    Stream stream = part.ReadAsStreamAsync().Result;
                    return new File(stream, fileName, contentType);
                }
            }
            return null;
        }
    }
}