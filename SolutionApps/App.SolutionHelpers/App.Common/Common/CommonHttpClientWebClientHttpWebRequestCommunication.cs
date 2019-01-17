using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;

namespace App.Common
{
    /// <summary>
    /// The HttpClient WebClient HttpWebRequest.
    /// </summary>
    public class CommonHttpClientWebClientHttpWebRequestCommunication
    {




        public string CreateCredentialsString(string userId, string secret, string domain = "AA")
        {
            string clientId =  string.Format("{0}{1}", userId,  domain);
            return Base64Encode(Base64Encode(clientId) + ":" + Base64Encode(secret));
        }
        public string CreateCredentialsString(string userId, string secret)
        {
            return Base64Encode(Base64Encode(userId) + ":" + Base64Encode(secret));
        }

        /// <summary>
        /// Encodes the string in base64.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>The string in base64.</returns>
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Performs a GET call asynchronously.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response (response body will be deserialized to this type).</typeparam>
        /// <param name="path">The relative request path (service endpoint).</param>
        /// <param name="queryDictionary">The query dictionary (mapping between parameter names and values that will be placed in the query string).</param>
        /// <returns>The HTTP response containing the deserialized result.</returns>
        //public async Task GetAsync<TResponse>(string path, IDictionary<string, string> queryDictionary)
        //{
        //    string queryString = string.Join("&", queryDictionary.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
        //    string requestUri = path + "?" + queryString;
        //    return await this.GetAsync<TResponse>(requestUri);
        //}

        /// <summary>
        /// Performs a POST call asynchronously.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request model.</typeparam>
        /// <typeparam name="TResponse">The type of the response (response body will be deserialized to this type).</typeparam>
        /// <param name="path">The relative request path (service endpoint)</param>
        /// <param name="requestModel">The request model that will be serialized to JSON and sent in request body.</param>
        /// <returns>The HTTP response containing the deserialized result.</returns>
        /// 

        public async Task<CommonUtility> GetActionHttpClient()
        {
            CommonUtility Utility = new CommonUtility();
            var baseAddress = new System.Uri("https://private-a8014-xxxxxx.apiary-mock.com/");
            using (var httpClient = new System.Net.Http.HttpClient { BaseAddress = baseAddress })
            {

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                using (var response = await httpClient.GetAsync("user/list{?organizationId}"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Utility = new CommonUtility() { Data = responseData };
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var sendrequest = new System.Net.Http.HttpRequestMessage()
                {
                    RequestUri = new System.Uri("http://www.someURI.com"),
                    Method = System.Net.Http.HttpMethod.Get,
                };
                sendrequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
                using (var response = await httpClient.SendAsync(sendrequest))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Utility = new CommonUtility() { Data = responseData };
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                System.Net.Http.HttpContent postcontent = new System.Net.Http.StringContent("{\r\n  \"OTA_AirLowFareSearchRQ\": {\r\n    \"OriginDestinationInformation\": [{\r\n      \"RPH\": \"1\",\r\n      \"DepartureDateTime\": \"2016-11-15T00:00:00\",\r\n      \"OriginLocation\": {\r\n        \"LocationCode\": \"LAX\"\r\n      },\r\n      \"DestinationLocation\": {\r\n        \"LocationCode\": \"JFK\"\r\n      }\r\n    }],\r\n    \"TPA_Extensions\": {\r\n      \"IntelliSellTransaction\": {\r\n        \"RequestType\": {\r\n          \"Name\": \"50ITINS\"\r\n        }\r\n      }\r\n    },\r\n    \"TravelerInfoSummary\": {\r\n      \"AirTravelerAvail\": [{\r\n        \"PassengerTypeQuantity\": [{\r\n          \"Code\": \"ADT\",\r\n          \"Quantity\": 1\r\n        }]\r\n      }]\r\n    },\r\n    \"TravelPreferences\": {\r\n      \"TPA_Extensions\": {\r\n      }\r\n    },\r\n    \"POS\": {\r\n      \"Source\": [{\r\n        \"RequestorID\": {\r\n          \"Type\": \"0.AAA.X\",\r\n          \"ID\": \"REQ.ID\",\r\n          \"CompanyName\": {\r\n            \"Code\": \"TN\"\r\n          }\r\n        },\r\n        \"PseudoCityCode\": \"6DTH\"\r\n      }]\r\n    }\r\n  }\r\n}\r\n", System.Text.Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://www.someURI.com", postcontent))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Utility = new CommonUtility() { Data = responseData };
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                System.Net.Http.HttpContent deletecontent = new System.Net.Http.StringContent("{\r\n  \"OTA_AirLowFareSearchRQ\": {\r\n    \"OriginDestinationInformation\": [{\r\n      \"RPH\": \"1\",\r\n      \"DepartureDateTime\": \"2016-11-15T00:00:00\",\r\n      \"OriginLocation\": {\r\n        \"LocationCode\": \"LAX\"\r\n      },\r\n      \"DestinationLocation\": {\r\n        \"LocationCode\": \"JFK\"\r\n      }\r\n    }],\r\n    \"TPA_Extensions\": {\r\n      \"IntelliSellTransaction\": {\r\n        \"RequestType\": {\r\n          \"Name\": \"50ITINS\"\r\n        }\r\n      }\r\n    },\r\n    \"TravelerInfoSummary\": {\r\n      \"AirTravelerAvail\": [{\r\n        \"PassengerTypeQuantity\": [{\r\n          \"Code\": \"ADT\",\r\n          \"Quantity\": 1\r\n        }]\r\n      }]\r\n    },\r\n    \"TravelPreferences\": {\r\n      \"TPA_Extensions\": {\r\n      }\r\n    },\r\n    \"POS\": {\r\n      \"Source\": [{\r\n        \"RequestorID\": {\r\n          \"Type\": \"0.AAA.X\",\r\n          \"ID\": \"REQ.ID\",\r\n          \"CompanyName\": {\r\n            \"Code\": \"TN\"\r\n          }\r\n        },\r\n        \"PseudoCityCode\": \"6DTH\"\r\n      }]\r\n    }\r\n  }\r\n}\r\n", System.Text.Encoding.UTF8, "application/json");
                using (var response = await httpClient.DeleteAsync("http://www.someURI.com"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Utility = new CommonUtility() { Data = responseData };
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                System.Net.Http.HttpContent putcontent = new System.Net.Http.StringContent("{\r\n  \"OTA_AirLowFareSearchRQ\": {\r\n    \"OriginDestinationInformation\": [{\r\n      \"RPH\": \"1\",\r\n      \"DepartureDateTime\": \"2016-11-15T00:00:00\",\r\n      \"OriginLocation\": {\r\n        \"LocationCode\": \"LAX\"\r\n      },\r\n      \"DestinationLocation\": {\r\n        \"LocationCode\": \"JFK\"\r\n      }\r\n    }],\r\n    \"TPA_Extensions\": {\r\n      \"IntelliSellTransaction\": {\r\n        \"RequestType\": {\r\n          \"Name\": \"50ITINS\"\r\n        }\r\n      }\r\n    },\r\n    \"TravelerInfoSummary\": {\r\n      \"AirTravelerAvail\": [{\r\n        \"PassengerTypeQuantity\": [{\r\n          \"Code\": \"ADT\",\r\n          \"Quantity\": 1\r\n        }]\r\n      }]\r\n    },\r\n    \"TravelPreferences\": {\r\n      \"TPA_Extensions\": {\r\n      }\r\n    },\r\n    \"POS\": {\r\n      \"Source\": [{\r\n        \"RequestorID\": {\r\n          \"Type\": \"0.AAA.X\",\r\n          \"ID\": \"REQ.ID\",\r\n          \"CompanyName\": {\r\n            \"Code\": \"TN\"\r\n          }\r\n        },\r\n        \"PseudoCityCode\": \"6DTH\"\r\n      }]\r\n    }\r\n  }\r\n}\r\n", System.Text.Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://www.someURI.com", putcontent))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Utility = new CommonUtility() { Data = responseData };
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
           
            return Utility;
        }

        public void RequestLogMessageSOAP(string message, string fileName,string RequestID)
        {
            FileStream fileStream = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                //message = message + "   :::   " + DateTime.Now.TimeOfDay.ToString();
                //message = message + Environment.NewLine;

                string path = AppDomain.CurrentDomain.BaseDirectory + (@"App_Data\ApiLogFiles\UserLogFiles\");

                string finalPath = path + @"\" + RequestID;
                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }
                string systemdate = Convert.ToString(DateTime.Now.Date);
                stringBuilder.Append(message);

                fileName = finalPath + @"\" + fileName;
                if ((File.Exists(fileName + ".log")))
                {
                    fileStream = File.Open(fileName + ".log", FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fileStream = File.Create(fileName + ".log");
                }
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(stringBuilder.ToString());
                streamWriter.Close();
                streamWriter = null;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if ((fileStream != null))
                {
                    fileStream.Close();
                }
                fileStream = null;
                stringBuilder = null;
            }
        }
        public CommonUtility GetTokenHttpWebRequest(string ClientID,string ClientSecret,string RequestID)
        {
            CommonUtility Utility = new CommonUtility();
            try
            {                
                String securityToken = string.Empty, expires_in = string.Empty, result = string.Empty,  URL_ADDRESS = System.Configuration.ConfigurationManager.AppSettings["SareAuthTokenURL"].ToString();
                //String securityToken = string.Empty, expires_in = string.Empty, result = string.Empty,
                 // URL_ADDRESS = "https://api.live.sabre.com/v2/auth/token";
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (string.IsNullOrEmpty(TokenDataManager()))
                {
                    var cache = System.Runtime.Caching.MemoryCache.Default;
                    if (cache.Get("dataCache") == null)
                    {
                        //string clientsecret = ConfigurationManager.AppSettings["clientsecret"].ToString();
                        //string clientid = ConfigurationManager.AppSettings["clientID"].ToString();

                        string clientsecret = ClientSecret;
                        string clientid = ClientID;

                        String ClientIdSecretData = CreateCredentialsString(clientid, clientsecret);
                        System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL_ADDRESS);
                        req.Headers.Add(System.Net.HttpRequestHeader.Authorization, "Basic " + ClientIdSecretData);
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.Method = "POST";
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        RequestLogMessageSOAP(JsonConvert.SerializeObject(req, Newtonsoft.Json.Formatting.Indented), "GetToken" + "RQ",RequestID);
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
                            dictionary = ser.Deserialize<Dictionary<string, object>>(result);
                            securityToken = dictionary["access_token"].ToString();
                            expires_in = dictionary["expires_in"].ToString();
                            var cachePolicty = new System.Runtime.Caching.CacheItemPolicy();
                            cachePolicty.AbsoluteExpiration = DateTime.Now.AddSeconds(Convert.ToDouble(expires_in) - 100);
                            cache.Add("Auth_Access_Token_Cache", securityToken, cachePolicty);
                            Utility = new CommonUtility()
                            {
                                Data = securityToken,
                                ActionType = "GetTokenHttpWebRequest From Request",
                                Message = "HttpClient SendAsync excuted successfully.",
                                Status = true
                            };
                            RequestLogMessageSOAP(securityToken, "GetToken" + "RS", RequestID);
                        }
                        catch (Exception wex)
                        {
                           // var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                           
                            Utility = new CommonUtility()
                            {
                                Data = wex,
                                ActionType = "GetTokenHttpWebRequest From Cache",
                                Message = "HttpClient SendAsync excution Failed.",
                                Status = false
                            };
                            RequestLogMessageSOAP(wex.ToString(), "GetToken" + "RS", RequestID);
                            // Base.ErrorsLog.ErrorsLogInstance.RequestLogMessageSOAP(pageContent, "GetToken-" + "RS");
                        }
                        TokenDataManager(securityToken, expires_in);
                    }
                    else
                    {
                        Utility = new CommonUtility()
                        {
                            Data = cache.Get("Auth_Access_Token_Cache"),
                            ActionType = "GetTokenHttpWebRequest From Cache",
                            Message = "HttpClient SendAsync excuted successfully.",
                            Status = true
                        };
                        RequestLogMessageSOAP(cache.Get("Auth_Access_Token_Cache").ToString(), "GetToken" + "RS", RequestID);
                    }
                }
                else
                {
                    Utility = new CommonUtility()
                    {
                        Data = TokenDataManager(),
                        ActionType = "GetTokenHttpWebRequest From Cache",
                        Message = "HttpClient SendAsync excuted successfully.",
                        Status = true
                    };
                    RequestLogMessageSOAP(TokenDataManager(), "GetToken" + "RS", RequestID);
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                Utility = new CommonUtility()
                {
                    Data = ex.StackTrace,
                    ActionType = "GetTokenHttpWebRequest From Cache",
                    Message = "HttpClient SendAsync excution Failed.",
                    Status = false
                };
            }
            return Utility;
        }
        public string TokenDataManager(string Common_Utility = "", string Common_expiresin = "")
        {
            string Utility = string.Empty;
            FileStream fileStream = null;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            try
            {
                string TokenName = string.Empty;
                string filename = AppDomain.CurrentDomain.BaseDirectory + ("/App_Data/AppErrorLogsFiles/TokenManagerMessage/TokenMessage");
                if (!string.IsNullOrEmpty(Common_Utility))
                {
                    
                    stringBuilder.AppendFormat("{0}{1}", DateTime.Now.AddSeconds(Convert.ToDouble(Common_expiresin) - 100), Environment.NewLine);
                    stringBuilder.AppendFormat("{0}{1}", Common_Utility, Environment.NewLine);
                    if ((File.Exists(filename + ".log")))
                    {
                        fileStream = File.Open(filename + ".log", FileMode.Append, FileAccess.Write);
                    }
                    else
                    {
                        fileStream = File.Create(filename + ".log");
                    }
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    streamWriter.Write(stringBuilder.ToString());
                    streamWriter.Close();
                    streamWriter = null;
                }
                else
                {
                    if ((File.Exists(filename + ".log")))
                    {
                        string[] lines = System.IO.File.ReadAllLines(filename + ".log");
                        if ((Convert.ToDateTime(lines[0]) > DateTime.Now))
                        {
                            Utility = lines[1];
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if ((fileStream != null))
                {
                    fileStream.Close();
                }
                fileStream = null;
                stringBuilder = null;
            }
            return Utility;
        }
        public CommonUtility GetTokenHttpClient(string strUrl, string requestContent)
        {
            CommonUtility Utility = new CommonUtility();
            try
            {
                String securityToken = string.Empty, result = string.Empty,  URL_ADDRESS = "https://api.test.sabre.com/v2/auth/token";
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var cache = System.Runtime.Caching.MemoryCache.Default;
                if (cache.Get("dataCache") == null)
                {
                    string clientsecret = ConfigurationManager.AppSettings["clientsecret"].ToString();
                    string clientid = ConfigurationManager.AppSettings["clientID"].ToString();
                    String ClientIdSecretData = CreateCredentialsString(clientid, clientsecret,"AA");
                    //using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
                    //{
                        

                    //    //httpClient.BaseAddress = new Uri("https://api.test.sabre.com/v2/auth/token");
                    //    //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", ClientIdSecretData);
                    //    ////client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    //    //httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    //    httpClient.DefaultRequestHeaders.Add("Cache-control", "no-cache");
                    //    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    //    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + ClientIdSecretData);


                    //    var args = new Dictionary<string, string>();
                    //    args.Add("grant_type", "client_credentials");
                    //    var content = new System.Net.Http.FormUrlEncodedContent(args);
                    //    string json = new JavaScriptSerializer().Serialize(content);
                    //    System.Net.Http.HttpContent contents = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    //    //jsonData = new JavaScriptSerializer().Serialize(Utility);
                    //    //string json = new JavaScriptSerializer().Serialize("{\"OTA_AirLowFareSearchRQ\":{\"OriginDestinationInformation\":[{\"RPH\":\"1\",\"DepartureDateTime\":\"2016-11-15T00:00:00\",\"OriginLocation\":{\"LocationCode\":\"LAX\"},\"DestinationLocation\":{\"LocationCode\":\"JFK\"}}],\"TPA_Extensions\":{\"IntelliSellTransaction\":{\"RequestType\":{\"Name\":\"50ITINS\"}}},\"TravelerInfoSummary\":{\"AirTravelerAvail\":[{\"PassengerTypeQuantity\":[{\"Code\":\"ADT\",\"Quantity\":1}]}]},\"TravelPreferences\":{\"TPA_Extensions\":{}},\"POS\":{\"Source\":[{\"RequestorID\":{\"Type\":\"0.AAA.X\",\"ID\":\"REQ.ID\",\"CompanyName\":{\"Code\":\"TN\"}},\"PseudoCityCode\":\"6DTH\"}]}}}");
                    //    //string json = Newtonsoft.Json.JsonConvert.SerializeObject("{\"OTA_AirLowFareSearchRQ\":{\"OriginDestinationInformation\":[{\"RPH\":\"1\",\"DepartureDateTime\":\"2016-11-15T00:00:00\",\"OriginLocation\":{\"LocationCode\":\"LAX\"},\"DestinationLocation\":{\"LocationCode\":\"JFK\"}}],\"TPA_Extensions\":{\"IntelliSellTransaction\":{\"RequestType\":{\"Name\":\"50ITINS\"}}},\"TravelerInfoSummary\":{\"AirTravelerAvail\":[{\"PassengerTypeQuantity\":[{\"Code\":\"ADT\",\"Quantity\":1}]}]},\"TravelPreferences\":{\"TPA_Extensions\":{}},\"POS\":{\"Source\":[{\"RequestorID\":{\"Type\":\"0.AAA.X\",\"ID\":\"REQ.ID\",\"CompanyName\":{\"Code\":\"TN\"}},\"PseudoCityCode\":\"6DTH\"}]}}}");
                    //    var response = await httpClient.PostAsync("https://api.test.sabre.com/v2/auth/token", contents);
                    //    string requestUri = response.RequestMessage.RequestUri.ToString();
                    //    if (response.IsSuccessStatusCode)
                    //    {
                    //        //AuthTokenRS value = await response.Content.ReadAsAsync<AuthTokenRS>();
                    //        //return HttpResponse<AuthTokenRS>.Success(response.StatusCode, value, requestUri);
                    //    }
                    //    else
                    //    {
                    //        //return HttpResponse<AuthTokenRS>.Fail(response.StatusCode, await response.Content.ReadAsStringAsync(), requestUri);
                    //    }
                    //}


                    //var baseAddress = new System.Uri("https://private-a8014-xxxxxx.apiary-mock.com/");
                    //using (var httpClient = new System.Net.Http.HttpClient { BaseAddress = baseAddress })
                    //{
                    //    string clientsecret = ConfigurationManager.AppSettings["clientsecret"].ToString();
                    //    string clientid = ConfigurationManager.AppSettings["clientID"].ToString();
                    //    String ClientIdSecretData = CreateCredentialsString(clientid, clientsecret);
                    //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //    var sendrequest = new System.Net.Http.HttpRequestMessage()
                    //    {
                    //        RequestUri = new System.Uri("http://www.someURI.com"),
                    //        Method = System.Net.Http.HttpMethod.Get,
                    //    };
                    //    sendrequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
                    //    using (var response = await httpClient.SendAsync(sendrequest))
                    //    {
                    //        string responseData = await response.Content.ReadAsStringAsync();
                    //        Utility = new CommonUtility()
                    //        {
                    //            Data = responseData,
                    //            ActionType = "SendAsync",
                    //            Message = "HttpClient SendAsync excuted successfully.",
                    //            Status = true
                    //        };
                    //    }
                    //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //}
                    var cachePolicty = new System.Runtime.Caching.CacheItemPolicy();
                    cachePolicty.AbsoluteExpiration = DateTime.Now.AddSeconds(60);
                    cache.Add("dataCache", Utility, cachePolicty);
                }
                else
                {
                    Utility = new CommonUtility()
                    {
                        Data = cache.Get("dataCache"),
                        ActionType = "SendAsync",
                        Message = "HttpClient SendAsync excuted successfully.",
                        Status = true
                    };
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
            }
            return Utility;
        }
        public CommonUtility GetAsync(string strUrl, string requestContent)
        {
            CommonUtility Utility = new CommonUtility();
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage()
                {
                    RequestUri = new System.Uri("http://www.someURI.com"),
                    Method = System.Net.Http.HttpMethod.Get,
                };
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
                var task = httpClient.SendAsync(request)
                    .ContinueWith((taskwithmsg) =>
                    {
                        var response = taskwithmsg.Result;
                        var jsonTask = response.Content.ReadAsStringAsync();
                        jsonTask.Wait();
                        var jsonObject = jsonTask.Result;
                        Utility = new CommonUtility()
                        {
                            Data = jsonTask.Result,
                            ActionType = "Get",
                            Message = "HttpClient getAsync excuted successfully.",
                            Status = true
                        };
                    });
                task.Wait();
            }
            return Utility;
        }
        public CommonUtility PostAsync(string strUrl, string requestContent,string RequestId,string ClientID,string ClientSecret)
        {
            CommonUtility Utility = new CommonUtility();
            dynamic RequestRespanceData=null;
            try
            {
                if (System.Web.HttpContext.Current.Application["dataCachekey"] == null)
                {
                    Utility = GetTokenHttpWebRequest(ClientID,ClientSecret,RequestId);

                    if (Utility.Status == false)
                    {
                        return Utility;
                    }
                }
                else
                {
                    Utility = (CommonUtility)System.Web.HttpContext.Current.Application["dataCachekey"];
                }
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("Cache-control", "no-cache");
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    //httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Utility.Data);
                    System.Net.Http.HttpContent content = new System.Net.Http.StringContent(requestContent, System.Text.Encoding.UTF8, "application/json");
                    //dynamic text = httpClient.PostAsync(strUrl, content);
                    RequestRespanceData = httpClient.PostAsync(strUrl, content).Result;
                    if (RequestRespanceData.IsSuccessStatusCode)
                    {
                        //dynamic RespanceResult = RequestRespanceData.Content.ReadAsStringAsync().Result;
                        dynamic RespanceResult = RequestRespanceData.Content.ReadAsStringAsync();
                        Utility = new CommonUtility()
                        {
                            //Data = RequestRespanceData.Content.ReadAsStringAsync().Result,
                            Data = RespanceResult,
                            Data1 = RequestId,
                            ActionType = "Post",
                            Message = "HttpClient postAsync excuted successfully.",
                            Status = true
                        };
                    }
                    else
                    {
                        Utility = new CommonUtility()
                        {
                            //Data = RequestRespanceData.Content.ReadAsStringAsync().Result,
                            Data = RequestRespanceData,
                            Data1 = RequestId,
                            ActionType = "Post",
                            Message = "HttpClient postAsync excution Returned False Status Code",
                            Status = true
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Utility = new CommonUtility()
                {
                    //Data = RequestRespanceData.Content.ReadAsStringAsync().Result,
                    Data = RequestRespanceData,
                    Data1 = RequestId,
                    ActionType = "Post",
                    Message = "HttpClient postAsync excution Failed",
                    Status = true
                };
            }
            return Utility;
        }
        public CommonUtility PutAsync(string strUrl, string requestContent)
        {
            CommonUtility Utility = new CommonUtility();
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                string strURLPath = "https://api.test.sabre.com/v1.9.0/shop/flights?mode=live";
                string strToken = "T1RLAQJggXKZnI3caV0YB74HpWy9QW+GrRB3Z8CS/IMkiDbMtxMIArJMAADAj1sF4JJQQaqsCWnL3twyOl/ZP4+2ljIuxDu01Hz+7yHD4YzdSMpQF37p+KbKPfI9LWc3FGlM0gs0U66X1dOUQlIvg9hNw86yV8fN7PJ+fg08ZXUWi7psSWY1FrZJuppLt0Mf5wjIZ/uAkEOip8IezjVcSI8NYcnl+2Xqr5eCb01VSbhoXYYFgXXY771PYUCDuBAmcFcmi00KWBEJZH/B6kCmjZgl1evQzpFF/Cb2kB09BoMbzpnL6mmrqrFRAcEs";
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Cache-control", "no-cache");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + strToken);
                System.Net.Http.HttpContent content = new System.Net.Http.StringContent("{\r\n  \"OTA_AirLowFareSearchRQ\": {\r\n    \"OriginDestinationInformation\": [{\r\n      \"RPH\": \"1\",\r\n      \"DepartureDateTime\": \"2016-11-15T00:00:00\",\r\n      \"OriginLocation\": {\r\n        \"LocationCode\": \"LAX\"\r\n      },\r\n      \"DestinationLocation\": {\r\n        \"LocationCode\": \"JFK\"\r\n      }\r\n    }],\r\n    \"TPA_Extensions\": {\r\n      \"IntelliSellTransaction\": {\r\n        \"RequestType\": {\r\n          \"Name\": \"50ITINS\"\r\n        }\r\n      }\r\n    },\r\n    \"TravelerInfoSummary\": {\r\n      \"AirTravelerAvail\": [{\r\n        \"PassengerTypeQuantity\": [{\r\n          \"Code\": \"ADT\",\r\n          \"Quantity\": 1\r\n        }]\r\n      }]\r\n    },\r\n    \"TravelPreferences\": {\r\n      \"TPA_Extensions\": {\r\n      }\r\n    },\r\n    \"POS\": {\r\n      \"Source\": [{\r\n        \"RequestorID\": {\r\n          \"Type\": \"0.AAA.X\",\r\n          \"ID\": \"REQ.ID\",\r\n          \"CompanyName\": {\r\n            \"Code\": \"TN\"\r\n          }\r\n        },\r\n        \"PseudoCityCode\": \"6DTH\"\r\n      }]\r\n    }\r\n  }\r\n}\r\n", System.Text.Encoding.UTF8, "application/json");

                dynamic RequestRespanceData = httpClient.PostAsync(strURLPath, content).Result;
                if (RequestRespanceData.IsSuccessStatusCode)
                {
                    dynamic RespanceResult = RequestRespanceData.Content.ReadAsStringAsync().Result;
                    Utility = new CommonUtility()
                    {
                        Data = RequestRespanceData.Content.ReadAsStringAsync().Result,
                        ActionType = "Put",
                        Message = "HttpClient putAsync excuted successfully.",
                        Status = true
                    };
                }
            }
            return Utility;
        }
        public CommonUtility DeleteAsync(string strUrl, string requestContent)
        {
            CommonUtility Utility = new CommonUtility();
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                string strURLPath = "https://api.test.sabre.com/v1.9.0/shop/flights?mode=live";
                string strToken = "T1RLAQJggXKZnI3caV0YB74HpWy9QW+GrRB3Z8CS/IMkiDbMtxMIArJMAADAj1sF4JJQQaqsCWnL3twyOl/ZP4+2ljIuxDu01Hz+7yHD4YzdSMpQF37p+KbKPfI9LWc3FGlM0gs0U66X1dOUQlIvg9hNw86yV8fN7PJ+fg08ZXUWi7psSWY1FrZJuppLt0Mf5wjIZ/uAkEOip8IezjVcSI8NYcnl+2Xqr5eCb01VSbhoXYYFgXXY771PYUCDuBAmcFcmi00KWBEJZH/B6kCmjZgl1evQzpFF/Cb2kB09BoMbzpnL6mmrqrFRAcEs";
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Cache-control", "no-cache");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + strToken);
                //string json = Newtonsoft.Json.JsonConvert.SerializeObject("{\"OTA_AirLowFareSearchRQ\":{\"OriginDestinationInformation\":[{\"RPH\":\"1\",\"DepartureDateTime\":\"2016-11-15T00:00:00\",\"OriginLocation\":{\"LocationCode\":\"LAX\"},\"DestinationLocation\":{\"LocationCode\":\"JFK\"}}],\"TPA_Extensions\":{\"IntelliSellTransaction\":{\"RequestType\":{\"Name\":\"50ITINS\"}}},\"TravelerInfoSummary\":{\"AirTravelerAvail\":[{\"PassengerTypeQuantity\":[{\"Code\":\"ADT\",\"Quantity\":1}]}]},\"TravelPreferences\":{\"TPA_Extensions\":{}},\"POS\":{\"Source\":[{\"RequestorID\":{\"Type\":\"0.AAA.X\",\"ID\":\"REQ.ID\",\"CompanyName\":{\"Code\":\"TN\"}},\"PseudoCityCode\":\"6DTH\"}]}}}");
                //System.Net.Http.HttpContent content = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");
                System.Net.Http.HttpContent content = new System.Net.Http.StringContent("{\r\n  \"OTA_AirLowFareSearchRQ\": {\r\n    \"OriginDestinationInformation\": [{\r\n      \"RPH\": \"1\",\r\n      \"DepartureDateTime\": \"2016-11-15T00:00:00\",\r\n      \"OriginLocation\": {\r\n        \"LocationCode\": \"LAX\"\r\n      },\r\n      \"DestinationLocation\": {\r\n        \"LocationCode\": \"JFK\"\r\n      }\r\n    }],\r\n    \"TPA_Extensions\": {\r\n      \"IntelliSellTransaction\": {\r\n        \"RequestType\": {\r\n          \"Name\": \"50ITINS\"\r\n        }\r\n      }\r\n    },\r\n    \"TravelerInfoSummary\": {\r\n      \"AirTravelerAvail\": [{\r\n        \"PassengerTypeQuantity\": [{\r\n          \"Code\": \"ADT\",\r\n          \"Quantity\": 1\r\n        }]\r\n      }]\r\n    },\r\n    \"TravelPreferences\": {\r\n      \"TPA_Extensions\": {\r\n      }\r\n    },\r\n    \"POS\": {\r\n      \"Source\": [{\r\n        \"RequestorID\": {\r\n          \"Type\": \"0.AAA.X\",\r\n          \"ID\": \"REQ.ID\",\r\n          \"CompanyName\": {\r\n            \"Code\": \"TN\"\r\n          }\r\n        },\r\n        \"PseudoCityCode\": \"6DTH\"\r\n      }]\r\n    }\r\n  }\r\n}\r\n", System.Text.Encoding.UTF8, "application/json");

                dynamic RequestRespanceData = httpClient.DeleteAsync(strURLPath).Result;
                if (RequestRespanceData.IsSuccessStatusCode)
                {
                    dynamic RespanceResult = RequestRespanceData.Content.ReadAsStringAsync().Result;
                    Utility = new CommonUtility()
                    {
                        Data = RequestRespanceData.Content.ReadAsStringAsync().Result,
                        ActionType = "Delete",
                        Message = "HttpClient deleteAsync excuted successfully.",
                        Status = true
                    };
                }
            }            
            return Utility;
        }

        /*
        /// <summary>
        /// Performs a HTTP call with authentication with specified number of retry attempts.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="method">The callback used to perform request.</param>
        /// <param name="retryCount">The retry count.</param>
        /// <param name="forceRefresh">If set to <c>true</c>, then acquire a new authentication token.</param>
        /// <returns>The HTTP response.</returns>
        private async  Task<CommonUtility> CallAuthRetry(Func<HttpClient, Task<HttpResponseMessage>> method,int retryCount = 1, bool forceRefresh = false)
        {
            //TokenHolder tokenHolder = await this.restAuthorizationManager.GetAuthorizationTokenAsync(forceRefresh);
            TokenHolder tokenHolder = await this.restAuthorizationManager.GetValidateAuthorizationTokenAsync(forceRefresh);
            
            if (tokenHolder.IsValid)
            {
                var response = await this.Call<TResponse>(method, tokenHolder.Token);
                if (response.StatusCode == HttpStatusCode.Unauthorized && retryCount > 0)
                {
                    return await this.CallAuthRetry<TResponse>(method, retryCount - 1, true);
                }

                return response;
            }
            else
            {
                return HttpResponse<TResponse>.Fail(tokenHolder.ErrorStatusCode, tokenHolder.ErrorMessage);
            }
        }

        /// <summary>
        /// Performs a HTTP call using the specified callback and deserializes the response.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="method">The callback used to perform request.</param>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <returns>The HTTP response.</returns>
        private async Task<CommonUtility> Call<TResponse>(Func<HttpClient, Task<HttpResponseMessage>> method, string authorizationToken = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.config.Environment);
                if (authorizationToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
                }

                var response = await method(client);
                string requestUri = response.RequestMessage.RequestUri.ToString();
                if (response.IsSuccessStatusCode)
                {
                    TResponse value = await response.Content.ReadAsAsync<TResponse>();
                    return HttpResponse<TResponse>.Success(response.StatusCode, value, requestUri);
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return HttpResponse<TResponse>.Fail(response.StatusCode, message, requestUri);
                }
            }
        }

        */
    }
}