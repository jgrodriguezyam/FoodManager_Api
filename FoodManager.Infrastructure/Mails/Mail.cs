using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.WebServices.Data;
using FoodManager.Infrastructure.Files;

namespace FoodManager.Infrastructure.Mails
{
    public static class Mail
    {
        public static bool Send(IEnumerable<string> to, string subject, string bodyMessage)
        {
            return Send(to, subject, bodyMessage, string.Empty, false, string.Empty);
        }

        public static bool Send(IEnumerable<string> to, string subject, string bodyMessage, string buttonUri, bool withButton, string buttonName)
        {
            try
            {
                const string image = "/Content/ProjectImage/Logo.png";
                const string backgroudImage = "/Content/ProjectImage/BG.png";
                var button = withButton
                    ? "<td align='center' valign='middle' class='kmButtonContent' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; color: white; font-family: Helvetica, Arial; font-size: 16px; padding: 15px; padding-top:15px;padding-bottom:15px;padding-left:15px;padding-right:15px;color:#ffffff;font-weight:bold;font-size:16px;font-family:Helvetica, Arial;'>" +
                          "<a class='kmButton ' title='' href='" + buttonUri + "' target='_self' style='word-wrap: break-word; font-weight: normal; letter-spacing: -0.5px; line-height: 100%; text-align: center; text-decoration: underline; color: #0000cd; font-family: Helvetica, Arial; font-size: 16px; text-decoration:initial;color:#ffffff;font-weight:bold;font-size:16px;font-family:Helvetica, Arial;padding-top:0;padding-bottom:0;'>" + 
                            buttonName + 
                          "</a>" +
                      "</td>"
                    : "";

                var msgHtml = "";
                msgHtml += "</head>" +
                           "<body style='margin: 0; padding: 0; background-image: url(" + ServerDomainResolver.GetCurrentDomain() + backgroudImage +
                           "); background-repeat: repeat; background-position: left top' >" +
                           "<center>" +
                           "<table align='center' border='0' cellpadding='0' cellspacing='0' id='bodyTable' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; padding: 0; height: 100%; margin: 0; width: 100%' background='" +
                           ServerDomainResolver.GetCurrentDomain() + backgroudImage + "'>" +
                           "<tbody>" +
                           "<tr>" +
                           "<td align='center' id='bodyCell' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; padding-top: 50px; padding-left: 20px; padding-bottom: 20px; padding-right: 20px; border-top: 0; height: 100%; margin: 0; width: 100%'>" +
                           "<table border='0' cellpadding='0' cellspacing='0' id='templateContainer' width='600' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0;'>" +
                           "<tbody>" +
                           "<tr>" +
                           "<td id='templateContainerInner' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; padding: 0'>" +
                           "<table border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tr>" +
                           "<td align='center' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<table border='0' cellpadding='0' cellspacing='0' class='templateRow' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody>" +
                           "<tr>" +
                           "<td class='rowContainer kmFloatLeft' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<table border='0' cellpadding='0' cellspacing='0' class='kmImageBlock' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody class='kmImageBlockOuter'>" +
                           "<tr>" +
                           "<td class='kmImageBlockInner' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; padding:9px;' valign='top'>" +
                           "<table align='left' border='0' cellpadding='0' cellspacing='0' class='kmImageContentContainer' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody>" +
                           "<tr>" +
                           "<td class='kmImageContent' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; padding: 0; padding-top:0px;padding-bottom:0;padding-left:9px;padding-right:9px;text-align: center;'>" +
                           "<img align='center' alt='' class='kmImage' src='" + ServerDomainResolver.GetCurrentDomain() + image +
                           "' width='200' style='border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; padding-bottom: 0; display: inline; vertical-align: bottom; max-width:200px;' />" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "<table border='0' cellpadding='0' cellspacing='0' width='100%' class='kmDividerBlock' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody class='kmDividerBlockOuter'>" +
                           "<tr>" +
                           "<td class='kmDividerBlockInner' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; padding-top:18px;padding-bottom:18px;padding-left:18px;padding-right:18px;'>" +
                           //"<table class='kmDividerContent' border='0' cellpadding='0' cellspacing='0' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; border-top-width:2px;border-top-style:solid;border-top-color:#ccc;'>" +
                           //"<tbody>" +
                           //"<tr><td style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'><span></span></td></tr>" +
                           //"</tbody>" +
                           //"</table>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "<table border='0' cellpadding='0' cellspacing='0' class='kmTextBlock' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody class='kmTextBlockOuter'>" +
                           "<tr>" +
                           "<td class='kmTextBlockInner' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; '>" +
                           "<table align='left' border='0' cellpadding='0' cellspacing='0' class='kmTextContentContainer' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody>" +
                           "<tr>" +
                           "<td class='kmTextContent' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; color: #ffffff; font-family: Helvetica, Arial; font-size: 14px; line-height: 150%; text-align: left; padding-top:9px;padding-bottom:9px;padding-left:18px;padding-right:18px;'>" +
                           "<span style='font-size:20px;'></span><span style='font-size:16px;'><span style='font-family:arial,helvetica,sans-serif;'><strong>" +
                           bodyMessage + "</strong></span></span>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</td></tr></tbody></table>" +
                           "<table border='0' cellpadding='0' cellspacing='0' width='100%' class='kmButtonBlock' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody class='kmButtonBlockOuter'>" +
                           "<tr>" +
                           "<td valign='top' align='center' class='kmButtonBlockInner' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; padding: 9px 18px; '>" +
                           "<table border='0' cellpadding='0' cellspacing='0' width='100%' class='kmButtonContentContainer' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0; border-top-left-radius: 5px; border-top-right-radius: 5px; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px; background-color: #999; background-color:#23AAE7;border-radius:15px;'>" +
                           "<tbody>" +
                           "<tr>" + button +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</td>" +
                           "</tr>" +
                           "<tr>" +
                           "<td align='center' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<table border='0' cellpadding='0' cellspacing='0' class='templateRow' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody>" +
                           "<tr>" +
                           "<td class='rowContainer kmFloatLeft firstColumn' valign='top' width='50%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "</td>" +
                           "<td class='rowContainer kmFloatLeft lastColumn' valign='top' width='50%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</td>" +
                           "</tr>" +
                           "<tr>" +
                           "<td align='center' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<table border='0' cellpadding='0' cellspacing='0' class='templateRow' width='100%' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "<tbody>" +
                           "<tr>" +
                           "<td class='rowContainer kmFloatLeft' valign='top' style='border-collapse: collapse; mso-table-lspace: 0; mso-table-rspace: 0'>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</td>" +
                           "</tr>" +
                           "</table>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</td>" +
                           "</tr>" +
                           "</tbody>" +
                           "</table>" +
                           "</center>" +
                           "</body>" +
                           "</html>";

                var service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                service.Url = new Uri(MailSettings.SmtpHost);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(MailSettings.UserName, MailSettings.Password);

                var message = new EmailMessage(service);
                message.Subject = subject;
                message.Body = msgHtml;
                message.ToRecipients.AddRange(to);
                message.Save();
                message.SendAndSaveCopy();
                
                //var newThread = new Thread(message.SendAndSaveCopy);
                //newThread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}