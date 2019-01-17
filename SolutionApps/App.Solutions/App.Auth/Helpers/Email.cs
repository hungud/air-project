using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;

namespace App.Auth.Helpers
{
    public static class Email
    {
        public static string Send(string userEmail, string emailBody, string subject,string fromemail)
        {
            //var adminEmail = ConfigurationManager.AppSettings["adminEmail"].ToString();
            //var adminEmailPass = ConfigurationManager.AppSettings["emailPass"].ToString();
            //var hostName = ConfigurationManager.AppSettings["hostName"].ToString();
            //var port = ConfigurationManager.AppSettings["emailPort"];



            //int portNum = 25;
            //if (port != null)
            //{
            //    var isInt = int.TryParse(port, out portNum);
            //    if (!isInt)
            //    {
            //        portNum = 25;
            //    }
            //}

            //MailMessage mail = new MailMessage(adminEmail, userEmail);
            //SmtpClient client = new SmtpClient();
            //client.Port = portNum;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = true;
            //client.Credentials = new NetworkCredential(adminEmail, adminEmailPass);
            //client.EnableSsl = true;
            //client.Host = hostName;
            //mail.Subject = subject;
            //mail.Body = emailBody;
            //mail.IsBodyHtml = true;
            string result = "sent successfully";
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("webmail.skylinkgroup.com");
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(fromemail);

                mail.To.Add(new MailAddress(userEmail));


                mail.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["recepientEmailBCC"]));
                mail.Subject = subject;
                mail.Body = emailBody;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("reservations@skyflight.ca", "");
                //SmtpServer.EnableSsl = true;


                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                //throw;
            }
            return result;
            //client.Send(mail);
        }
    }
}
