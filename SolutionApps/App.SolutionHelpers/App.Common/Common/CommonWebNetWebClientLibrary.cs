namespace App.Common.WebNetWebClientLibrary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    namespace AspWebMail
    {
        using System.Web.Mail;
        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL                 
        /// Company Name:   RMSI Ltd            
        /// Created Date:   Developed on: 11/05/2016           
        /// Summary :
        ///*************************************************

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
                string ErrorMessage = "EXMESSAGE:-   " + Exce.Message + "  |    EXSOURCE:-    " + Exce.Source + "      |       EXTARGETSITE:-   " + Exce.TargetSite + "      |      EXData:-   " + Exce.Data + "      |     EXInnerException:-  " + Exce.InnerException;
                AspNetMail.CommonLibrary.SetLogMessage(ErrorMessage.ToString());
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
        /// Company Name:   RMSI Ltd            
        /// Created Date:   Developed on: 11/05/2016           
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
                string ErrorMessage = "EXMESSAGE:-   " + Exce.Message + "  |    EXSOURCE:-    " + Exce.Source + "      |       EXTARGETSITE:-   " + Exce.TargetSite + "      |      EXData:-   " + Exce.Data + "      |     EXInnerException:-  " + Exce.InnerException;
                SetLogMessage(ErrorMessage.ToString());

            }
            public static void SetLogMessage(string message)
            {
            }
        }
    }
    namespace BaseHttpWebClient
    {
        public class BaseHttpWebClient
        {
            public BaseHttpWebClient()
            {

            }

            public class ServiceReqModels
            {
                public string AppAccessToken { get; set; }
                public string AppServiceURI { get; set; }
                public List<string> QeryConditions { get; set; }

            }
            public class ServiceHCMRSRReqModels
            {
                public string SRNo { get; set; }
                public string STATUScode { get; set; }
                public string strstatusdesc { get; set; }
                public string strActionURL { get; set; }
            }
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="uri"></param>
            /// <returns></returns>
            public App.Common.CommonUtility GetServiceReqRes(ServiceReqModels ReqResServiseAccess)
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                try
                {
                    dynamic Mydata;
                    using (var Reqestclient = new System.Net.WebClient())
                    {
                        Reqestclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        Reqestclient.Headers.Add("Authorization", "Bearer " + ReqResServiseAccess.AppAccessToken);
                        Reqestclient.Headers.Add("Accept", "application/json");
                        Mydata = Reqestclient.DownloadString(ReqResServiseAccess.AppServiceURI);
                    }
                    Utility = new App.Common.CommonUtility()
                    {
                        Data = Mydata,
                        Message = "Sucess Full Respance",
                        Status = true,
                        ErrorCode = "0",
                    };
                }
                catch (InvalidOperationException ivoex)
                {
                }
                catch (Exception e)
                {
                }
                return Utility;
            }
            public App.Common.CommonUtility UpdateHCMRSR(ServiceHCMRSRReqModels HCMRSRDesc) // Invoked by oracle usp 
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                string returnstring = string.Empty;
                string fullUrl = string.Empty;
                dynamic Mydata;
                try
                {
                    //<add key="URLMSHCRMS" value="http://pwa-crmfe-u01:8001/HCRMS/HCRMSInboundService.svc"/>
                    string MSHCRMSURL = System.Configuration.ConfigurationManager.AppSettings["URLMSHCRMS"].ToString();
                    using (System.Net.WebClient proxy = new System.Net.WebClient())
                    {
                        proxy.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/json");
                        proxy.Headers.Add(System.Net.HttpRequestHeader.Accept, "application/json");
                        fullUrl = MSHCRMSURL + "/UpdateHCRMSServiceRequest/" + HCMRSRDesc.SRNo + "/" + HCMRSRDesc.STATUScode + "/" + HCMRSRDesc.strstatusdesc;
                        Mydata = proxy.UploadString(fullUrl, "post");
                    }
                    Utility = new App.Common.CommonUtility()
                    {
                        Data = Mydata,
                        Message = "Sucess Full Respance",
                        Status = true,
                        ErrorCode = "0",
                    };
                }
                catch (Exception ex)
                {
                    returnstring = ex.Message + "-" + fullUrl;

                }
                return Utility;
            }
            public App.Common.CommonUtility GetWebClient(ServiceHCMRSRReqModels HCMRSRDesc) // Invoked by oracle usp 
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                dynamic Mydata;
                try
                {
                    const string json = @"{
                                    ""User_Reqest"": 
                                        {
                                            ""ProductID"":""1"",
                                            ""UserId"":""lgn"",
                                            ""Token"":""lgn"",
                                            ""RoleId"":""123""
                                        }
                                    }";
                    Uri uri = new Uri("http://localhost:11012/Controllers/WCFService/WCFService.svc/GetData");
                    var wc = new System.Net.WebClient();
                    wc.Headers["Content-Type"] = "application/json";
                    var resJson = wc.UploadString(uri, "POST", json);





                    using (System.Net.WebClient proxy = new System.Net.WebClient())
                    {
                        proxy.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/json");
                        proxy.Headers.Add(System.Net.HttpRequestHeader.Accept, "application/json");
                        string fullUrl = HCMRSRDesc.strActionURL + "/Add/" + HCMRSRDesc.SRNo + "/" + HCMRSRDesc.STATUScode;
                        Mydata = proxy.UploadString(fullUrl, "post");
                    }
                    Utility = new App.Common.CommonUtility()
                    {
                        Data = Mydata,
                        Message = "Sucess Full Respance",
                        Status = true,
                        ErrorCode = "0",
                    };
                }
                catch (Exception ex)
                {
                }
                return Utility;
            }
            public App.Common.CommonUtility GetWebRequest(ServiceHCMRSRReqModels HCMRSRDesc) // Invoked by oracle usp 
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                string returnstring = string.Empty;
                string fullUrl = string.Empty;
                dynamic Mydata;
                try
                {
                    //<add key="URLMSHCRMS" value="http://pwa-crmfe-u01:8001/HCRMS/HCRMSInboundService.svc"/>
                    string MSHCRMSURL = System.Configuration.ConfigurationManager.AppSettings["URLMSHCRMS"].ToString();
                    using (System.Net.WebClient proxy = new System.Net.WebClient())
                    {
                        proxy.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/json");
                        proxy.Headers.Add(System.Net.HttpRequestHeader.Accept, "application/json");
                        fullUrl = MSHCRMSURL + "/UpdateHCRMSServiceRequest/" + HCMRSRDesc.SRNo + "/" + HCMRSRDesc.STATUScode + "/" + HCMRSRDesc.strstatusdesc;
                        Mydata = proxy.UploadString(fullUrl, "post");
                    }
                    Utility = new App.Common.CommonUtility()
                    {
                        Data = Mydata,
                        Message = "Sucess Full Respance",
                        Status = true,
                        ErrorCode = "0",
                    };
                }
                catch (Exception ex)
                {
                    returnstring = ex.Message + "-" + fullUrl;

                }
                return Utility;
            }
            public App.Common.CommonUtility GetHttpClient(ServiceHCMRSRReqModels HCMRSRDesc) // Invoked by oracle usp 
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                string returnstring = string.Empty;
                string fullUrl = string.Empty;
                dynamic Mydata;
                try
                {
                    ////<add key="URLMSHCRMS" value="http://pwa-crmfe-u01:8001/HCRMS/HCRMSInboundService.svc"/>
                    //string MSHCRMSURL = System.Configuration.ConfigurationManager.AppSettings["URLMSHCRMS"].ToString();
                    //using (var proxy = new System.Net.Http.HttpClient())
                    //{
                    //    proxy.BaseAddress = new Uri("http://70.0.111.17");
                    //    proxy.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //    //option 1, you can just pass an object
                    //    var responseString = proxy.PostAsync("VerifyEmail", new ApplicationRequest { email = "email.asdlkfj" }).Result.Content.ReadAsStringAsync().Result;
                    //    //option 2, you can pass plain json string
                    //    var req = new HttpRequestMessage(HttpMethod.Post, "VerifyEmail");
                    //    req.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    //    var response2String = client.SendAsync(req).Result.Content.ReadAsStringAsync().Result;
                    //}
                    //Utility = new App.Common.CommonUtility()
                    //{
                    //    Data = Mydata,
                    //    Message = "Sucess Full Respance",
                    //    Status = true,
                    //    ErrorCode = "0",
                    //};
                }
                catch (Exception ex)
                {
                    returnstring = ex.Message + "-" + fullUrl;

                }
                return Utility;
            }

        }
    }
}