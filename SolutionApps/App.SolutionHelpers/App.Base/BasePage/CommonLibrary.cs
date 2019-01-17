namespace App.Base
{
    namespace Common
    {
        using System;
        using System.Collections;
        namespace AspWebMail
        {
            using System.Web.Mail;
            /// <summary>
            ///*************************************************
            /// Developed By:   RAKESH PAL                
            /// Company Name:   VITAL Ltd           
            /// Created Date:   Developed on: 11/01/2011           
            /// Summary :
            ///*************************************************
            /// </summary>

            public static class CommonLibrary
            {
                public static class WebMailService
                {
                    public static void SetSendUserMail(string FromEmail, string ToEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {


                                System.Web.Mail.MailMessage Message = new System.Web.Mail.MailMessage();
                                Message.From = FromEmail;
                                Message.To = ToEmail;
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                System.Web.Mail.SmtpMail.SmtpServer = "webmail.skylinkgroup.com";
                                System.Web.Mail.SmtpMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                GetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMail(string ToEmail, string FromEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = FromEmail;
                                Message.To = ToEmail;
                                Message.Subject = MailSubject;
                                Message.Body = "*** DO NOT WRITE BELOW THIS LINE ***" + Environment.NewLine + Environment.NewLine + MailMessageBody + Environment.NewLine + Environment.NewLine + Environment.NewLine + "You can Call ESRI-India technical support center @1800-102-1918 toll free to log your call or to track your call status. Monday-Friday 9:30 Am to 6:00 PM.";
                                SmtpMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                GetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMailTo(string ToEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = "support.gis@niit-tech.com";
                                Message.Bcc = "support@esriindia.com";
                                Message.To = ToEmail;
                                Message.Subject = MailSubject;
                                Message.Body = "*** DO NOT WRITE BELOW THIS LINE ***" + Environment.NewLine + Environment.NewLine + MailMessageBody + Environment.NewLine + Environment.NewLine + Environment.NewLine + "You can Call ESRI-India technical support center @1800-102-1918 toll free to log your call or to track your call status. Monday-Friday 9:30 Am to 6:00 PM.";
                                SmtpMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                GetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMailWithCC(string ToEmail, string BCCEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(BCCEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = "support.gis@niit-tech.com";
                                Message.Bcc = BCCEmail;
                                Message.To = ToEmail;
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                SmtpMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                GetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMailWithCCBCC(string ToEmail, string CCEmail, string BCCEmail, string FromEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(CCEmail) && !string.IsNullOrEmpty(BCCEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = FromEmail;
                                Message.Bcc = BCCEmail;
                                Message.Cc = CCEmail;
                                Message.To = ToEmail;
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                SmtpMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                GetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMailWithAttachment(string ToEmail, string FromEmail, string MailSubject, string MailMessageBody, string MailAttachmentFileName)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = FromEmail;
                                Message.To = ToEmail;
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                Message.Attachments.Add(new System.Web.Mail.MailAttachment(MailAttachmentFileName));
                                IList msgAttachments = Message.Attachments;
                                SmtpMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                GetManageException(Ex);
                            }
                        }
                    }
                }

                public static void GetManageException(Exception Exce)
                {
                    ErrorsLog.ErrorsLogInstance.ManageException(Exce);
                }
            }
        }

        namespace AspNetMail
        {
            using System.Net.Mail;
            using System.Configuration;

            /// <summary>
            ///*************************************************
            /// Developed By:   RAKESH PAL                
            /// Company Name:   VITAL Ltd           
            /// Created Date:   Developed on: 11/01/2011           
            /// Summary :
            ///*************************************************
            /// </summary>

            public static class CommonLibrary
            {

                public static class NetMailService
                {
                    public static string StrSMTPServerIP = ConfigurationManager.AppSettings["SMTPServerIP"].ToString();
                    public static string StrSMTPPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

                    public static void SetSendUserMail(string FromEmail, string ToEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress("support.gis@niit-tech.com");
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }

                    public static void SetCRMSGISServerUserMailTo(string ToEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress("support.gis@niit-tech.com");
                                Message.Bcc.Add("support@esriindia.com");
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = "*** DO NOT WRITE BELOW THIS LINE ***" + Environment.NewLine + Environment.NewLine + MailMessageBody + Environment.NewLine + Environment.NewLine + Environment.NewLine + "You can Call ESRI-India technical support center @1800-102-1918 toll free to log your call or to track your call status. Monday-Friday 9:30 Am to 6:00 PM.";
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMailTo(string ToEmail, string MailSubject, string MailMessageBody, Attachment MyAttachment)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {

                                //Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                                //MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
                                //MailMessage email = new MailMessage("support.gis@niit-tech.com", ToEmail, MailSubject, MailMessageBody);
                                //SmtpClient mailClient = new SmtpClient();
                                //System.Net.NetworkCredential cred = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
                                //mailClient.Host = settings.Smtp.Network.Host;
                                //mailClient.Port = settings.Smtp.Network.Port;
                                //mailClient.Credentials = cred;
                                //mailClient.Send(email);

                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress("support.gis@niit-tech.com");
                                Message.Bcc.Add("support@esriindia.com");
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = "*** DO NOT WRITE BELOW THIS LINE ***" + Environment.NewLine + Environment.NewLine + MailMessageBody + Environment.NewLine + Environment.NewLine + Environment.NewLine + "You can Call ESRI-India technical support center @1800-102-1918 toll free to log your call or to track your call status. Monday-Friday 9:30 Am to 6:00 PM.";
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                if (MyAttachment != null)
                                {
                                    Message.Attachments.Add(MyAttachment);
                                }
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMail(string ToEmail, string FromEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress(FromEmail);
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = "*** DO NOT WRITE BELOW THIS LINE ***" + Environment.NewLine + Environment.NewLine + MailMessageBody + Environment.NewLine + Environment.NewLine + Environment.NewLine + "You can Call ESRI-India technical support center @1800-102-1918 toll free to log your call or to track your call status. Monday-Friday 9:30 Am to 6:00 PM.";
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }

                    public static void SetCRMSGISServerUserMailWithCC(string ToEmail, string BCCEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(BCCEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress("support.gis@niit-tech.com");
                                Message.Bcc.Add(BCCEmail);
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMailWithCCBCC(string ToEmail, string CCEmail, string BCCEmail, string FromEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(CCEmail) && !string.IsNullOrEmpty(BCCEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress(FromEmail);
                                Message.Bcc.Add(BCCEmail);
                                Message.CC.Add(CCEmail);
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSGISServerUserMailWithAttachment(string ToEmail, string FromEmail, string MailSubject, string MailMessageBody, string MailAttachmentFileName)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress(FromEmail);
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                Message.Attachments.Add(new Attachment(MailAttachmentFileName));
                                IList msgAttachments = Message.Attachments;
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }

                    public static void SetSendMail(string FromEmail, string ToEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                //MailMessage objeto_mail = new MailMessage();
                                //SmtpClient client = new SmtpClient();
                                //client.Port = 25;
                                //client.Host = "webmail.skylinkgroup.com";
                                //client.Timeout = 10000;
                                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                //client.UseDefaultCredentials = false;
                                //client.Credentials = new System.Net.NetworkCredential("user", "Password");
                                //objeto_mail.From = new MailAddress("from@server.com");
                                //objeto_mail.To.Add(new MailAddress("to@server.com"));
                                //objeto_mail.Subject = "Password Recover";
                                //objeto_mail.Body = "Message";
                                //client.Send(objeto_mail);

                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress(FromEmail);
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                Message.IsBodyHtml = true;
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }

                    public static void SetCRMSVITALMail(string ToEmail, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress("support.gis@niit-tech.com");
                                //Message.Bcc.Add("support@esriindia.com");
                                Message.To.Add(ToEmail);
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                Message.IsBodyHtml = true;
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }
                    public static void SetCRMSVITALTOCCMail(string ToEmail, string ToCC, string MailSubject, string MailMessageBody)
                    {
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress("support.gis@niit-tech.com");
                                //Message.Bcc.Add("support@esriindia.com");
                                Message.To.Add(ToEmail);
                                Message.CC.Add(ToCC);
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                Message.IsBodyHtml = true;
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                            }
                            catch (Exception Ex)
                            {
                                SetManageException(Ex);
                            }
                        }
                    }

                    public static void SetSendMail(string FromEmail, string ToEmail, string CCEmail, string MailSubject, string MailMessageBody, out Boolean MailStatus)
                    {
                        MailStatus = false;
                        if (!string.IsNullOrEmpty(ToEmail) && !string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(MailSubject) && !string.IsNullOrEmpty(MailMessageBody))
                        {
                            try
                            {
                                MailMessage Message = new MailMessage();
                                Message.From = new MailAddress(FromEmail);
                                Message.To.Add(ToEmail);
                                Message.CC.Add(CCEmail);
                                Message.Subject = MailSubject;
                                Message.Body = MailMessageBody;
                                Message.IsBodyHtml = true;
                                SmtpClient SmptNetMail = new SmtpClient();//172.17.112.244//--//172.17.120.125
                                SmptNetMail.Host = StrSMTPServerIP;
                                SmptNetMail.Port = Convert.ToInt32(StrSMTPPort);
                                SmptNetMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                                SmptNetMail.Send(Message);
                                MailStatus = true;
                            }
                            catch (Exception Ex)
                            {
                                MailStatus = false;
                                SetManageException(Ex);
                            }
                        }
                    }

                }
                public static void SetManageException(Exception Exce)
                {
                    ErrorsLog.ErrorsLogInstance.ManageException(Exce);
                }
            }
        }

        namespace ASPNETGlobal
        {
            public static class ApplicationGloblManager
            {
                public static void GetSetKillSessionCache()
                {
                    //PrasentationCommonLibrary.AspNet.AspNetBasePage.MyUserMenusDataSet = new System.Data.DataSet();
                    ////PrasentationCommonLibrary.AspNet.AspNetBasePage.MyUserProfileCollection = new ESRIGIS.ESRIGISUserProfileLibrary();
                    //PrasentationCommonLibrary.AspNet.AspNetBasePage.MenuLoginUserRole = 9;
                    //System.Web.Security.FormsAuthentication.SignOut();
                    //HttpContext.Current.Session.Clear();
                    //HttpContext.Current.Session.RemoveAll();
                    //HttpContext.Current.Session.Abandon();
                    //PrasentationCommonLibrary.AspNet.AspNetBasePage.AuthenticatedUser = false;
                }
            }
        }

        public static class CommonMailManager
        {
            public static void SendMailManager(App.Common.CommonUtility Utility)
            {
                //sendEmail("rakeshp@nanojot.com", "rakeshp@nanojot.com", "Test Mail", "Test Mail");
                AspWebMail.CommonLibrary.WebMailService.SetSendUserMail("rakeshp@nanojot.com", "rakeshp@nanojot.com", "Test Mail", "Test Mail");
                AspNetMail.CommonLibrary.NetMailService.SetSendMail("rakeshp@nanojot.com", "rakeshp@nanojot.com", "Test Mail", "Test Mail");
                //webmail.skylinkgroup.com
                //SetSendMail
            }
            public static void sendEmail(string strFrom, string strTo, string strSubject, string strBody)
            {
                /// Author, Md. Marufuzzaman
                /// Created,
                /// Local dependency, Microsoft .Net framework
                /// Description, Send an email using (SMTP).
                // using System.Net.Mail;
                System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
                System.Net.NetworkCredential objSMTPUserInfo =new System.Net.NetworkCredential();
                System.Net.Mail.SmtpClient objSmtpClient = new System.Net.Mail.SmtpClient();
                try
                {
                    objMailMessage.From = new System.Net.Mail.MailAddress(strFrom);
                    objMailMessage.To.Add(new System.Net.Mail.MailAddress(strTo));
                    objMailMessage.Subject = strSubject;
                    objMailMessage.Body = strBody;

                    objSmtpClient = new System.Net.Mail.SmtpClient("172.0.0.1"); /// Server IP
                    objSMTPUserInfo = new System.Net.NetworkCredential
                    ("Admin", "rakeshpal", "WORKGROUP");
                    objSmtpClient.Credentials = objSMTPUserInfo;
                    objSmtpClient.UseDefaultCredentials = false;
                    objSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                    objSmtpClient.Send(objMailMessage);
                }
                catch (Exception ex)
                { throw ex; }

                finally
                {
                    objMailMessage = null;
                    objSMTPUserInfo = null;
                    objSmtpClient = null;
                }
            }
        }
    }
}