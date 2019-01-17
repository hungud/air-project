using Newtonsoft.Json;
using SACS.Library.Configuration;
using SACS.Library.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;

namespace SACS.Library.Authentication
{
    public class SabreRESTAuthentication
    {
        /// <summary>
        /// The REST authorization manager
        /// </summary>
        private readonly RestAuthorizationManager restAuthorizationManager;
        /// <summary>
        /// The configuration provider
        /// </summary>
        private readonly IConfigProvider config;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        /// <param name="restAuthorizationManager">The REST authorization manager.</param>
        /// <param name="config">The configuration provider.</param>
        public SabreRESTAuthentication(RestAuthorizationManager restAuthorizationManager, IConfigProvider config)
        {
            this.config = config;
            this.restAuthorizationManager = restAuthorizationManager;
        }


        private static volatile SabreRESTAuthentication SingletonAuthenticationInstance;
        private static object syncRoot = new Object();
        public SabreRESTAuthentication() { }
        public static SabreRESTAuthentication AuthenticationInstance
        {
            get
            {
                if (SingletonAuthenticationInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (SingletonAuthenticationInstance == null)
                            SingletonAuthenticationInstance = new SabreRESTAuthentication();
                    }
                }

                return SingletonAuthenticationInstance;
            }
        }



        String securityToken = "";
        /// <summary>
        /// call the service
        /// </summary>
        /// <param name="request"></param>
        /// <param name="sessionconversionid"></param>
        /// <param name="binarysecuritytoken"></param>
        /// <returns></returns>
        public String callServiceForMasters(Const.Mastertype type, String value)
        {
            //logger.Info("Calling WEBSERVICE...START");
            string result = "";
            try
            {
                string URL_ADDRESS = ConfigurationManager.AppSettings[type + "webserviceurl"].ToString();
                if (type == Const.Mastertype.airline)
                {
                    URL_ADDRESS = URL_ADDRESS + "?airlinecode=" + value;
                }
                if (type == Const.Mastertype.equipment)
                {
                    URL_ADDRESS = URL_ADDRESS + "?aircraftcode=" + value;
                }
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
                req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + securityToken);
                req.ContentType = "application/x-www-form-urlencoded";
                req.Accept = "application/json";
                req.Method = "GET";
                if (type == Const.Mastertype.airport)
                {
                    req.ContentType = "application/json";
                    req.Method = "POST";
                }
                if (type == Const.Mastertype.airport)
                {
                    String[] apts = value.Split(',');
                    StringBuilder bldr = new StringBuilder();
                    bldr.Append("[");
                    int count = 0;
                    foreach (String ap in apts)
                    {
                        if (count > 0)
                        {
                            bldr.Append(",");
                        }
                        bldr.Append("{\"GeoCodeRQ\":{");
                        bldr.Append("\"PlaceById\":{");
                        bldr.Append("\"Id\":\"" + ap + "\",");
                        bldr.Append("\"BrowseCategory\": {");
                        bldr.Append("\"name\": \"AIR\"");
                        bldr.Append(" } }}}");
                        count++;
                    }
                    bldr.Append("]");
                    using (Stream stm = req.GetRequestStream())
                    {
                        using (StreamWriter stmw = new StreamWriter(stm))
                        {
                            stmw.Write(bldr.ToString());
                        }
                    }
                }
                using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    result = responseReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                // logger.Error(ex);
                System.Console.WriteLine(ex);
            }
            finally
            {
                //  logger.Info("Calling WEBSERVICE...END");
            }
            return result;
        }

        private async Task<App.Common.CommonUtility> CallAuthRetry()
        {
            App.Common.CommonUtility Utility = new App.Common.CommonUtility();
            try
            {
                TokenHolder tokenHolder = await this.restAuthorizationManager.GetValidateAuthorizationTokenAsync(false);
                Utility = new App.Common.CommonUtility()
                {
                    Data = tokenHolder,
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


        public async Task<string> getCallAuthToken()
        {
           
            Dictionary<string, string> AppConfigData = new SACS.Library.Configuration.ConfigDataProvider().AppConfigProperties;
            string result = "";
            try
            {
                string URL_ADDRESS = "https://api.test.sabre.com/v2/auth/token"; // ConfigurationManager.AppSettings["authtokenurl"].ToString();
                //URL_ADDRESS = "https://api.sabre.com/v2/auth/token"; // ConfigurationManager.AppSettings["authtokenurl"].ToString();
                string clientsecret = ConfigurationManager.AppSettings["clientsecret"].ToString();
                string clientid = ConfigurationManager.AppSettings["clientID"].ToString();


                string clientId = this.restAuthorizationManager.CreateCredentialsString(clientid, clientsecret);
                var response = await this.restAuthorizationManager.AuthorizeAsync(clientId);
                if (response.IsSuccess)
                {
                    var value = response.Value;
                    TokenHolder tokenHolder = TokenHolder.Valid(value.AccessToken, value.ExpiresIn);
                }

                //base64 encode clientId and clientSecret
                var bytes = Encoding.UTF8.GetBytes(clientid);
                var base64clientid = Convert.ToBase64String(bytes);
                bytes = Encoding.UTF8.GetBytes(clientsecret);
                var base64clientsecret = Convert.ToBase64String(bytes);
                //base64clientid = "VjE6aG8xMWlwOTNkN29pMXk3YjpERVZDRU5URVI6RVhU";
                //base64clientsecret = "V2tTN3I3cE4=";
                //Concatenate encoded client and secret strings, separated with colon
                String encodedClientIdSecret = base64clientid + ":" + base64clientsecret;
                bytes = Encoding.UTF8.GetBytes(encodedClientIdSecret);
                encodedClientIdSecret = Convert.ToBase64String(bytes);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
                req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + encodedClientIdSecret);
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                using (Stream stm = req.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stm))
                    {
                        stmw.Write("grant_type=client_credentials");
                    }
                }
                try
                {
                    using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        result = responseReader.ReadToEnd();
                    }
                    var ser = new JavaScriptSerializer();
                    Dictionary<string, object> dict = ser.Deserialize<Dictionary<string, object>>(result);
                    securityToken = dict["access_token"].ToString();
                }
                catch (WebException wex)
                {
                    var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                    return pageContent;
                }
            }
            catch (Exception ex)
            {
                // logger.Error(ex);
                System.Console.WriteLine(ex);
            }
            finally
            {
            }
            return securityToken;
        }
        /// <summary>
        /// call the service
        /// </summary>
        /// <param name="request"></param>
        /// <param name="sessionconversionid"></param>
        /// <param name="binarysecuritytoken"></param>
        /// <returns></returns>
        public string getAuthToken()
        {
            Dictionary<string, string> AppConfigData = new SACS.Library.Configuration.ConfigDataProvider().AppConfigProperties;
            string result = "";
            try
            {
                String ClientIdSecretData = String.Empty;
                string URL_ADDRESS = "https://api.test.sabre.com/v2/auth/token"; // ConfigurationManager.AppSettings["authtokenurl"].ToString();
                //URL_ADDRESS = "https://api.sabre.com/v2/auth/token"; // ConfigurationManager.AppSettings["authtokenurl"].ToString();
                string clientsecret = ConfigurationManager.AppSettings["clientsecret"].ToString();
                string clientid = ConfigurationManager.AppSettings["clientID"].ToString();

                ClientIdSecretData = new RestAuthorizationManager().CreateCredentialsString(clientid, clientsecret);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
                req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + ClientIdSecretData);
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                using (Stream stm = req.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stm))
                    {
                        stmw.Write("grant_type=client_credentials");
                    }
                }
                try
                {
                    using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        result = responseReader.ReadToEnd();
                    }
                    var ser = new JavaScriptSerializer();
                    Dictionary<string, object> dict = ser.Deserialize<Dictionary<string, object>>(result);
                    securityToken = dict["access_token"].ToString();
                }
                catch (WebException wex)
                {
                    var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                    return pageContent;
                }


                try
                {
                    //using (var httpClient = new HttpClient())
                    //{
                    //    httpClient.BaseAddress = new Uri(URL_ADDRESS);
                    //    httpClient.DefaultRequestHeaders.Add("authorization", "Basic " + ClientIdSecretData);
                    //    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    //    httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                    //    httpClient.DefaultRequestHeaders.Add("cache-control", "no-cache");

                    //    var args = new Dictionary<string, string>();
                    //    args.Add("grant_type", "client_credentials");
                    //    var content = new System.Net.Http.FormUrlEncodedContent(args);
                    //    string json = JsonConvert.SerializeObject(requestModel);
                    //    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    //    //var response = httpClient.PostAsync("https://api.test.sabre.com/v2/auth/token", content);
                    //    var response = httpClient.PostAsync("https://api.test.sabre.com/v2/auth/token", content);
                    //}
                    //using (var client = new HttpClient())
                    //{
                    //    var request = new CreateAppRequest()
                    //    {
                    //        userAgent = "myAgent",
                    //        endpointId = "1234",
                    //        culture = "en-US"
                    //    };
                    //    var response = client.PostAsync("https://domain.com/CreateApp",new StringContent(JsonConvert.SerializeObject(request).ToString(),Encoding.UTF8, "application/json")).Result;
                    //    if (response.IsSuccessStatusCode)
                    //    {
                    //        dynamic content = JsonConvert.DeserializeObject(
                    //            response.Content.ReadAsStringAsync()
                    //            .Result);

                    //        // Access variables from the returned JSON object
                    //        var appHref = content.links.applications.href;
                    //    }
                    //}


                    //dynamic client = new RestClient("https://api.test.sabre.com/v2/auth/token");
                    //dynamic request = new RestRequest(Method.POST);
                    //request.AddHeader("postman-token", "7ef7ca31-fd77-a233-b6fc-afa1d6159374");
                    //request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    //request.AddHeader("authorization", "Basic VjE6aG8xMWlwOTNkN29pMXk3YjpERVZDRU5URVI6RVhUOldrUzdyN3BO");
                    //IRestResponse response = client.Execute(request);


                    //using (HttpClient client = new HttpClient())
                    //{
                    //    //client.BaseAddress = new Uri(URL_ADDRESS);
                    //    //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", ClientIdSecretData);
                    //    //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    //    //client.DefaultRequestHeaders.Add("authorization", "Basic " + ClientIdSecretData);
                    //    //client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //    ////client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //    ////client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                    //    //client.DefaultRequestHeaders.Add("cache-control", "no-cache");


                    //    client.DefaultRequestHeaders.Add("cache-control", "no-cache");
                    //    client.DefaultRequestHeaders.Add("content-type", "application/x-www-form-urlencoded");
                    //    client.DefaultRequestHeaders.Add("authorization", "Basic VjE6aG8xMWlwOTNkN29pMXk3YjpERVZDRU5URVI6RVhUOldrUzdyN3BO");

                    //    var args = new Dictionary<string, string>();
                    //    args.Add("grant_type", "client_credentials");
                    //    var content = new System.Net.Http.FormUrlEncodedContent(args);
                    //    var response = client.PostAsync(URL_ADDRESS, new StringContent(JsonConvert.SerializeObject(args).ToString(), Encoding.UTF8, "application/json")).Result;
                    //    if (response.IsSuccessStatusCode)
                    //    {

                    //    }
                    //        //string requestUri = response.RequestMessage.RequestUri.ToString();
                    //        //if (response.IsSuccessStatusCode)
                    //        //{
                    //        //    AuthTokenRS value = await response.Content.ReadAsAsync<AuthTokenRS>();
                    //        //    return HttpResponse<AuthTokenRS>.Success(response.StatusCode, value, requestUri);
                    //        //}
                    //        //else
                    //        //{
                    //        //    return HttpResponse<AuthTokenRS>.Fail(response.StatusCode, await response.Content.ReadAsStringAsync(), requestUri);
                    //        //}
                    //    }
                }
                catch (WebException wex)
                {
                    
                }
            }
            catch (Exception ex)
            {
                // logger.Error(ex);
                System.Console.WriteLine(ex);
            }
            finally
            {
            }
            return securityToken;
        }
        public string AuthorizeTokecAsync(string credentials)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(this.config.Environment);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*"));
                var args = new Dictionary<string, string>();
                args.Add("grant_type", "client_credentials");
                var content = new System.Net.Http.FormUrlEncodedContent(args);
                var response = client.PostAsync("https://api.test.sabre.com/v2/auth/token", content);
                //string requestUri = response.RequestMessage.RequestUri.ToString();
                //if (response.IsSuccessStatusCode)
                //{
                //    AuthTokenRS value = await response.Content.ReadAsAsync<AuthTokenRS>();
                //    return HttpResponse<AuthTokenRS>.Success(response.StatusCode, value, requestUri);
                //}
                //else
                //{
                //    return HttpResponse<AuthTokenRS>.Fail(response.StatusCode, await response.Content.ReadAsStringAsync(), requestUri);
                //}
            }
            return string.Empty;
        }
        public string getAuthToken_Old()
        {
            Dictionary<string, string> AppConfigData = new SACS.Library.Configuration.ConfigDataProvider().AppConfigProperties;
            string result = "";
            try
            {
                String ClientIdSecretData = String.Empty;
                string URL_ADDRESS = "https://api.test.sabre.com/v2/auth/token"; // ConfigurationManager.AppSettings["authtokenurl"].ToString();
                //URL_ADDRESS = "https://api.sabre.com/v2/auth/token"; // ConfigurationManager.AppSettings["authtokenurl"].ToString();
                string clientsecret = ConfigurationManager.AppSettings["clientsecret"].ToString();
                string clientid = ConfigurationManager.AppSettings["clientID"].ToString();

                ClientIdSecretData = new RestAuthorizationManager().CreateCredentialsString(clientid, clientsecret);

                //base64 encode clientId and clientSecret
                var bytes = Encoding.UTF8.GetBytes(clientid);
                var base64clientid = Convert.ToBase64String(bytes);
                bytes = Encoding.UTF8.GetBytes(clientsecret);
                var base64clientsecret = Convert.ToBase64String(bytes);
                //base64clientid = "VjE6aG8xMWlwOTNkN29pMXk3YjpERVZDRU5URVI6RVhU";
                //base64clientsecret = "V2tTN3I3cE4=";
                //Concatenate encoded client and secret strings, separated with colon
                ClientIdSecretData = base64clientid + ":" + base64clientsecret;
                bytes = Encoding.UTF8.GetBytes(ClientIdSecretData);
                ClientIdSecretData = Convert.ToBase64String(bytes);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL_ADDRESS);
                req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + ClientIdSecretData);
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                using (Stream stm = req.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stm))
                    {
                        stmw.Write("grant_type=client_credentials");
                    }
                }
                try
                {
                    using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        result = responseReader.ReadToEnd();
                    }
                    var ser = new JavaScriptSerializer();
                    Dictionary<string, object> dict = ser.Deserialize<Dictionary<string, object>>(result);
                    securityToken = dict["access_token"].ToString();
                }
                catch (WebException wex)
                {
                    var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                    return pageContent;
                }
            }
            catch (Exception ex)
            {
                // logger.Error(ex);
                System.Console.WriteLine(ex);
            }
            finally
            {
            }
            return securityToken;
        }
    }
}
