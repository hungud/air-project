namespace App.Base
{
    namespace Page
    {
        using System;
        using System.Web.UI;
        using System.Collections;
        using System.Web;
        using System.ComponentModel;
        using System.Web.Caching;
        /// <summary>
        ///*************************************************
        /// Developed By:   RAKESH PAL                 
        /// Company Name:      
        /// Created Date:   Developed on: 
        /// Summary :BasePage
        ///*************************************************
        /// </summary>
        public class BasePage : Page
        {
           
            #region Comman Usese in Page

            public static void ManageException(Exception Exce)
            {
                string ErrorMessage = "EXMESSAGE:-   " + Exce.Message + "  |    EXSOURCE:-    " + Exce.Source + "      |       EXTARGETSITE:-   " + Exce.TargetSite + "      |      EXData:-   " + Exce.Data + "      |     EXInnerException:-  " + Exce.InnerException;
                LogMessage(ErrorMessage.ToString());
            }

            public void ManageException(string ErrorMessage)
            {
                LogMessage(ErrorMessage.ToString());
            }
            public static void LogMessage(string message)
            {
                ErrorsLog.ErrorsLogInstance.LogMessage(message);
            }
            public static void EventLogMessage(string message)
            {
                ErrorsLog.ErrorsLogInstance.LogEventMessage(message);
            }

            public static void EventLog(string StrUserDetails, string StrUserMessageDetails)
            {
                ErrorsLog.ErrorsLogInstance.LogUserEventLog(StrUserDetails, StrUserMessageDetails);
            }
            private void EventTrape(string message)
            {
                ErrorsLog.ErrorsLogInstance.LogEventTrape(message);
            }
           
            #region Comman Usese BrowserCompatibility in Page
            public static class BrowserCompatibility
            {
                #region IsUplevel Browser property
                private enum UpLevel { chrome, firefox, safari }
                public static bool IsUplevel
                {
                    get
                    {
                        bool ret = false;
                        string strbrowser;
                        try
                        {
                            if (HttpContext.Current == null) return ret;
                            strbrowser = HttpContext.Current.Request.UserAgent.ToLower();
                            foreach (UpLevel br in Enum.GetValues(typeof(UpLevel)))
                            { if (strbrowser.Contains(br.ToString())) { ret = true; break; } }
                            return ret;
                        }
                        catch { return ret; }
                    }
                }
                #endregion
            }
            #endregion Comman Usese BrowserCompatibility in Page

            #endregion Comman Usese in Page

            #region Design
            private bool bOverLinkHand = false;
            protected string CurrentCulture
            {
                get
                {
                    if (null != Session["PreferedCulture"])
                    {
                        return Session["PreferedCulture"].ToString();
                    }
                    //return String.Empty;
                    return "English".ToString();
                }
            }
            [Category("Design"), Description("OverLink Color")]
            private string sBackgroundColor = "#dddddd";
            public string SpecialLinkColor
            {
                get { return (sBackgroundColor); }
                set { sBackgroundColor = value; }
            }
            [Category("Design"), Description("OverLink Hand Type")]
            public bool OverLinkHand
            {
                get { return (bOverLinkHand); }
                set { bOverLinkHand = value; }
            }
            #endregion Design

            #region Page Event

            #region PageLofe Cycle

            protected override void OnInit(System.EventArgs e)
            {
                if (BrowserCompatibility.IsUplevel)
                {
                    this.ClientTarget = "uplevel";
                }
                base.OnInit(e);
            }
            protected override void OnInitComplete(System.EventArgs e)
            {
                base.OnInitComplete(e);
            }

            protected override void OnPreLoad(System.EventArgs e)
            {
                base.OnPreLoad(e);
            }
            protected override void OnLoad(System.EventArgs e)
            {
                base.OnLoad(e);
            }
            protected override void OnLoadComplete(System.EventArgs e)
            {
                base.OnLoadComplete(e);
            }
            protected override void OnPreRender(System.EventArgs e)
            {
                base.OnPreRender(e);
            }
            protected override void OnPreRenderComplete(System.EventArgs e)
            {
                base.OnPreRenderComplete(e);
            }
            protected override void OnSaveStateComplete(System.EventArgs e)
            {
                base.OnSaveStateComplete(e);
            }
            protected override void Render(HtmlTextWriter WriteOutPut)
            {
                base.Render(WriteOutPut);
            }
            protected override void OnUnload(System.EventArgs e)
            {
                base.OnUnload(e);
            }
            public override void Dispose()
            {
                base.Dispose();
            }

            protected override void OnError(System.EventArgs e)
            {
                #region Data

                string remoteAddr = "ServerError";
                SortedList slServerVars = new SortedList(13);
                // Extract a subset of the server variables
                slServerVars["SCRIPT_NAME"] = Request.ServerVariables["SCRIPT_NAME"];
                slServerVars["HTTP_HOST"] = Request.ServerVariables["HTTP_HOST"];
                slServerVars["HTTP_USER_AGENT"] = Request.ServerVariables["HTTP_USER_AGENT"];
                slServerVars["AUTH_TYPE"] = Request.ServerVariables["AUTH_USER"];
                slServerVars["AUTH_USER"] = Request.ServerVariables["AUTH_USER"];
                slServerVars["LOGON_USER"] = Request.ServerVariables["LOGON_USER"];
                slServerVars["SERVER_NAME"] = Request.ServerVariables["SERVER_NAME"];
                slServerVars["LOCAL_ADDR"] = Request.ServerVariables["LOCAL_ADDR"];
                slServerVars["REMOTE_ADDR"] = Request.ServerVariables["REMOTE_ADDR"];
                slServerVars["LastError"] = Server.GetLastError().ToString();
                slServerVars["QueryString"] = Request.QueryString;
                slServerVars["Form"] = Request.Form;
                slServerVars["Page"] = Request.Path;
                slServerVars["Message"] = Context.Error.Message.ToString();
                slServerVars["Source"] = Context.Error.Source.ToString();
                slServerVars["InnerException"] = Context.Error.InnerException;
                Cache.Insert(remoteAddr, slServerVars, null, DateTime.MaxValue, TimeSpan.FromMinutes(5));
                #endregion Data
                base.OnError(e);
            }
            protected void Page_Error(object sender, EventArgs e)
            {
                System.IO.FileInfo PageInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);
                string StrError = this.Context.Error.Message;
                this.Context.ClearError();
                Server.ClearError();
                Server.Transfer(ResolveUrl("~/Exception/ErrorPage.aspx?PageInfo=" + PageInfo.Name + "&Error=" + StrError));
            }

            #endregion PageLofe Cycle
        

            #endregion Page Event
        }
    }
}