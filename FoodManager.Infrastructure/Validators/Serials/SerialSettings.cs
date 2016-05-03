using System.Configuration;

namespace FoodManager.Infrastructure.Validators.Serials
{
    public static class SerialSettings
    {
        public readonly static string Serial = ConfigurationManager.AppSettings["Serial"];
    }
}