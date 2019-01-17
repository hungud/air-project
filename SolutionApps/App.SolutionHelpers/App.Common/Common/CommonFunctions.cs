using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace App.Common
{

    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:10/07/2015
    /// Summary/Description:
    /// </summary>
    public class CommonFunctions
    {
        public CommonFunctions()
        {
        }
        public static FileStream fs = null;
        public static StreamWriter writer = null;
        public static byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            System.IO.FileStream fs = File.OpenRead(fullFilePath);
            try
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return bytes;
            }
            finally
            {
                fs.Close();
            }

        }
        public static void WriteLog(string strLogText)
        {
            //Logger.Write(strLogText, "Rolling Flat File Trace Listener");
            //if (ConfigurationSettings.AppSettings["LOGGING"].ToString() == "true")
            //{
            //    string FolderPath = ConfigurationSettings.AppSettings["DOWNLOAD_TEMP_PHYSICALPATH"] + DateTime.Now.ToString("dd-MM-yyyy");
            //    try
            //    {
            //        if (Directory.Exists(FolderPath) == false)
            //        {
            //            Directory.CreateDirectory(FolderPath);
            //        }
            //        string filePath = FolderPath + "\\ManagementTool_log.txt";
            //        if (fs == null)
            //        {
            //            fs = new FileStream(filePath, FileMode.Append);
            //        }
            //        if (writer == null)
            //        {
            //            writer = new StreamWriter(fs);
            //        }

            //        writer.WriteLine(strLogText);
            //    }
            //    catch (Exception exp)
            //    {

            //    }
            //    finally
            //    {
            //        writer.Close();
            //        fs.Close();
            //        writer = null;
            //        fs = null;
            //    }
            //}

        }
        public static string UniqueFileName(string FileName)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + FileName;
        }
        public string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random(25);
            return random.Next(min, max);
        }
        public List<int> GetRandomNumbers(int count)
        {
            Random random = new Random();
            List<int> randomNumbers = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int number;
                do number = random.Next(10, 100);
                while (randomNumbers.Contains(number));
                randomNumbers.Add(number);
            }
            return randomNumbers;
        }

        public static string GetIP(System.Web.HttpRequestBase request)
        {
            string ip = request.Headers["X-Forwarded-For"]; // AWS compatibility
            if (string.IsNullOrEmpty(ip))
            {
                ip = request.UserHostAddress;
            }
            return ip;
        }
        public static Dictionary<string, string> GetAppSettings()
        {
            List<ConnectionStringSettings> ConnStringList = new List<ConnectionStringSettings>();
            Dictionary<string, string> AppSettingList = new Dictionary<string, string>();
            try
            {
                //AppSettingsReader reader = new AppSettingsReader();
                foreach (ConnectionStringSettings ConnString in System.Configuration.ConfigurationManager.ConnectionStrings)
                {
                    ConnStringList.Add(ConnString);
                    var use = ConnString.Name;
                }
                System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;
                AppSettingList = new Dictionary<string, string>();
                foreach (var item in appSettings.AllKeys)
                {
                    AppSettingList.Add(item, appSettings[item]);
                }

            }
            catch (ConfigurationErrorsException e)
            {
                Console.WriteLine("[DisplayAppSettings: {0}]", e.ToString());
            }
            return AppSettingList;
        }


    }
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    //namespace HttpUtils
    //{
    //    public class RestClientTest
    //    {
    //        public void GetClientTest()
    //        {
    //            string endPoint = @"http:\\myRestService.com\api\";
    //            var client = new RestClient(endPoint);
    //            var json = client.MakeRequest();
    //            ///////////////////////////////////////////
    //            var Respanceclient = new RestClient(endpoint: endPoint, method: HttpVerb.POST, postData: "{'someValueToPost': 'The Value being Posted'}");
    //            ///////////////////////////////////////////
    //            var Rest_Client = new RestClient();
    //            client.EndPoint = @"http:\\myRestService.com\api\"; ;
    //            client.Method = HttpVerb.POST;
    //            client.PostData = "{postData: value}";
    //            var Respance_Json = client.MakeRequest();
    //        }
    //    }

    //    public class RestClient
    //    {
    //        public string EndPoint { get; set; }
    //        public HttpVerb Method { get; set; }
    //        public string ContentType { get; set; }
    //        public string PostData { get; set; }
    //        public RestClient()
    //        {
    //            EndPoint = "";
    //            Method = HttpVerb.GET;
    //            ContentType = "text/xml";
    //            PostData = "";
    //        }
    //        public RestClient(string endpoint)
    //        {
    //            EndPoint = endpoint;
    //            Method = HttpVerb.GET;
    //            ContentType = "text/xml";
    //            PostData = "";
    //        }
    //        public RestClient(string endpoint, HttpVerb method)
    //        {
    //            EndPoint = endpoint;
    //            Method = method;
    //            ContentType = "text/xml";
    //            PostData = "";
    //        }
    //        public RestClient(string endpoint, HttpVerb method, string postData)
    //        {
    //            EndPoint = endpoint;
    //            Method = method;
    //            ContentType = "text/xml";
    //            PostData = postData;
    //        }
    //        public string MakeRequest()
    //        {
    //            return MakeRequest("");
    //        }
    //        public string MakeRequest(string parameters)
    //        {
    //            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
    //            request.Method = Method.ToString();
    //            request.ContentLength = 0;
    //            request.ContentType = ContentType;
    //            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
    //            {
    //                var encoding = new UTF8Encoding();
    //                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
    //                request.ContentLength = bytes.Length;
    //                using (var writeStream = request.GetRequestStream())
    //                {
    //                    writeStream.Write(bytes, 0, bytes.Length);
    //                }
    //            }
    //            using (var response = (HttpWebResponse)request.GetResponse())
    //            {
    //                var responseValue = string.Empty;
    //                if (response.StatusCode != HttpStatusCode.OK)
    //                {
    //                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
    //                    throw new ApplicationException(message);
    //                }
    //                // grab the response
    //                using (var responseStream = response.GetResponseStream())
    //                {
    //                    if (responseStream != null)
    //                        using (var reader = new StreamReader(responseStream))
    //                        {
    //                            responseValue = reader.ReadToEnd();
    //                        }
    //                }
    //                return responseValue;
    //            }
    //        }
    //    }
    //}
}
