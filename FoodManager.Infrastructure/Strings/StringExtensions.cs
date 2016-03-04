using FoodManager.Infrastructure.Files;

namespace FoodManager.Infrastructure.Strings
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !IsNullOrEmpty(value);
        }

        public static string ToContentPath(this string self)
        {
            if (self.IsNotNullOrEmpty())
            {
                return string.Format("{0}{1}{2}", ServerDomainResolver.GetCurrentDomain(), FileSettings.ContentFolder, self);
            }
            return null;
        }
    }
}