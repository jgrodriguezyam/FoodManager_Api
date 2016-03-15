using FoodManager.Infrastructure.Files;
using FoodManager.Infrastructure.Strings;

namespace FoodManager.Mapper.Resolvers
{
    public static class MapperResolver
    {
        public static string MultimediaPath(string fileName)
        {
            if(fileName.IsNotNullOrEmpty())
                return string.Format("{0}{1}{2}", ServerDomainResolver.GetCurrentDomain(), FileSettings.ContentFolder, fileName);
            return string.Empty;
        }
    }
}