namespace FoodManager.Infrastructure.Files
{
    public interface IFileResolver
    {
        string Resolve(string filePath);
    }
}