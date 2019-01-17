namespace App.Base
{
    namespace BaseHttpWebClient
    {
        using System;
        using System.Collections.Generic;
        using System.Web;
        using System.IO;
        using System.Xml.Serialization;
        using System.Web.Caching;
        using System.Net;
        using System.Security.Cryptography.X509Certificates;
        using System.Net.Http;
        using App.Models;
        using System.Threading.Tasks;
        using Newtonsoft.Json;
        using System.Text;
        using System.Net.Http.Headers;
        #region BaseProxy Block

        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL            
        /// Company Name:   RMSI            
        /// Created Date:   Developed on: 07/05/2016      
        /// Summary     :   BaseHttpWebClient Security Services.
        ///*************************************************
        /// </summary>
        public class BaseHttpWebClient 
        {
            public BaseHttpWebClient()
            {

            }

            public string RestBMFRS { get; set; }
            public string RestBMFRQ { get; set; }
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
                    using (var Reqestclient = new WebClient())
                    {
                        Reqestclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        Reqestclient.Headers.Add("Authorization", "Bearer " +ReqResServiseAccess.AppAccessToken);
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
                catch (Exception ex)
                {
                }
                return Utility;
            }
            public async Task<App.Common.CommonUtility> Get_ServiceReqRes(ServiceReqModels ReqResServiseAccess)
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                try
                {
                    dynamic Mydata;
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                        Mydata = await client.GetStringAsync(ReqResServiseAccess.AppServiceURI);
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
            public async Task<App.Common.CommonUtility> Post_ServiceReqRes(ServiceReqModels ReqResServiseAccess)
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                try
                {
                    dynamic Mydata;
                    using (var client = new HttpClient())
                    {
                        HttpContent content = new StringContent("json", Encoding.UTF8, "application/json");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ReqResServiseAccess.AppAccessToken);
                        Mydata = await client.PostAsync(ReqResServiseAccess.AppServiceURI, content);
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
            /// <summary>
             /// Performs a POST call asynchronously.
             /// </summary>
             /// <typeparam name="TRequest">The type of the request model.</typeparam>
             /// <typeparam name="TResponse">The type of the response (response body will be deserialized to this type).</typeparam>
             /// <param name="path">The relative request path (service endpoint)</param>
             /// <param name="requestModel">The request model that will be serialized to JSON and sent in request body.</param>
             /// <returns>The HTTP response containing the deserialized result.</returns>
            public App.Common.CommonUtility GetPostAsync(ServiceReqModels ReqResServiseAccess, dynamic ReqServiseData)
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                try
                {
                    dynamic Mydata;
                    dynamic Mydata1;

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(ReqResServiseAccess.AppServiceURI);
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ReqResServiseAccess.AppAccessToken));
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
                    request.Content = new StringContent(ReqServiseData,Encoding.UTF8,"application/json");//CONTENT-TYPE header
                    Mydata = client.PostAsync(ReqResServiseAccess.AppServiceURI, request.Content);


                    string json = JsonConvert.SerializeObject(ReqServiseData);
                    HttpContent content = new StringContent(ReqServiseData, Encoding.UTF8, "application/json");
                    using (var Http_Client = new HttpClient())
                    {
                        Http_Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReqResServiseAccess.AppAccessToken);
                        Http_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                         Mydata1 = Http_Client.PostAsync(ReqResServiseAccess.AppServiceURI, content);
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
                    var Error = ex;
                }
                return Utility;
            }
            /// <summary>
            /// Performs a POST call asynchronously.
            /// </summary>
            /// <typeparam name="TRequest">The type of the request model.</typeparam>
            /// <typeparam name="TResponse">The type of the response (response body will be deserialized to this type).</typeparam>
            /// <param name="path">The relative request path (service endpoint)</param>
            /// <param name="requestModel">The request model that will be serialized to JSON and sent in request body.</param>
            /// <returns>The HTTP response containing the deserialized result.</returns>
            public App.Common.CommonUtility PostAsync<TRequest, TResponse>(string path, TRequest requestModel, ServiceReqModels ReqResServiseAccess)
            {
                App.Common.CommonUtility Utility = new App.Common.CommonUtility();
                try
                {
                    dynamic Mydata;
                    string json = JsonConvert.SerializeObject(requestModel);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var Http_Client = new HttpClient())
                    {
                        Http_Client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                        Http_Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ReqResServiseAccess.AppAccessToken);
                        Http_Client.DefaultRequestHeaders.Add("Accept", "application/json");
                        Mydata = Http_Client.PostAsync(path, content);
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
            /// <summary>
            /// 
            /// </summary>
            /// <param name="uri"></param>
            /// <returns></returns>
            public string GetServiceToken(string ReqUriForToken)
            {
                try
                {

                    String clientId = "V1:ho11ip93d7oi1y7b:DEVCENTER:EXT";//Put Your Client Id Here
                    String clientSecret = "WkS7r7pN";//Put Your Secret Id Here
                    String encodedClientId =App.Common.AppConvert.EncodeTo64(clientId);
                    String encodedClientSecret = App.Common.AppConvert.EncodeTo64(clientSecret);

                    //Concatenate encoded client and secret strings, separated with colon
                    String encodedClientIdSecret = encodedClientId + ":" + encodedClientSecret;
                    //Convert the encoded concatenated string to a single base64 encoded string.
                    encodedClientIdSecret = App.Common.AppConvert.EncodeTo64(encodedClientIdSecret);
                    ReqUriForToken = "https://api.test.sabre.com/v2/auth/token";
                    //receives : apiEndPoint (https://api.test.sabre.com)
                    // //encodedCliAndSecret : base64Encode(  base64Encode(V1:[user]:[group]:[domain]) + ":" + base64Encode([secret]) )
                    // string OutPutData=string.Empty;
                    // var Mydata="";



                    //string proxyToken = string.Empty;
                    //System.Net.WebRequest tokenRequest = System.Net.WebRequest.Create(apiEndPoint + "?token=" + encodedClientIdSecret);

                    //System.Net.WebResponse tokenResponse = tokenRequest.GetResponse();
                    //System.IO.Stream responseStream = tokenResponse.GetResponseStream();
                    //System.IO.StreamReader readStream = new System.IO.StreamReader(responseStream);
                    //proxyToken = readStream.ReadToEnd();
                    //using (var client = new HttpClient())
                    //{
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", encodedClientIdSecret);
                    //    Mydata = client.GetStringAsync(apiEndPoint);
                    //}


                    //using (var Reqestclient = new WebClient())
                    //{
                    //    Reqestclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    //    Reqestclient.Headers.Add("Authorization", "Bearer " + encodedClientIdSecret);
                    //    Reqestclient.Headers.Add("Accept", "application/json");
                    //    Mydata = Reqestclient.DownloadString(apiEndPoint);
                    //}


           


                }
                catch (InvalidOperationException ivoex)
                {
                }
                catch (Exception e)
                {
                }
                return string.Empty;
            }

            #region RestBMFRQ-RestBMFRS Block

            private void JsonData()
            {
                RestBMFRQ = "";

            }
            #endregion RestBMFRQ-RestBMFRS Block
        }

        #endregion BaseProxy Block
    }
}