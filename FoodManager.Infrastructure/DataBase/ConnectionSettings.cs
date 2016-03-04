using System.Configuration;

namespace FoodManager.Infrastructure.DataBase
{
    public class ConnectionSettings
    {
        public static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionStringFoodManager"].ConnectionString;
        } 
    }
}