using System.Collections.Generic;
using System.Configuration;
using ServiceStack.Common.Extensions;
using FoodManager.Infrastructure.Files.ElementConfigs;

namespace FoodManager.Infrastructure.Files
{
    public static class FileSettings
    {        
        public readonly static int MaximumFileSize = int.Parse(ConfigurationManager.AppSettings["MaximumFileSize"]);

        public static IEnumerable<FileInstanceElement> ValidFiles
        {
            get
            {
                var sections = (FileConfigSection) ConfigurationManager.GetSection("AllowedFileExtensions");
                List<FileInstanceElement> fileElements = sections.Extensions.ToList<FileInstanceElement>();
                return fileElements;
            }
        }
        
        public readonly static string ContentFolder = ConfigurationManager.AppSettings["ContentMultimedia"];
    }
}