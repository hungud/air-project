namespace App.Base
{
    using System;
    using System.Web;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using System.Xml;
    using System.Linq;

    /// <summary>
    /// Summary description for ErrorLog
    /// </summary>
    public sealed class ErrorsLog
    {

        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL                
        /// Company Name:             
        /// Created Date:   Developed on:            
        /// Summary :ErrorsLog
        ///*************************************************
        /// </summary>

        private static volatile ErrorsLog SingletonInstance;
        private static object syncRoot = new Object();
        public string RequestID
        {
            get;  set;
        }
        private ErrorsLog() { }
        public static ErrorsLog ErrorsLogInstance
        {
            get
            {
                if (SingletonInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (SingletonInstance == null)
                            SingletonInstance = new ErrorsLog();
                    }
                }

                return SingletonInstance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Exce"></param>
        public void ManageException(Exception Exce)
        {
            string ErrorMessage = "|| EXMESSAGE ||:- " + Exce.Message + "  || EXSOURCE ||:- " + Exce.Source + " || EXTARGETSITE ||:- " + Exce.TargetSite + "  ||  EXData ||:- " + Exce.Data + Environment.NewLine + Environment.NewLine + Environment.NewLine + "||EXInnerException||:-  " + Environment.NewLine + Exce.InnerException;
            LogMessage(ErrorMessage.ToString());
        }
        public void REQRESException(Exception Exce)
        {
            string ErrorMessage = "|| EXMESSAGE ||:- " + Exce.Message + "  || EXSOURCE ||:- " + Exce.Source + " || EXTARGETSITE ||:- " + Exce.TargetSite + "  ||  EXData ||:- " + Exce.Data + Environment.NewLine + Environment.NewLine + Environment.NewLine + "||EXInnerException||:-  " + Environment.NewLine + Exce.InnerException;
            RequestResponseErrorLog(ErrorMessage.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void RequestLogMessage(string message)
        {
            FileStream fileStream = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                message = message + "   :::   " + DateTime.Now.TimeOfDay.ToString();
                message = message + Environment.NewLine;
                string systemdate = Convert.ToString(DateTime.Now.Date);
                stringBuilder.AppendFormat("{0}{1}", message, Environment.NewLine);
                string errorfilename = DateTime.Now.Date.ToString("dd/MM/yyyy");
                errorfilename = errorfilename.Replace("/", "_");
                string filename = AppDomain.CurrentDomain.BaseDirectory + (@"App_Data\AppErrorLogsFiles\ApplicationErrorLogs\");
                //string filename = HttpContext.Current.Server.MapPath("/App_Data/AppErrorLogsFiles/ApplicationErrorLogFiles/") + "error" + errorfilename;
                CreateValicdateDirectory(filename);
                filename += "RequestLogMessage" + errorfilename;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ResponseLogMessage(string message)
        {
            FileStream fileStream = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                message = message + "   :::   " + DateTime.Now.TimeOfDay.ToString();
                message = message + Environment.NewLine;
                string systemdate = Convert.ToString(DateTime.Now.Date);
                stringBuilder.AppendFormat("{0}{1}", message, Environment.NewLine);
                string errorfilename = DateTime.Now.Date.ToString("dd/MM/yyyy");
                errorfilename = errorfilename.Replace("/", "_");
                string filename = AppDomain.CurrentDomain.BaseDirectory + (@"App_Data\AppErrorLogsFiles\ApplicationErrorLogs\");
                //string filename = HttpContext.Current.Server.MapPath("/App_Data/AppErrorLogsFiles/ApplicationErrorLogFiles/") + "error" + errorfilename;
                CreateValicdateDirectory(filename);
                filename += "ResponseLogMessage" + errorfilename;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void RequestResponseErrorLog(string message)
        {
            FileStream fileStream = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                message = message + "   :::   " + DateTime.Now.TimeOfDay.ToString();
                message = message + Environment.NewLine;
                string systemdate = Convert.ToString(DateTime.Now.Date);
                stringBuilder.AppendFormat("{0}{1}", message, Environment.NewLine);
                string errorfilename = DateTime.Now.Date.ToString("dd/MM/yyyy");
                errorfilename = errorfilename.Replace("/", "_");
                string filename = AppDomain.CurrentDomain.BaseDirectory + (@"App_Data\AppErrorLogsFiles\ApplicationErrorLogs\");
                //string filename = HttpContext.Current.Server.MapPath("/App_Data/AppErrorLogsFiles/ApplicationErrorLogFiles/") + "error" + errorfilename;
                CreateValicdateDirectory(filename);
                filename += "RequestResponseErrorLog" + errorfilename;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void LogMessage(string message)
        {
            FileStream fileStream = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                message = message + "   :::   "+ DateTime.Now.TimeOfDay.ToString();
                message = message + Environment.NewLine;
                string systemdate = Convert.ToString(DateTime.Now.Date);
                stringBuilder.AppendFormat("{0}{1}", message, Environment.NewLine);
                string errorfilename = DateTime.Now.Date.ToString("dd/MM/yyyy");
                errorfilename = errorfilename.Replace("/", "_");
                string filename = AppDomain.CurrentDomain.BaseDirectory + (@"App_Data\AppErrorLogsFiles\ApplicationErrorLogFiles\");
                //string filename = HttpContext.Current.Server.MapPath("/App_Data/AppErrorLogsFiles/ApplicationErrorLogFiles/") + "error" + errorfilename;
                CreateValicdateDirectory(filename);
                filename += "error" + errorfilename;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void LogEventMessage(string message)
        {
            FileStream fileStream = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                message = message + "   :::   " + DateTime.Now.TimeOfDay.ToString();
                message = message + Environment.NewLine;
                string systemdate = Convert.ToString(DateTime.Now.Date);
                stringBuilder.AppendFormat("{0}{1}", message, Environment.NewLine);
                string errorfilename = DateTime.Now.Date.ToString("dd/MM/yyyy");
                errorfilename = errorfilename.Replace("/", "_");
                //string filename = AppDomain.CurrentDomain.BaseDirectory + ("/App_Data/AppErrorLogsFiles/EventLogFiles/") + "EventLog" + errorfilename;
                string filename = AppDomain.CurrentDomain.BaseDirectory + (@"App_Data\AppErrorLogsFiles\ApplicationErrorLogs\") + "error" + errorfilename;
                CreateValicdateDirectory(filename);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void HttpModuleLogEvent(string message)
        {
            FileStream fileStream = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                message = message + "   :::   " + DateTime.Now.TimeOfDay.ToString();
                message = message + Environment.NewLine;
                string systemdate = Convert.ToString(DateTime.Now.Date);
                stringBuilder.AppendFormat("{0}{1}", message, Environment.NewLine);
                string errorfilename = DateTime.Now.Date.ToString("dd/MM/yyyy");
                errorfilename = errorfilename.Replace("/", "_");
                string filename = AppDomain.CurrentDomain.BaseDirectory + ("/App_Data/AppErrorLogsFiles/HttpModuleEventLogFiles/") + "EventLog" + errorfilename;
                CreateValicdateDirectory(filename);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StrUserDetails"></param>
        /// <param name="StrUserMessageDetails"></param>
        public void LogUserEventLog(string StrUserDetails, string StrUserMessageDetails)
        {
            FileStream fileStream = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                StrUserMessageDetails = StrUserMessageDetails + "   :::   " + DateTime.Now.TimeOfDay.ToString();
                StrUserMessageDetails = StrUserMessageDetails + Environment.NewLine;
                string systemdate = Convert.ToString(DateTime.Now.Date);
                stringBuilder.AppendFormat("{0}{1}", StrUserMessageDetails, Environment.NewLine);
                string errorfilename = DateTime.Now.Date.ToString("dd/MM/yyyy");
                errorfilename = errorfilename.Replace("/", "_");
                string filename = AppDomain.CurrentDomain.BaseDirectory + ("/App_Data/AppErrorLogsFiles/UserLogFiles/") + "UserByLogs" + errorfilename;
                CreateValicdateDirectory(filename);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void LogEventTrape(string message)
        {
            lock (this)
            {
                StreamWriter wrtr = new StreamWriter(HttpContext.Current.Server.MapPath("/App_Data/AppErrorLogsFiles/LogEventTrape/AspPageEvents.log"), true);
                wrtr.WriteLine(DateTime.Now.ToString() + " | " + DateTime.Now.Millisecond.ToString() + " | " + message.ToString());
                wrtr.Close();
                wrtr = null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DirectoryPath"></param>
        private void CreateValicdateDirectory(string DirectoryPath)
        {
            string[] pathParts = DirectoryPath.Split('\\');
            IList<string> PathList = pathParts;
            List<String> list = new List<string>(pathParts);
            for (int i = 0; i < pathParts.Length; i++)
            {
                if (i > 0)
                    pathParts[i] = Path.Combine(pathParts[i - 1], pathParts[i]);

                if (!Directory.Exists(pathParts[i]))
                    Directory.CreateDirectory(pathParts[i]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="indent"></param>
        private void ShowAllFoldersUnder(string path, int indent)
        {
            foreach (string folder in Directory.GetDirectories(path))
            {
                Console.WriteLine("{0}{1}", new string(' ', indent), Path.GetFileName(folder));
                ShowAllFoldersUnder(folder, indent + 2);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDir"></param>
        /// <param name="path"></param>
        private void DirSearch(string sDir, string path)
        {
            try
            {
                System.Collections.Generic.List<string> MyData = new System.Collections.Generic.List<string>();
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d, path))
                    {
                        MyData.Add(f);
                    }
                    DirSearch(d, path);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public void SaveFile(string file, string folderName, string fileName)
        {
            folderName = RequestID;
            string path = AppDomain.CurrentDomain.BaseDirectory + (@"App_Data\ApiLogFiles\UserLogFiles\");
            //string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Request_Responses");
            string finalPath = path + @"\" + folderName;
            if (!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(file);

            xdoc.Save(Path.Combine(finalPath, fileName));
        }

        public void RequestLogMessageSOAP(string message, string fileName)
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
                stringBuilder.Append( message);

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

        public void RequestLogMessageREST(string message,string fileNameQuote)
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
                                
                string fileName = finalPath + @"\" + fileNameQuote;
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
        private static Random random = new Random();
        public static string RandomString()
        {
            int length = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}