using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace SACS.Library.Authentication
{
    public class SabreSOAPAuthentication
    {
        public void CallWebService()
        {
            var _url = "https://webservices3.sabre.com";
            var _action = "SessionCreateRQ";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            WebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();
            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);
            }
        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private WebRequest CreateWebRequest(string url, string action)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            WebRequest webRequest = WebRequest.Create(url);
            //webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "application/soap+xml;charset=UTF-8;action=\"SOAP:Action\"";
            //webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            //webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
        private static XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelop = new XmlDocument();
            string requestXml = "H:/NanoJot/Requests/sessionreq.xml";
            soapEnvelop.Load(requestXml);
            //soapEnvelop.LoadXml(@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/1999/XMLSchema""><SOAP-ENV:Body><HelloWorld xmlns=""http://tempuri.org/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><int1 xsi:type=""xsd:integer"">12</int1><int2 xsi:type=""xsd:integer"">32</int2></HelloWorld></SOAP-ENV:Body></SOAP-ENV:Envelope>");
            return soapEnvelop;
        }
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, WebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
        public void Execute()
        {
            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string requestXml = "H:/NanoJot/Requests/sessionreq.xml";
            //soapEnvelopeXml.Load(requestXml);
            soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <HelloWorld xmlns=""http://tempuri.org/"" />
                  </soap:Body>
                </soap:Envelope>");
            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        string soapResult = rd.ReadToEnd();
                        Console.WriteLine(soapResult);
                    }
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
            }
        }

        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"https://webservices3.sabre.com/ ");
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                webRequest.Headers.Add("SOAPAction", "\"https://webservices3.sabre.com/SessionCreateRQ\"");
                //webRequest.Headers.Add(@"SOAP:Action");
                //webRequest.ProtocolVersion = HttpVersion.Version11;
                //webRequest.ContentType = "application/x-www-form-urlencoded";
                //webRequest.Accept = "text/xml";
                webRequest.Method = "POST";
                webRequest.Headers.Clear();
                webRequest.AllowAutoRedirect = true;
                webRequest.Timeout = 1000 * 5000;
                webRequest.PreAuthenticate = true;
                webRequest.ContentType = "application / x - www - form - urlencoded";
                webRequest.Credentials = CredentialCache.DefaultCredentials;
                webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                webRequest.Timeout = 150000;
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream())
                                      .ReadToEnd();
            }
            return webRequest;
        }

        String sessionconversionid = "";
        String binarysecuritytoken = "";
        private String replaceMessage(String msg)
        {

            String message = msg.Replace(" xmlns:soap-env=\"http://schemas.xmlsoap.org/soap/envelope/\"", "");
            message = message.Replace(" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\"", "");
            message = message.Replace(" xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\"", "");

            message = message.Replace(" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\"", "");
            message = message.Replace(" xmlns:swse=\"http://wse.sabre.com/eventing\"", "");
            message = message.Replace(" xmlns:wsa=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\"", "");
            message = message.Replace(" xmlns:wse=\"http://schemas.xmlsoap.org/ws/2004/08/eventing\"", "");
            message = message.Replace(" eb:version=\"1.0\" soap-env:mustUnderstand=\"1\"", "");
            message = message.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>", "");

            message = message.Replace("wse:", "");

            message = message.Replace("wsse:", "");
            message = message.Replace("swse:", "");
            message = message.Replace("eb:", "");
            message = message.Replace("wsa:", "");
            message = message.Replace("sCCC.", "");
            message = message.Replace("soap-env:", "");

            return message;

        }

        /// <returns>ResultCode, 1 if success.</returns>
        private void QueueRead()
        {

            String xml = "";//textRequest.Text;
            XmlDocument doc = new XmlDocument();
            doc.Load("D:/Works/SABRE/docs/queueread.xml");
            xml = doc.OuterXml;
            xml = xml.Replace("####", sessionconversionid);
            xml = xml.Replace("%%%%", binarysecuritytoken);
            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            String pcc = ConfigurationManager.AppSettings["pcc"].ToString();
            String url = ConfigurationManager.AppSettings["webserviceurl"].ToString();

            xml = xml.Replace("SABREUSERNAME", username);
            xml = xml.Replace("SABREPASSWORD", password);
            xml = xml.Replace("SABREPCC", pcc);
            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(xml);
                }
            }

            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                result = responseReader.ReadToEnd();
                result = replaceMessage(result);
                //textResult.Text = result;
            }
        }

        /// <returns>ResultCode, 1 if success.</returns>
        private void QueueCount()
        {
            String xml = "";// textRequest.Text;
            XmlDocument doc = new XmlDocument();
            doc.Load("D:/Works/SABRE/docs/queuecount.xml");
            xml = doc.OuterXml;
            xml = xml.Replace("####", sessionconversionid);
            xml = xml.Replace("%%%%", binarysecuritytoken);
            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            String pcc = ConfigurationManager.AppSettings["pcc"].ToString();
            String url = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            xml = xml.Replace("SABREUSERNAME", username);
            xml = xml.Replace("SABREPASSWORD", password);
            xml = xml.Replace("SABREPCC", pcc);
            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(xml);
                }
            }
            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                result = responseReader.ReadToEnd();
                result = replaceMessage(result);
                //textResult.Text = result;
            }
        }

        /// <returns>ResultCode, 1 if success.</returns>
        private void RetrieveCurrency()
        {
            String xml = "";// textRequest.Text;
            XmlDocument doc = new XmlDocument();
            doc.Load("D:/Works/SABRE/docs/currency.xml");
            xml = doc.OuterXml;
            xml = xml.Replace("####", sessionconversionid);
            xml = xml.Replace("%%%%", binarysecuritytoken);
            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            String pcc = ConfigurationManager.AppSettings["pcc"].ToString();
            String url = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            xml = xml.Replace("SABREUSERNAME", username);
            xml = xml.Replace("SABREPASSWORD", password);
            xml = xml.Replace("SABREPCC", pcc);
            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(xml);
                }
            }
            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                result = responseReader.ReadToEnd();
                result = replaceMessage(result);
                //textResult.Text = result;
            }
        }
        /// <returns>ResultCode, 1 if success.</returns>
        private void RetrievePNR()
        {
            //String xml = textRequest.Text;
            XmlDocument doc = new XmlDocument();
            //doc.Load("D:/Works/SABRE/docs/pr.xml");
            String xml = doc.OuterXml;
            xml = xml.Replace("####", sessionconversionid);
            xml = xml.Replace("%%%%", binarysecuritytoken);
            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            String pcc = ConfigurationManager.AppSettings["pcc"].ToString();
            xml = xml.Replace("SABREUSERNAME", username);
            xml = xml.Replace("SABREPASSWORD", password);
            xml = xml.Replace("SABREPCC", pcc);
            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(xml);
                }
            }
            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                result = responseReader.ReadToEnd();
                result = replaceMessage(result);
                //textResult.Text = result;
            }
        }

        public string postXMLData()
        {
            //SessionCreate();
            string requestXml = @"C:\Users\sandeep.chaudhary\Desktop\AAI-N1Key\SessionRQ.xml";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            try
            {
                request.Headers.Add("SOAPAction", "SessionCreateRQ");
                request.ContentType = "text/xml; encoding='utf-8'";
                request.Accept = "gzip,deflate";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.Proxy = WebRequest.GetSystemWebProxy();
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                return pageContent;
            }
            return null;
        }

        /// <returns>ResultCode, 1 if success.</returns>
        public string SessionCreate()
        {
            //postXMLData();
            XmlDocument doc = new XmlDocument();
            //doc.LoadXml(getBargainRequest("T1RLAQL/GgCMc97C6LnN2KIm5hTpXWC8mBCDzupWj23IYuSdKhJEfg01AACgVj4Om7b7x+Zxj/HTT/MbAd37MP/i1K/B8Yvspx8AcqhcsvKKI3kX47g4FcywxFReyt1ja//1/9g97KRBoB9RLiojLy6Tej959fzZFLHIwq558RLtaMKXafRteiLuDJ2ppY+v7G2yYjmJZF8B8zVDOzzV7S9RUN6/N8H+jzYYWao9PvrKVOD2U6MtUPvqP6DQqHYaTGfXJ3V8WeS0a6Ptuw**"));
            //doc.Load("H:/NanoJot/Requests/sessionreq.xml");
            doc.LoadXml(getSessionRequest());
            string result = "";
            string URL_ADDRESS = ConfigurationManager.AppSettings["webserviceurl"].ToString();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "gzip,deflate";
            req.Method = "POST";
            using (Stream stm = req.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(doc.OuterXml);
                }
            }
            try
            {
                using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    result = responseReader.ReadToEnd();
                    result = replaceMessage(result);
                    XmlDocument doc1 = new XmlDocument();
                    doc1.LoadXml(result);
                    sessionconversionid = doc1.SelectSingleNode("//ConversationId").InnerText;
                    binarysecuritytoken = doc1.SelectSingleNode("//BinarySecurityToken").InnerText;
                    return result;
                }
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                return pageContent;
            }
        }

        private String getSessionRequest()
        {

            String username = ConfigurationManager.AppSettings["username"].ToString();
            String password = ConfigurationManager.AppSettings["password"].ToString();
            String pcc = "6DTH";
            StringBuilder bldr = new StringBuilder();
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:mes=\"http://www.ebxml.org/namespaces/messageHeader\" xmlns:ns=\"http://www.opentravel.org/OTA/2002/11\">");
            bldr.Append(" <soapenv:Header>");
            bldr.Append("     <sec:Security  xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("        <sec:UsernameToken>");
            bldr.Append("           <sec:Username>" + username + "</sec:Username>");
            bldr.Append("          <sec:Password>" + password + "</sec:Password>");
            bldr.Append("            <Organization>" + pcc + "</Organization>");
            bldr.Append("           <Domain>Default</Domain>");
            bldr.Append("         </sec:UsernameToken>");
            bldr.Append("     </sec:Security>");
            bldr.Append("      <mes:MessageHeader  mes:mustUnderstand=\"1\" mes:id=\"1000\" mes:version=\"3.6.0\">");
            bldr.Append("        <mes:From>");
            bldr.Append("          <mes:PartyId mes:type=\"urn: x12.org:IO5:01\">999999</mes:PartyId>");
            bldr.Append("        </mes:From>");
            bldr.Append("       <mes:To>");
            bldr.Append("           <mes:PartyId mes:type=\"urn: x12.org:IO5:01\">123123</mes:PartyId>");
            bldr.Append("         </mes:To>");
            bldr.Append("         <mes:CPAId>" + pcc + "</mes:CPAId>");
            bldr.Append("        <mes:ConversationId>1111111111111111111</mes:ConversationId>");
            bldr.Append("        <mes:Service mes:type=\"OTA\">SessionCreateRQ</mes:Service>");
            bldr.Append("        <mes:Action>SessionCreateRQ</mes:Action>");
            bldr.Append("         <mes:MessageData>");
            bldr.Append("           <mes:MessageId>1000</mes:MessageId>");
            bldr.Append("            <mes:Timestamp>2016-05-11T19:20:10Z</mes:Timestamp>");
            bldr.Append("            <mes:TimeToLive>2016-05-11T19:20:10Z</mes:TimeToLive>");
            bldr.Append("         </mes:MessageData>");
            bldr.Append("     </mes:MessageHeader>");
            bldr.Append("   </soapenv:Header>");
            bldr.Append("  <soapenv:Body>");
            bldr.Append("      <ns:SessionCreateRQ>");
            bldr.Append("         <ns:POS>");
            bldr.Append("            <ns:Source PseudoCityCode=\"" + pcc + "\"/>");
            bldr.Append("         </ns:POS>");
            bldr.Append("      </ns:SessionCreateRQ>");
            bldr.Append("  </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();
        }

        private String getBargainRequest(string token)
        {
            string one = "1";
            StringBuilder bldr = new StringBuilder();
            string pcc = "6DTH";
            bldr.Append("<?xml version='1.0' encoding='UTF-8'?>");
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:eb=\"http://www.ebxml.org/namespaces/messageHeader\" xmlns:ns1=\"http://www.opentravel.org/OTA/2002/11\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("  <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("       <eb:From>");
            bldr.Append("           <eb:PartyId eb:type=\"urn: x12.org:IO5: 01\">999999</eb:PartyId>");
            bldr.Append("       </eb:From>");
            bldr.Append("       <eb:To>");
            bldr.Append("           <eb:PartyId type=\"urn:x12.org:IO5:01\">123123</eb:PartyId>");
            bldr.Append("       </eb:To>");
            bldr.Append("       <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("       <eb:ConversationId>1111111111111111111</eb:ConversationId>");
            bldr.Append("       <eb:Service eb:type=\"OTA\">Air Shopping Service</eb:Service>");
            bldr.Append("       <eb:Action>BargainFinderMaxRQ</eb:Action>");
            bldr.Append("       <eb:MessageData>");
            bldr.Append("           <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("           <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("           <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("       </eb:MessageData>");
            bldr.Append("       <eb:DuplicateElimination/>");
            bldr.Append("       <eb:Description>Bargain Finder Max Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");

            bldr.Append("     <sec:Security  xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("       <sec:BinarySecurityToken>" + token + "</sec:BinarySecurityToken>");
            bldr.Append("    </sec:Security>");
            bldr.Append("</soapenv:Header>");

            bldr.Append("<soapenv:Body>");
            bldr.Append("      <ns1:OTA_AirLowFareSearchRQ Version=\"1.2.7\" xmlns:ns1=\"http://www.opentravel.org/OTA/2003/05\">");
            bldr.Append("          <ns1:POS>");
            bldr.Append("              <ns1:Source PseudoCityCode=\"6DTH\">");
            bldr.Append("                  <ns1:RequestorID ID=" + one + " Type= " + one + ">");
            bldr.Append("                      <ns1:CompanyName Code=\"TN\">TN</ns1:CompanyName>");
            bldr.Append("                  </ns1:RequestorID>");
            bldr.Append("              </ns1:Source>");
            bldr.Append("          </ns1:POS>");

            bldr.Append("      <ns1:OriginDestinationInformation RPH=" + one + ">");
            bldr.Append("          <ns1:DepartureDateTime>2014-10-31T00:00:00</ns1:DepartureDateTime>");
            bldr.Append("          <ns1:OriginLocation LocationCode=\"BWI\"/>");
            bldr.Append("          <ns1:DestinationLocation LocationCode=\"MBJ\"/>");

            bldr.Append("          <ns1:TPA_Extensions>");
            bldr.Append("              <ns1:SegmentType Code=\"O\"/>");
            bldr.Append("          </ns1:TPA_Extensions>");
            bldr.Append("      </ns1:OriginDestinationInformation>");

            bldr.Append("      <ns1:OriginDestinationInformation RPH=\"2\">");
            bldr.Append("          <ns1:DepartureDateTime>2014-11-03T00:00:00</ns1:DepartureDateTime>");
            bldr.Append("          <ns1:OriginLocation LocationCode=\"MBJ\"/>");
            bldr.Append("          <ns1:DestinationLocation LocationCode=\"BWI\"/>");
            bldr.Append("          <ns1:TPA_Extensions>");
            bldr.Append("              <ns1:SegmentType Code=\"O\"/>");
            bldr.Append("          </ns1:TPA_Extensions>");
            bldr.Append("      </ns1:OriginDestinationInformation>");

            bldr.Append("      <ns1:TravelPreferences>");
            bldr.Append("          <ns1:CabinPref Cabin=\"Y\" PreferLevel=\"Preferred\"/>");
            bldr.Append("          <ns1:TPA_Extensions>");
            bldr.Append("              <ns1:TripType Value=\"Return\"/>");
            bldr.Append("          </ns1:TPA_Extensions>");
            bldr.Append("      </ns1:TravelPreferences>");

            //bldr.Append("      <ns1:TravelerInfoSummary>");
            //bldr.Append("          <ns1:SeatsRequested>1</ns1:SeatsRequested>");
            //bldr.Append("          <ns1:AirTravelerAvail>");
            //bldr.Append("              <ns1:PassengerTypeQuantity Code=\"ADT\" Quantity=\"2\"/>");
            //bldr.Append("          </ns1:AirTravelerAvail>");
            //bldr.Append("      </ns1:TravelerInfoSummary>");

            //bldr.Append("      <ns1:TPA_Extensions>");
            //bldr.Append("          <ns1:IntelliSellTransaction>");
            //bldr.Append("              <ns1:RequestType Name=\"50ITINS\"/>");
            //bldr.Append("          </ns1:IntelliSellTransaction>");
            //bldr.Append("      </ns1:TPA_Extensions>");

            bldr.Append("      </ns1:OTA_AirLowFareSearchRQ>");
            bldr.Append("     </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }

        private string getBookRequest_ShortSellRQ(string token, string pcc, string convoid)
        {
            string one = "1";
            StringBuilder bldr = new StringBuilder();
            bldr.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:mes=\"http://www.ebxml.org/namespaces/messageHeader\" xmlns:ns=\"http://www.opentravel.org/OTA/2002/11\">");
            bldr.Append("<soapenv:Header>");
            bldr.Append("   <eb:MessageHeader soapenv:mustUnderstand=\"1\" eb:version=\"1.0\">");
            bldr.Append("   <eb:From>");
            bldr.Append("       <mes:PartyId mes:type=\"urn: x12.org:IO5: 01\">999999</mes:PartyId>");
            bldr.Append("   </eb:From>");
            bldr.Append("   <eb:To>");
            bldr.Append("       <eb:PartyId type=\"urn:x12.org:IO5:01\">webservices.sabre.com</eb:PartyId>");
            bldr.Append("   </eb:To>");
            bldr.Append("   <eb:CPAId>" + pcc + "</eb:CPAId>");
            bldr.Append("   <eb:ConversationId>" + convoid + "</eb:ConversationId>");
            bldr.Append("   <eb:Service eb:type=\"sabreXML\">Air Shopping Service</eb:Service>");
            bldr.Append("   <eb:Action>ShortSellRQ</eb:Action>");
            bldr.Append("   <eb:MessageData>");
            bldr.Append("       <eb:MessageId>mid:0_2014-09-22T21:43:40m</eb:MessageId>");
            bldr.Append("       <eb:Timestamp>2014-09-22T21:43:40</eb:Timestamp>");
            bldr.Append("       <eb:TimeToLive>2014-09-22T21:43:40</eb:TimeToLive>");
            bldr.Append("    </eb:MessageData>");
            bldr.Append("    <eb:DuplicateElimination/>");
            bldr.Append("    <eb:Description>Bargain Finder Max Service</eb:Description>");
            bldr.Append("   </eb:MessageHeader>");

            bldr.Append("<wsse:Security xmlns:wsse=\"http://schemas.xmlsoap.org/ws/2002/12/secext\" xmlns:wsu=\"http://schemas.xmlsoap.org/ws/2002/12/utility\">");
            bldr.Append("   <wsse:BinarySecurityToken>" + token + "</wsse:BinarySecurityToken>");
            bldr.Append("</wsse:Security>");

            bldr.Append("</soapenv:Header>");
            bldr.Append("<soapenv:Body>");
            bldr.Append("      <ns1:ShortSellRQ xmlns=\"http://webservices.sabre.com/sabreXML/2011/10\" Version=\"2.0.1\" xmlns:ns1=\"http://www.opentravel.org/OTA/2003/05\">");

            //bldr.Append("      <ns1:OriginDestinationInformation>");
            //bldr.Append("          <ns1:FlightSegment DepartureDateTime="03-12" FlightNumber="453" NumberInParty="1" ResBookDesigCode="Y" Status="NN">2014-10-31T00:00:00</ns1:DepartureDateTime>");
            //bldr.Append("               <ns1:OriginLocation LocationCode=\"BWI\"/>");
            //bldr.Append("               <ns1:MarketingAirline Code="LO" FlightNumber="453"/>");
            //bldr.Append("               <ns1:DestinationLocation LocationCode=\"MBJ\"/>");
            //bldr.Append("          </ns1:FlightSegment>");

            //bldr.Append("          <ns1:FlightSegment DepartureDateTime="03-12" FlightNumber="453" NumberInParty="1" ResBookDesigCode="Y" Status="NN">2014-10-31T00:00:00</ns1:DepartureDateTime>");
            //bldr.Append("               <ns1:OriginLocation LocationCode=\"BWI\"/>");
            //bldr.Append("               <ns1:MarketingAirline Code="LO" FlightNumber="453"/>");
            //bldr.Append("               <ns1:DestinationLocation LocationCode=\"MBJ\"/>");
            //bldr.Append("          </ns1:FlightSegment>");
            //bldr.Append("      </ns1:OriginDestinationInformation>");

            bldr.Append("      </ns1:ShortSellRQ>");
            bldr.Append("     </soapenv:Body>");
            bldr.Append("</soapenv:Envelope>");
            return bldr.ToString();

        }
    }
}