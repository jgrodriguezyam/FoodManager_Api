using System.Configuration;

namespace FoodManager.Infrastructure.Mails
{
    public static class MailSettings
    {
        public static string UserName = ConfigurationManager.AppSettings["UserName"]; 

        public static string Password = ConfigurationManager.AppSettings["Password"];

        public static string SmtpHost = ConfigurationManager.AppSettings["SmtpHost"];
    }
}