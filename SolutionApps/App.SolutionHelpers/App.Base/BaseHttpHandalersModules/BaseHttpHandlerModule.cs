
using System.Text;
using System.Web;
using System;
using System.Security.Principal;
using System.Threading;
using System.Net.Http.Headers;
namespace App.Base
{
    namespace BaseHttpHandlerModule
    {
        using System;
        using System.Web;
        using System.IO;
        using System.Web.UI;
        using System.Web.SessionState;
        /// <summary>
        /// Developed by RMSI Ltd (Rakesh Pal)
        /// </summary>
        public class BaseHttpHandlerModule : IHttpModule, IRequiresSessionState, IHttpHandler, IHttpHandlerFactory
        {
            public BaseHttpHandlerModule()
            {
            }

            const string ORIGINAL_PATHINFO = "UrlRewriterOriginalPathInfo";
            const string ORIGINAL_QUERIES = "UrlRewriterOriginalQueries";

            #region IHttpHandlerFactory

            public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
            {
                context.Items["fileName"] = Path.GetFileNameWithoutExtension(url).ToLower();
                return PageParser.GetCompiledPageInstance(url, context.Server.MapPath("~/UserAccount/frmLogin.aspx"), context);
            }

            public void ReleaseHandler(IHttpHandler handler)
            {
            }

            #endregion IHttpHandlerFactory



            #region IHttpHandler
            public bool IsReusable
            {
                get
                {
                    return true;
                }
            }
            public void ProcessRequest(System.Web.HttpContext context)
            {
                // Check to see if the specified HTML file actual exists and serve it if so..        
                String strReqPath = context.Server.MapPath(context.Request.AppRelativeCurrentExecutionFilePath);
                if (File.Exists(strReqPath))
                {
                    context.Response.WriteFile(strReqPath);
                    context.Response.End();
                    return;
                }

                // Record the original request PathInfo and QueryString information to handle graceful postbacks        
                context.Items[ORIGINAL_PATHINFO] = context.Request.PathInfo;
                context.Items[ORIGINAL_QUERIES] = context.Request.QueryString.ToString();

                // Map the friendly URL to the back-end one..        
                String strVirtualPath = "";
                String strQueryString = "";
                MapFriendlyUrl(context, out strVirtualPath, out strQueryString);
                if (strVirtualPath.Length > 0)
                {
                    foreach (string strOriginalQuery in context.Request.QueryString.Keys)
                    {
                        // To ensure that any query strings passed in the original request are preserved, we append these                 
                        // to the new query string now, taking care not to add any keys which have been rewritten during the handler..                
                        if (strQueryString.ToLower().IndexOf(strOriginalQuery.ToLower() + "=") < 0)
                        {
                            strQueryString += string.Format("{0}{1}={2}", ((strQueryString.Length > 0) ? "&" : ""), strOriginalQuery, context.Request.QueryString[strOriginalQuery]);
                        }
                    }

                    // Apply the required query strings to the request            
                    context.RewritePath(context.Request.Path, string.Empty, strQueryString);
                    // Now get a page handler for the ASPX page required, using this context.              
                    Page aspxHandler = (Page)PageParser.GetCompiledPageInstance(strVirtualPath, context.Server.MapPath(strVirtualPath), context);
                    // Execute the handler..            
                    aspxHandler.PreRenderComplete += new EventHandler(AspxPage_PreRenderComplete);
                    aspxHandler.ProcessRequest(context);
                }
                else
                {
                    // No mapping was found - emit a 404 response.             
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Page Not Found");
                    context.Response.End();
                }

            }
            void MapFriendlyUrl(HttpContext context, out String strVirtualPath, out String strQueryString)
            {
                strVirtualPath = ""; strQueryString = "";
                // TODO: This routine should examine the context.Request properties and implement        
                //       an appropriate mapping system.        
                //        
                //       Set strVirtualPath to the virtual path of the target aspx page.        
                //       Set strQueryString to any query strings required for the page.         
                if (context.Request.Path.IndexOf("Default.aspx") >= 0)
                {
                    // Example hard coded mapping of "FriendlyPage.html" to "UnfriendlyPage.aspx"             

                    strVirtualPath = "~/UserAccount/frmLogin.aspx";
                    strQueryString = "FirstQuery=1&SecondQuery=2";
                }
            }
            void AspxPage_PreRenderComplete(object sender, EventArgs e)
            {
                // We need to rewrite the path replacing the original tail and query strings..        
                // This happens AFTER the page has been loaded and setup but has the effect of ensuring        
                // postbacks to the page retain the original un-rewritten pages URL and queries.         

                HttpContext.Current.RewritePath(HttpContext.Current.Request.Path,
                HttpContext.Current.Items[ORIGINAL_PATHINFO].ToString(),
                HttpContext.Current.Items[ORIGINAL_QUERIES].ToString());
            }
            #endregion IHttpHandler



            #region IHttpModule
            public void Dispose()
            {

            }
            public void Init(HttpApplication objApplication)
            {
                // Register event handler of the piple line
                objApplication.BeginRequest += new EventHandler(this.context_BeginRequest);
                objApplication.EndRequest += new EventHandler(this.context_EndRequest);


                objApplication.AcquireRequestState += new EventHandler(objApplication_AcquireRequestState);
                objApplication.AuthenticateRequest += new EventHandler(objApplication_AuthenticateRequest);
                objApplication.AuthorizeRequest += new EventHandler(objApplication_AuthorizeRequest);
                objApplication.Error += new EventHandler(objApplication_Error);


                //This operation requires IIS integrated pipeline mode.
                //objApplication.LogRequest += new EventHandler(objApplication_LogRequest);
                //objApplication.MapRequestHandler += new EventHandler(objApplication_MapRequestHandler);


                objApplication.PreRequestHandlerExecute += new EventHandler(objApplication_PreRequestHandlerExecute);
                objApplication.PreSendRequestContent += new EventHandler(objApplication_PreSendRequestContent);
                objApplication.PreSendRequestHeaders += new EventHandler(objApplication_PreSendRequestHeaders);
                objApplication.ReleaseRequestState += new EventHandler(objApplication_ReleaseRequestState);
                objApplication.ResolveRequestCache += new EventHandler(objApplication_ResolveRequestCache);


                objApplication.PostAcquireRequestState += new EventHandler(objApplication_PostAcquireRequestState);
                objApplication.PostAuthenticateRequest += new EventHandler(objApplication_PostAuthenticateRequest);
                objApplication.PostAuthorizeRequest += new EventHandler(objApplication_PostAuthorizeRequest);

                //This operation requires IIS integrated pipeline mode.
                //objApplication.PostLogRequest += new EventHandler(objApplication_PostLogRequest);
                //objApplication.PostMapRequestHandler += new EventHandler(objApplication_PostMapRequestHandler);
                objApplication.PostReleaseRequestState += new EventHandler(objApplication_PostReleaseRequestState);
                objApplication.PostRequestHandlerExecute += new EventHandler(objApplication_PostRequestHandlerExecute);
                objApplication.PostResolveRequestCache += new EventHandler(objApplication_PostResolveRequestCache);

                objApplication.UpdateRequestCache += new EventHandler(objApplication_UpdateRequestCache);

            }


            void objApplication_UpdateRequestCache(object sender, EventArgs e)
            {

                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("UpdateRequestCache Request called at " + DateTime.Now.ToString());
            }
            void objApplication_ResolveRequestCache(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("End Request called at " + DateTime.Now.ToString());
            }
            void objApplication_ReleaseRequestState(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("ReleaseRequestState Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PreSendRequestHeaders(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PreSendRequestHeaders Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PreSendRequestContent(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PreSendRequestContent Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PreRequestHandlerExecute(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PreRequestHandlerExecute Request called at " + DateTime.Now.ToString());
            }


            void objApplication_PostReleaseRequestState(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PostReleaseRequestState Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostMapRequestHandler(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PostMapRequestHandler Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostLogRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PostLogRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostAuthorizeRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PostAuthorizeRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostAuthenticateRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PostAuthenticateRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostAcquireRequestState(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PostAcquireRequestState Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostResolveRequestCache(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PostResolveRequestCache Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostRequestHandlerExecute(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("PostRequestHandlerExecute Request called at " + DateTime.Now.ToString());
            }



            void objApplication_MapRequestHandler(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("MapRequestHandler Request called at " + DateTime.Now.ToString());
            }
            void objApplication_LogRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("LogRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_Error(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("Error Request called at " + DateTime.Now.ToString());
            }
            void objApplication_AuthorizeRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("AuthorizeRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_AuthenticateRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("AuthenticateRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_AcquireRequestState(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("AcquireRequestState Request called at " + DateTime.Now.ToString());
            }
            public void context_EndRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("EndRequest Request called at " + DateTime.Now.ToString());
                //HttpApplication appObject = (HttpApplication)sender;
                //HttpContext contextObject = appObject.Context;
                //contextObject.RewritePath("~/UserAccount/frmLogin.aspx");
            }
            public void context_BeginRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("BeginRequest Request called at " + DateTime.Now.ToString());
            }


            #endregion IHttpModule

        }
    }
    namespace BaseBaseHttpHandler
    {
        using System;
        using System.Web;
        using System.IO;
        using System.Web.UI;
        using System.Web.SessionState;
        /// <summary>
        /// Developed by RMSI Ltd (Rakesh Pal)
        /// </summary>
        public class BaseHttpHandler :  IHttpHandler
        {
            public BaseHttpHandler()
            {
            }

            const string ORIGINAL_PATHINFO = "UrlRewriterOriginalPathInfo";
            const string ORIGINAL_QUERIES = "UrlRewriterOriginalQueries";
            #region IHttpHandler
            public bool IsReusable
            {
                get
                {
                    return true;
                }
            }
            public void ProcessRequest(System.Web.HttpContext context)
            {
                // Check to see if the specified HTML file actual exists and serve it if so..        
                String strReqPath = context.Server.MapPath(context.Request.AppRelativeCurrentExecutionFilePath);
                if (File.Exists(strReqPath))
                {
                    context.Response.WriteFile(strReqPath);
                    context.Response.End();
                    return;
                }

                // Record the original request PathInfo and QueryString information to handle graceful postbacks        
                context.Items[ORIGINAL_PATHINFO] = context.Request.PathInfo;
                context.Items[ORIGINAL_QUERIES] = context.Request.QueryString.ToString();

                // Map the friendly URL to the back-end one..        
                String strVirtualPath = "";
                String strQueryString = "";
                MapFriendlyUrl(context, out strVirtualPath, out strQueryString);
                if (strVirtualPath.Length > 0)
                {
                    foreach (string strOriginalQuery in context.Request.QueryString.Keys)
                    {
                        // To ensure that any query strings passed in the original request are preserved, we append these                 
                        // to the new query string now, taking care not to add any keys which have been rewritten during the handler..                
                        if (strQueryString.ToLower().IndexOf(strOriginalQuery.ToLower() + "=") < 0)
                        {
                            strQueryString += string.Format("{0}{1}={2}", ((strQueryString.Length > 0) ? "&" : ""), strOriginalQuery, context.Request.QueryString[strOriginalQuery]);
                        }
                    }

                    // Apply the required query strings to the request            
                    context.RewritePath(context.Request.Path, string.Empty, strQueryString);
                    // Now get a page handler for the ASPX page required, using this context.              
                    Page aspxHandler = (Page)PageParser.GetCompiledPageInstance(strVirtualPath, context.Server.MapPath(strVirtualPath), context);
                    // Execute the handler..            
                    aspxHandler.PreRenderComplete += new EventHandler(AspxPage_PreRenderComplete);
                    aspxHandler.ProcessRequest(context);
                }
                else
                {
                    // No mapping was found - emit a 404 response.             
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Page Not Found");
                    context.Response.End();
                }

            }
            void MapFriendlyUrl(HttpContext context, out String strVirtualPath, out String strQueryString)
            {
                strVirtualPath = ""; strQueryString = "";
                // TODO: This routine should examine the context.Request properties and implement        
                //       an appropriate mapping system.        
                //        
                //       Set strVirtualPath to the virtual path of the target aspx page.        
                //       Set strQueryString to any query strings required for the page.         
                if (context.Request.Path.IndexOf("Default.aspx") >= 0)
                {
                    // Example hard coded mapping of "FriendlyPage.html" to "UnfriendlyPage.aspx"             

                    strVirtualPath = "~/UserAccount/frmLogin.aspx";
                    strQueryString = "FirstQuery=1&SecondQuery=2";
                }
            }
            void AspxPage_PreRenderComplete(object sender, EventArgs e)
            {
                // We need to rewrite the path replacing the original tail and query strings..        
                // This happens AFTER the page has been loaded and setup but has the effect of ensuring        
                // postbacks to the page retain the original un-rewritten pages URL and queries.         

                HttpContext.Current.RewritePath(HttpContext.Current.Request.Path,
                HttpContext.Current.Items[ORIGINAL_PATHINFO].ToString(),
                HttpContext.Current.Items[ORIGINAL_QUERIES].ToString());
            }
            #endregion IHttpHandler
        }
    }

    namespace BaseBaseHttpModule
    {
        using System;
        using System.Web;
        using System.IO;
        using System.Web.UI;
        using System.Web.SessionState;
        /// <summary>
        /// Developed by RMSI Ltd (Rakesh Pal)
        /// </summary>
        public class BaseHttpModule : IHttpModule
        {
            private const string Realm = "MyRealm";
            public BaseHttpModule()
            {
            }
            #region IHttpModule
            public void Dispose()
            {

            }
            public void Init(HttpApplication objApplication)
            {
                // Register event handler of the piple line
                objApplication.BeginRequest += new EventHandler(this.context_BeginRequest);
                objApplication.EndRequest += new EventHandler(this.context_EndRequest);


                objApplication.AcquireRequestState += new EventHandler(objApplication_AcquireRequestState);
                objApplication.AuthenticateRequest += new EventHandler(objApplication_AuthenticateRequest);
                objApplication.AuthorizeRequest += new EventHandler(objApplication_AuthorizeRequest);
                objApplication.Error += new EventHandler(objApplication_Error);


                //This operation requires IIS integrated pipeline mode.
                //objApplication.LogRequest += new EventHandler(objApplication_LogRequest);
                //objApplication.MapRequestHandler += new EventHandler(objApplication_MapRequestHandler);


                objApplication.PreRequestHandlerExecute += new EventHandler(objApplication_PreRequestHandlerExecute);
                objApplication.PreSendRequestContent += new EventHandler(objApplication_PreSendRequestContent);
                objApplication.PreSendRequestHeaders += new EventHandler(objApplication_PreSendRequestHeaders);
                objApplication.ReleaseRequestState += new EventHandler(objApplication_ReleaseRequestState);
                objApplication.ResolveRequestCache += new EventHandler(objApplication_ResolveRequestCache);


                objApplication.PostAcquireRequestState += new EventHandler(objApplication_PostAcquireRequestState);
                objApplication.PostAuthenticateRequest += new EventHandler(objApplication_PostAuthenticateRequest);
                objApplication.PostAuthorizeRequest += new EventHandler(objApplication_PostAuthorizeRequest);

                //This operation requires IIS integrated pipeline mode.
                //objApplication.PostLogRequest += new EventHandler(objApplication_PostLogRequest);
                //objApplication.PostMapRequestHandler += new EventHandler(objApplication_PostMapRequestHandler);
                objApplication.PostReleaseRequestState += new EventHandler(objApplication_PostReleaseRequestState);
                objApplication.PostRequestHandlerExecute += new EventHandler(objApplication_PostRequestHandlerExecute);
                objApplication.PostResolveRequestCache += new EventHandler(objApplication_PostResolveRequestCache);

                objApplication.UpdateRequestCache += new EventHandler(objApplication_UpdateRequestCache);

            }

            #region IHttpModule Events
            void objApplication_UpdateRequestCache(object sender, EventArgs e)
            {

                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: UpdateRequestCache Request called at " + DateTime.Now.ToString());
            }
            void objApplication_ResolveRequestCache(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: End Request called at " + DateTime.Now.ToString());
            }
            void objApplication_ReleaseRequestState(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: ReleaseRequestState Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PreSendRequestHeaders(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PreSendRequestHeaders Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PreSendRequestContent(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PreSendRequestContent Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PreRequestHandlerExecute(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PreRequestHandlerExecute Request called at " + DateTime.Now.ToString());
            }

            void objApplication_PostReleaseRequestState(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PostReleaseRequestState Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostMapRequestHandler(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PostMapRequestHandler Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostLogRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PostLogRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostAuthorizeRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PostAuthorizeRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostAuthenticateRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PostAuthenticateRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostAcquireRequestState(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PostAcquireRequestState Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostResolveRequestCache(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PostResolveRequestCache Request called at " + DateTime.Now.ToString());
            }
            void objApplication_PostRequestHandlerExecute(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: PostRequestHandlerExecute Request called at " + DateTime.Now.ToString());
            }

            void objApplication_MapRequestHandler(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: MapRequestHandler Request called at " + DateTime.Now.ToString());
            }
            void objApplication_LogRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: LogRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_Error(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: Error Request called at " + DateTime.Now.ToString());
            }
            void objApplication_AuthorizeRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: AuthorizeRequest Request called at " + DateTime.Now.ToString());
            }
            void objApplication_AuthenticateRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: AuthenticateRequest Request called at " + DateTime.Now.ToString());
                var request = HttpContext.Current.Request;
                var authHeader = request.Headers["Authorization"];
                if (authHeader != null)
                {
                    var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
                    // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                    if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
                    {
                        AuthenticateUser(authHeaderVal.Parameter);
                    }
                }
            }
            void objApplication_AcquireRequestState(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: AcquireRequestState Request called at " + DateTime.Now.ToString());
            }
            public void context_EndRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: EndRequest Request called at " + DateTime.Now.ToString());
                //HttpApplication appObject = (HttpApplication)sender;
                //HttpContext contextObject = appObject.Context;
                //contextObject.RewritePath("~/UserAccount/frmLogin.aspx");
                var response = HttpContext.Current.Response;
                if (response.StatusCode == 401)
                {
                    response.Headers.Add("WWW-Authenticate",
                        string.Format("Basic realm=\"{0}\"", Realm));
                }
            }
            public void context_BeginRequest(object sender, EventArgs e)
            {
                ErrorsLog.ErrorsLogInstance.HttpModuleLogEvent("::::::::::: BeginRequest Request called at " + DateTime.Now.ToString());
            }

            private static void SetPrincipal(IPrincipal principal)
            {
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }
            private static bool CheckPassword(string username, string password)
            {
                return username == "user" && password == "password";
            }
            private static void AuthenticateUser(string credentials)
            {
                try
                {
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    credentials = encoding.GetString(Convert.FromBase64String(credentials));

                    int separator = credentials.IndexOf(':');
                    string name = credentials.Substring(0, separator);
                    string password = credentials.Substring(separator + 1);

                    if (CheckPassword(name, password))
                    {
                        var identity = new GenericIdentity(name);
                        SetPrincipal(new GenericPrincipal(identity, null));
                    }
                    else
                    {
                        // Invalid username or password.
                        HttpContext.Current.Response.StatusCode = 401;
                    }
                }
                catch (FormatException)
                {
                    // Credentials were not formatted correctly.
                    HttpContext.Current.Response.StatusCode = 401;
                }
            }
            #endregion IHttpModule Events

            #endregion IHttpModule
        }
    }
}