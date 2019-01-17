namespace App.Base
{
    namespace Page
    {
        using System;
        using System.Collections;

        /// <summary>
        /// Summary description for AspNetBaseUserControl
        /// </summary>
        public class BasePageUserControl : System.Web.UI.UserControl
        {
            public BasePageUserControl()
            {
                //
                // TODO: Add constructor logic here
                //
            }
            protected override void AddedControl(System.Web.UI.Control control, int index)
            {
                if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
                    this.Page.ClientTarget = "uplevel";
                base.AddedControl(control, index);
            }
            protected override void OnError(EventArgs e)
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
        }
    }
}