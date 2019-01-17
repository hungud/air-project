using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace App.Base.TokenManager
{
    public class CORSHandler : DelegatingHandler
    {
        
        const string Origin = "Origin";
        const string AccessControlRequestMethod = "Access-Control-Request-Method";
        const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        public const string CORSHandlerAllowerHostsSettings = "CORSHandlerAllowedHosts";
        private readonly IList<string> allowedOrigins;
        private Dictionary<string, IEnumerable<string>> request_Headers { get; set; }
        public CORSHandler()
        { }
        public CORSHandler(string allowedOrigins)
        {
            if (string.IsNullOrWhiteSpace(allowedOrigins))
            {
                throw new ArgumentNullException("allowedOrigins");
            }
            this.allowedOrigins = new List<string>(allowedOrigins.Split(',').Select(s => s.Trim()));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                bool isCorsRequest = request.Headers.Contains(Origin);
                bool isPreflightRequest = request.Method == HttpMethod.Options;
                bool tstatus = validateTokenFromSTS(request);
                if (isCorsRequest)
                {
                    if (isPreflightRequest)
                    {
                        return Task.Factory.StartNew<HttpResponseMessage>(() =>
                        {
                            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                            response.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());
                            
                            //response.Headers.Add(AccessControlAllowHeaders, "Origin, Content-Type, Accept, Authorization");
                            response.Headers.Add(AccessControlAllowHeaders, "*");
                            response.Headers.Add(AccessControlAllowMethods, "*");
                            response.Headers.Add(AccessControlRequestMethod, "*");
                            response.Headers.Add(AccessControlRequestHeaders, "*");

                            //response.Headers.Add(AccessControlRequestMethod, "GET, POST, PUT, DELETE");
                            //response.Headers.Add(AccessControlAllowHeaders, "Content-Type, Accept, X-Authorization-Token, X-HTTP-Method-Override");
                

                            var tsc = new TaskCompletionSource<HttpResponseMessage>();
                            tsc.SetResult(response);
                            string accessControlRequestMethod = request.Headers.GetValues(AccessControlRequestMethod).FirstOrDefault();
                            if (accessControlRequestMethod != null)
                            {
                                response.Headers.Add(AccessControlAllowMethods, accessControlRequestMethod);
                            }
                            string requestedHeaders = string.Join(", ", request.Headers.GetValues(AccessControlRequestHeaders));
                            if (!string.IsNullOrEmpty(requestedHeaders))
                            {
                                response.Headers.Add(AccessControlAllowHeaders, requestedHeaders);
                            }
                            return response;
                        }, cancellationToken);
                    }
                    else
                    {
                        return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(t =>
                        {
                            HttpResponseMessage resp = t.Result;
                            System.Net.Http.Headers.HttpHeaders headers = request.Headers;
                            resp.Headers.Add(AccessControlAllowOrigin, request.Headers.GetValues(Origin).First());
                            return resp;
                        });
                    }
                }
                else
                {
                    return base.SendAsync(request, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                return base.SendAsync(request, cancellationToken);
            }
        }

        private bool validateTokenFromSTS(HttpRequestMessage request)
        {
            try
            {
                if (request.Headers.Contains("User-Authorization-Token"))
                {
                    request_Headers = new Dictionary<string, IEnumerable<string>>();
                    foreach (var item in request.Headers)
                    {
                        request_Headers.Add(item.Key, item.Value);
                    }
                    var token = request.Headers.GetValues("User-Authorization-Token").First();
                }
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://localhost:11012/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    SecurityToken sToken;
                    if (TryRetrieveToken(request, out sToken))
                    {
                        //FederatedAuthentication.SessionAuthenticationModule.TryReadSessionTokenFromCookie(out s);
                        //HttpResponseMessage response = httpClient.PostAsJsonAsync("api/validatetoken", sToken).Result;
                        //if (response.IsSuccessStatusCode)
                        //{
                        //    //var result = new JavaScriptSerializer().Deserialize<List<string>>(response.Content.ReadAsAsync<string>().Result.ToString());
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        
        private static bool TryRetrieveToken(HttpRequestMessage request, out SecurityToken token)
        {
            try
            {
                token = null;
                IEnumerable<string> authHeaders;
                request.Headers.TryGetValues("Authorization", out authHeaders);
                var authHeadersList = authHeaders.ToList();
                if (authHeadersList.Count() != 1)
                {
                    // Fail if no Authorization header or more than one Authorization headers 
                    // are found in the HTTP request 
                    return false;
                }
                string json_token = authHeadersList.FirstOrDefault();
                //var t = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(json_token,string);
                //var t = JsonConvert.DeserializeObject(json_token);
                //token = (SecurityToken)t;
                return true;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                token = null;
                return false;
            }
        }

    }
}