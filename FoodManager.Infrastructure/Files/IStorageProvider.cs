using System.IO;

namespace FoodManager.Infrastructure.Files
{
    public interface IStorageProvider
    {
        string Save(IFile file);
        Stream Retrieve(string filePath);
        void Delete(string filePath);
        string DomainResolver();
    }
}