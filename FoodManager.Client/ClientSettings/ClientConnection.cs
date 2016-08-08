using System.Configuration;

namespace FoodManager.Client.ClientSettings
{
    public static class ClientConnection
    {
        public static string ServerApi => ConfigurationManager.AppSettings["SmartOrderService"];
    }
}