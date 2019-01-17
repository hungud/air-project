namespace App.Base
{
    namespace BaseProxy
    {
        using System;
        using System.Collections.Generic;
        using System.Web;
        using System.IO;
        using System.Xml.Serialization;
        using System.Web.Caching;
        using System.Net;
        using System.Security.Cryptography.X509Certificates;


        #region BaseProxy Block

        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL            
        /// Company Name:   RMSI            
        /// Created Date:   Developed on: 07/10/2015      
        /// Summary     :   BaseProxy for ArcGIS Server Security Services.
        ///*************************************************
        /// </summary>

        public class MyPolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
            {
                //Return True to force the certificate to be accepted.
                return true;
            } // end CheckValidationResult
        }


        /// <summary>
        /// Summary description for Proxy
        /// </summary>
        public class BaseProxy : IHttpHandler, IDisposable
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void ProcessRequest(HttpContext context)
            {
                try
                {
                    HttpResponse response = context.Response;
                    // Get the URL requested by the client (take the entire querystring at once
                    //  to handle the case of the URL itself containing querystring parameters)
                    string uri = Uri.UnescapeDataString(context.Request.QueryString.ToString());
                    // Debug: Ping to make sure avaiable (ie. http://localhost/giproxy/WebSecuredService.ashx?ping)
                    if (uri.StartsWith("ping"))
                    {
                        context.Response.Write("Hello and Welcome RMSI Proxy Web Applications");
                        return;
                    }

                    // Get token, if applicable, and append to the request
                    string token = getTokenFromConfigFile(uri);
                    if (!String.IsNullOrEmpty(token))
                    {
                        if (uri.Contains("?"))
                            uri += "&token=" + token;
                        else
                            uri += "?token=" + token;
                    }
                    System.Net.WebRequest req = System.Net.WebRequest.Create(new Uri(uri));
                    req.Method = context.Request.HttpMethod;
                    // Set body of request for POST requests
                    if (context.Request.InputStream.Length > 0)
                    {
                        byte[] bytes = new byte[context.Request.InputStream.Length];
                        context.Request.InputStream.Read(bytes, 0, (int)context.Request.InputStream.Length);
                        req.ContentLength = bytes.Length;
                        req.ContentType = "application/x-www-form-urlencoded";
                        using (Stream outputStream = req.GetRequestStream())
                        {
                            outputStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    // Send the request to the server
                    System.Net.WebResponse serverResponse = null;
                    try
                    {
                        serverResponse = req.GetResponse();
                    }
                    catch (System.Net.WebException webExc)
                    {
                        response.StatusCode = 500;
                        response.StatusDescription = webExc.Status.ToString();
                        response.Write(webExc.Response);
                        response.End();
                        return;
                    }
                    // Set up the response to the client
                    if (serverResponse != null)
                    {
                        response.ContentType = serverResponse.ContentType;
                        using (Stream byteStream = serverResponse.GetResponseStream())
                        {
                            // Text response
                            if (serverResponse.ContentType.Contains("text"))
                            {
                                using (StreamReader sr = new StreamReader(byteStream))
                                {
                                    string strResponse = sr.ReadToEnd();
                                    response.Write(strResponse);
                                }
                            }
                            else
                            {
                                // Binary response (image, lyr file, other binary file)
                                BinaryReader br = new BinaryReader(byteStream);
                                byte[] outb = br.ReadBytes((int)serverResponse.ContentLength);
                                br.Close();
                                // Tell client not to cache the image since it's dynamic
                                response.CacheControl = "no-cache";
                                // Send the image to the client
                                // (Note: if large images/files sent, could modify this to send in chunks)
                                response.OutputStream.Write(outb, 0, outb.Length);
                            }
                            serverResponse.Close();
                        }
                    }
                    response.End();
                }
                catch (Exception ex)
                {
                    context.Response.Write("Hello and Welcome RMSI Proxy. This is Not Vailid Request Action.");
                    context.Response.Write("<p>Your Browser:</p>");
                    context.Response.Write("Type: " + context.Request.Browser.Type + "<br>");
                    context.Response.Write("Version: " + context.Request.Browser.Version);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="uri"></param>
            /// <returns></returns>
            private string getTokenFromConfigFile(string uri)
            {
                try
                {
                    ProxyConfig config = ProxyConfig.GetCurrentConfig();
                    if (config != null)
                    {
                        return config.GetToken(uri);
                    }
                    else
                    {
                        throw new ApplicationException("Proxy.config file does not exist at application root, or is not readable.");
                    }
                }
                catch (InvalidOperationException ivoex)
                {
                    // Proxy is being used for an unsupported service (proxy.config has mustMatch="true")
                    HttpResponse response = HttpContext.Current.Response;
                    response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    response.End();
                }
                catch (Exception e)
                {
                    if (e is ApplicationException)
                        throw e;
                    // just return an empty string at this point
                    // -- may want to throw an exception, or add to a log file
                }

                return string.Empty;
            }
            /// <summary>
            /// 
            /// </summary>
            public bool IsReusable
            {
                get
                {
                    return false;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlRoot("ProxyConfig")]
        public class ProxyConfig
        {
            #region Static Members

            private static object _lockobject = new object();
            public static ProxyConfig LoadProxyConfig(string fileName)
            {
                ProxyConfig config = null;
                lock (_lockobject)
                {
                    if (System.IO.File.Exists(fileName))
                    {
                        XmlSerializer reader = new XmlSerializer(typeof(ProxyConfig));
                        using (System.IO.StreamReader file = new System.IO.StreamReader(fileName))
                        {
                            config = (ProxyConfig)reader.Deserialize(file);
                        }
                    }
                }
                return config;
            }

            public static ProxyConfig GetCurrentConfig()
            {
                ProxyConfig config = HttpRuntime.Cache["gistokenproxyConfig"] as ProxyConfig;
                if (config == null)
                {
                    string fileName = GetFilename(HttpContext.Current);
                    config = LoadProxyConfig(fileName);
                    if (config != null)
                    {
                        CacheDependency dep = new CacheDependency(fileName);
                        HttpRuntime.Cache.Insert("gistokenproxyConfig", config, dep);
                    }
                }
                return config;
            }

            public static string GetFilename(HttpContext context)
            {
                //return context.Server.MapPath("~/Proxy.config");
                //return context.Server.MapPath("~/Proxy.config");
                return context.Server.MapPath("~/App_Data/Web.Configuration/Proxy.config");
            }
            #endregion

            ServerUrl[] serverUrls;
            bool mustMatch;

            [XmlArray("serverUrls")]
            [XmlArrayItem("serverUrl")]
            public ServerUrl[] ServerUrls
            {
                get { return this.serverUrls; }
                set { this.serverUrls = value; }
            }
            [XmlAttribute("mustMatch")]
            public bool MustMatch
            {
                get { return mustMatch; }
                set { mustMatch = value; }
            }
            public string GetToken(string uri)
            {
                foreach (ServerUrl su in serverUrls)
                {
                    if (su.MatchAll && uri.StartsWith(su.Url, StringComparison.InvariantCultureIgnoreCase) && su.DynamicToken)
                    {
                        string proxyToken = string.Empty;
                        if (HttpRuntime.Cache[su.Url] != null)
                        {
                            string existingToken = (HttpRuntime.Cache[su.Url] as
                            Dictionary<string, object>)["token"] as string;
                            DateTime expireTime = (DateTime)((HttpRuntime.Cache[su.Url] as Dictionary<string, object>)["timeout"]);
                            // If token not expired, return it
                            if (DateTime.Now.CompareTo(expireTime) < 0)
                                proxyToken = existingToken;
                        }
                        // If token not available or expired, generate one and store it in cache
                        if (string.IsNullOrEmpty(proxyToken))
                        {
                            // Code to dynamically get the token
                            //string tokenService = string.Format("http://{0}/ArcGIS/tokens?request=getToken&username={1}&password={2}&expiration=30", su.Host, su.UserName, su.Password);
                            string tokenService = string.Format("http://{0}/ArcGIS/tokens?request=getToken&username={1}&password={2}&expiration={3}", su.Host, su.UserName, su.Password, su.TokenExpiration);

                            // This script is added to force the application to certify the SSL script
                            //System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
                            System.Net.WebRequest tokenRequest = System.Net.WebRequest.Create(tokenService);
                            if (su.DomainAccess)
                            {
                                //tokenRequest.Proxy.Credentials = new NetworkCredential("<username>", "<password>", "<fully qualifed domain name>");
                                //tokenRequest.Proxy.Credentials = new NetworkCredential("webadmin", "web@dmin1", "MHSDC-MRSAC-WEB");
                                tokenRequest.Proxy.Credentials = new NetworkCredential(su.DomainUserName, su.DomainPassword, su.DomainName);
                            }

                            System.Net.WebResponse tokenResponse = tokenRequest.GetResponse();
                            System.IO.Stream responseStream = tokenResponse.GetResponseStream();
                            System.IO.StreamReader readStream = new System.IO.StreamReader(responseStream);
                            proxyToken = readStream.ReadToEnd();

                            Dictionary<string, object> serverItemEntries = new Dictionary<string, object>();
                            serverItemEntries.Add("token", proxyToken);
                            serverItemEntries.Add("timeout", DateTime.Now.AddMinutes(Convert.ToInt32(su.TokenExpiration)));
                            HttpRuntime.Cache.Insert(su.Url, serverItemEntries);
                        }
                        return proxyToken;

                    }
                    else if (su.MatchAll && uri.StartsWith(su.Url, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return su.Token;
                    }
                    else
                    {
                        if (String.Compare(uri, su.Url, StringComparison.InvariantCultureIgnoreCase) == 0)
                            return su.Token;
                    }
                }

                if (mustMatch)
                    throw new InvalidOperationException();

                return string.Empty;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public class ServerUrl
        {
            [XmlAttribute("url")]
            public string Url { get; set; }
            [XmlAttribute("matchAll")]
            public bool MatchAll { get; set; }
            [XmlAttribute("token")]
            public string Token { get; set; }
            [XmlAttribute("dynamicToken")]
            public bool DynamicToken { get; set; }
            [XmlAttribute("host")]
            public string Host { get; set; }
            [XmlAttribute("userName")]
            public string UserName { get; set; }
            [XmlAttribute("password")]
            public string Password { get; set; }
            [XmlAttribute("TokenExpiration")]
            public string TokenExpiration { get; set; }

            //Domain Auth
            [XmlAttribute("DomainAccess")]
            public bool DomainAccess { get; set; }
            [XmlAttribute("DomainName")]
            public string DomainName { get; set; }
            [XmlAttribute("DomainUserName")]
            public string DomainUserName { get; set; }
            [XmlAttribute("DomainPassword")]
            public string DomainPassword { get; set; }

            //Outh 2.0
            [XmlAttribute("clientId")]
            public string ClientId { get; set; }
            [XmlAttribute("clientSecret")]
            public string ClientSecret { get; set; }
            [XmlAttribute("accessToken")]
            public string AccessToken { get; set; }
            [XmlAttribute("tokenParamName")]
            public string TokenParamName { get; set; }
            [XmlAttribute("rateLimit")]
            public int RateLimit { get; set; }
            [XmlAttribute("rateLimitPeriod")]
            public int RateLimitPeriod { get; set; }
        }


        #endregion BaseProxy Block
    }
}