using System.IO;
using FoodManager.Infrastructure.Strings;

namespace FoodManager.Infrastructure.Files
{
    public class File : IFile
    {
        public File(Stream stream, string name, string contentType)
        {
            Stream = stream;
            Name = name;
            ContentType = contentType;
        }
        
        public Stream Stream { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }

        public string Extension
        {
            get { return Name.IsNotNullOrEmpty() ? (Path.GetExtension(Name) ?? string.Empty).TrimStart('.') : null; }
        }
    }
}