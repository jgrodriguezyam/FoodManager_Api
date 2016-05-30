using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FoodManager.Infrastructure.Files
{
    public class StorageProvider : IStorageProvider
    {
        private readonly IFileResolver _fileResolver;
        private readonly string _destinationPath = FileSettings.ContentFolder;

        public StorageProvider(IFileResolver fileResolver)
        {
            _fileResolver = fileResolver;
        }

        public string Save(IFile file)
        {
            var fullFileName = string.Format("{0}.{1}", Guid.NewGuid(), file.Extension);
            var physicalPath = string.Concat(_fileResolver.Resolve(_destinationPath), fullFileName);

            using (var fileStream = System.IO.File.Create(physicalPath))
            {
                file.Stream.Seek(0, SeekOrigin.Begin);
                file.Stream.CopyTo(fileStream);
            }

            return fullFileName;
        }

        public Stream Retrieve(string filePath)
        {
            var fileLocation = _fileResolver.Resolve(_destinationPath + filePath);
            var fileStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.Read);
            return fileStream;
        }

        public void Delete(string filePath)
        {
            var fileLocation = _fileResolver.Resolve(_destinationPath + filePath);
            System.IO.File.Delete(fileLocation);
        }

        public string DomainResolver()
        {
            var appPath = string.Format("{0}{1}", ServerDomainResolver.GetCurrentDomain(), _destinationPath);
            return appPath;
        }

        public IEnumerable<string> ReadAllLinesCsv(string filePath)
        {
            var physicalPath = string.Concat(_fileResolver.Resolve(_destinationPath), filePath);
            var csvLines = System.IO.File.ReadAllLines(physicalPath).Skip(1);
            return csvLines;
        }
    }
}