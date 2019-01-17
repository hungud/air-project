using System;
using System.Web.Mvc;
using System.Threading;
using System.Web;
using App.Models.AccountManager;
using System.Web.Routing;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace App.Base
{
    /// <summary>
    /// Developed by RMSI Ltd (Rakesh Pal & Team)
    /// </summary>
    public class BaseController : Controller, IPagingDataList
    {

        public UserProfiles InitUserProfile { get; set; }
        public UserProfiles UserProfile
        {
            get { return (UserProfiles)(System.Web.HttpContext.Current.Session["UserProfileDetails"] == null ? new UserProfiles() : ((UserProfiles)System.Web.HttpContext.Current.Session["UserProfileDetails"])); }
            set { System.Web.HttpContext.Current.Session["UserProfileDetails"] = value; }
        }

        public BaseController()
        {
            this.PageTitle = ":: App DashBoard ::"; 
            this.AppName = ":: App DashBoard ::";
            ViewBag.PageTitle = this.PageTitle;
            ViewBag.AppTitle = this.PageTitle;
            ViewBag.AppName = this.AppName;
            ViewBag.Title = this.PageTitle;
            PageSize = 25;
          
        }

        public void GetGenError()
        {
            try
            {
                int zero = 0;
                int result = 100 / zero;
            }
            catch (DivideByZeroException ex)
            {
                throw ex;
            }
        }
        


       

        #region Application Resources Collection
        public Dictionary<string, string> GetApplicationResources()
        {
            // ... Organizational stuff goes here.

            // function GetPageResources (page, callback)
            //    $.ajax({ // Setup the AJAX call to your WebMethod
            //        data: "{ 'currentPage':'" + page + "' }",
            //        url: /Ajax/Resources.asmx/GetPageResources, // Or similar.
            //        success: function (result) { // To be replaced with .done in jQuery 1.8
            //            callback(result.d);
            //        }
            //    });
            //Then, in the .js executed on the page, you should be able to consume that data like:
            //// Whatever first executes when you load a page and its JS files
            //// -- I assume that you aren't using something like $(document).ready(function () {});
            //GetPageResources(document.location, SetPageResources);
            //function SetPageResources(resources) {
            //    for (currentResource in resources) {
            //        $("#" + currentResource.Key).html(currentResource.Value);
            //    }
            //}
            return new Dictionary<string, string>();
        }
        #endregion Application Resources Collection



        #region Comman Usese Application
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> AllAreasNames()
        {
            var areas = new List<string>();
            var areaNames = RouteTable.Routes.OfType<Route>().Where(d => d.DataTokens != null && d.DataTokens.ContainsKey("area")).Select(r => r.DataTokens["area"]).ToArray();
            foreach (string DataType in areaNames)
            {
                areas.Add(DataType);
            }
            return areas;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> AllControllerNames()
        {
            List<string> controllerNames = new List<string>();
            GetSubClasses<Controller>().ForEach(type => controllerNames.Add(type.Name));
            return controllerNames;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public List<string> AllActionsNames(string controllerName)
        {
            var types = from a in AppDomain.CurrentDomain.GetAssemblies()
                        from t in a.GetTypes()
                        where typeof(IController).IsAssignableFrom(t) && string.Equals(controllerName + "Controller", t.Name, StringComparison.OrdinalIgnoreCase)
                        select t;

            var controllerType = types.FirstOrDefault();
            if (controllerType == null)
            {
                return Enumerable.Empty<string>().ToList();
            }
            return new ReflectedControllerDescriptor(controllerType).GetCanonicalActions().Select(x => x.ActionName).ToList();
        }


        #endregion Comman Usese Application





        #region Controller Events
        public ActionResult SetCulture(string culture)
        {
            // culture = CultureHelper.GetImplementedCulture(culture);
            HttpCookie cookie = Request.Cookies["QPRO_App_Culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("QPRO_App_Culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home", new { area = "", controller = "" });
        }       

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }
       
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["QPRO_App_Culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages
            // Validate culture name
            cultureName = App.Base.Culture.CultureHelper.GetImplementedCulture(cultureName); // This is safe
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }

        
        protected override System.Web.Mvc.ContentResult Content(string content, string contentType, Encoding contentEncoding)
        {
            return base.Content(content, contentType, contentEncoding);
        }
        protected override System.Web.Mvc.IActionInvoker CreateActionInvoker()
        {
            return base.CreateActionInvoker();
        }
        protected override System.Web.Mvc.ITempDataProvider CreateTempDataProvider()
        {
            return base.CreateTempDataProvider();
        }
        protected override void EndExecute(IAsyncResult asyncResult)
        {
            base.EndExecute(asyncResult);
        }
        protected override void EndExecuteCore(IAsyncResult asyncResult)
        {
            base.EndExecuteCore(asyncResult);
        }
        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            base.Execute(requestContext);
        }
        protected override System.Web.Mvc.FileContentResult File(byte[] fileContents, string contentType, string fileDownloadName)
        {
            return base.File(fileContents, contentType, fileDownloadName);
        }
        protected override System.Web.Mvc.FilePathResult File(string fileName, string contentType, string fileDownloadName)
        {
            return base.File(fileName, contentType, fileDownloadName);
        }
        protected override System.Web.Mvc.FileStreamResult File(System.IO.Stream fileStream, string contentType, string fileDownloadName)
        {
            return base.File(fileStream, contentType, fileDownloadName);
        }
        protected override void HandleUnknownAction(string actionName)
        {
            base.HandleUnknownAction(actionName);
        }
        protected override System.Web.Mvc.HttpNotFoundResult HttpNotFound(string statusDescription)
        {
            return base.HttpNotFound(statusDescription);
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        protected override System.Web.Mvc.JavaScriptResult JavaScript(string script)
        {
            return base.JavaScript(script);
        }
        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return base.Json(data, contentType, contentEncoding);
        }
        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding, System.Web.Mvc.JsonRequestBehavior behavior)
        {
            return base.Json(data, contentType, contentEncoding, behavior);
        }
        protected override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            // If the browser session or authentication session has expired...
            //if (session.IsNewSession || Session["UserProfileDetails"] == null)
            //{
            //    if (filterContext.HttpContext.Request.IsAjaxRequest())
            //    {
            //        // For AJAX requests, return result as a simple string, 
            //        // and inform calling JavaScript code that a user should be redirected.
            //        JsonResult result = Json("SessionTimeout", "text/html");
            //        filterContext.Result = result;
            //    }
            //    else
            //    {
            //        // For round-trip requests,
            //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Accounts" },{ "Action", "Login" }
            //    });
            //    }
            //}
            base.OnActionExecuting(filterContext);
        }
        protected override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //GetUserAuthenticationAuthorizationValidation(filterContext);
        }

        private void GetUserAuthenticationAuthorizationValidation(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (!filterContext.RouteData.GetRequiredString("controller").Equals("Home"))
            {
                if (!User.Identity.IsAuthenticated)
                {
                    if (string.IsNullOrEmpty(this.UserProfile.UserName))
                    {
                        Response.Redirect("~/", true);
                    }
                }
            }
        }


        protected override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            Exception ex = filterContext.Exception;
            //do something with these details here
            //BasePage.BasePage.GetManageException(ex);
            //RedirectToAction("Error", "Home");
            ExceptionLog(ex);
            System.Type ExceptionType = filterContext.Exception.GetType();
            //var exception = Server.GetLastError();
            //if (ExceptionType.Name == "DbUpdateException")
            //{
            //    ViewData["ErrorMessage"] = filterContext.Exception.InnerException.Message;
            //    this.View("DatabaseException", filterContext.Exception).ExecuteResult(this.ControllerContext);

            //}
            //else
            //{
            //    ViewData["ErrorMessage"] = filterContext.Exception.Message;
            //    this.View("ApplicationException", filterContext.Exception).ExecuteResult(this.ControllerContext);
            //}
           
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            if (IsAjax(filterContext))
            {
                //Return JSON
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    //Data = new { error = true, message = "Sorry, an error occurred while processing your request." }
                    Data = new { error = true, message = filterContext.Exception.Message }
                };
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            else
            {
                //Redirect user to error page
                filterContext.ExceptionHandled = true;
                filterContext.Result = this.RedirectToAction("Error", "Home");
            }
            base.OnException(filterContext);
            // Write error logging code here if you wish.

            //if want to get different of the request
            //var currentController = (string)filterContext.RouteData.Values["controller"];
            //var currentActionName = (string)filterContext.RouteData.Values["action"];
        }
        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        public void ExceptionLog(System.Exception filterContext)
        {
            ErrorsLog.ErrorsLogInstance.ManageException(filterContext);
            //RedirectToAction("Error", "Home");
        }


        /*
        protected override void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        protected override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        protected override System.Web.Mvc.PartialViewResult PartialView(string viewName, object model)
        {
            return base.PartialView(viewName, model);
        }
        protected override System.Web.Mvc.RedirectResult Redirect(string url)
        {
            return base.Redirect(url);
        }
        protected override System.Web.Mvc.RedirectResult RedirectPermanent(string url)
        {
            return base.RedirectPermanent(url);
        }
        protected override System.Web.Mvc.RedirectToRouteResult RedirectToAction(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            return base.RedirectToAction(actionName, controllerName, routeValues);
        }
        protected override System.Web.Mvc.RedirectToRouteResult RedirectToActionPermanent(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            return base.RedirectToActionPermanent(actionName, controllerName, routeValues);
        }
        protected override System.Web.Mvc.RedirectToRouteResult RedirectToRoute(string routeName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            return base.RedirectToRoute(routeName, routeValues);
        }
        protected override System.Web.Mvc.RedirectToRouteResult RedirectToRoutePermanent(string routeName, System.Web.Routing.RouteValueDictionary routeValues)
        {
            return base.RedirectToRoutePermanent(routeName, routeValues);
        }
        protected override System.Web.Mvc.ViewResult View(string viewName, string masterName, object model)
        {
            return base.View(viewName, masterName, model);
        }
        protected override System.Web.Mvc.ViewResult View(System.Web.Mvc.IView view, object model)
        {
            return base.View(view, model);
        }
        protected override void ExecuteCore()
        {
            base.ExecuteCore();
        }
        */


        protected override bool DisableAsyncSupport
        {
            get
            {
                return base.DisableAsyncSupport;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
        #endregion Controller Events

        #region  Comman Usese in Controlls
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string UserName()
        {
            if (User == null || User.Identity == null)
                return string.Empty;
            return User.Identity.Name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMsg"></param>
        protected void ErrorMessage(string errorMsg)
        {
            this.ViewBag.ErrorMessage = errorMsg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="succeedMsg"></param>
        protected void SucceedMessage(string succeedMsg)
        {
            this.ViewBag.SucceedMessage = succeedMsg;
        }


        
        public string PageTitle { get; set; }
        public string AppName { get; set; }
        public int FirstItemOnPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int LastItemOnPage { get; set; }
        public int PageCount { get; set; }
        public static int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public string SortDirection { get; set; }
        public string SortExpression { get; set; }
        public enum SortOrder
        {
            /// <summary>
            /// The ascending.
            /// </summary>
            Ascending,
            /// <summary>
            /// The descending.
            /// </summary>
            Descending
        }
        #endregion Comman Usese in Controlls
    }

    public class BaseHomeController : Controller, IPagingDataList
    {
        public UserProfiles InitUserProfile { get; set; }
        public UserProfiles UserProfile
        {
            get { return (UserProfiles)(System.Web.HttpContext.Current.Session["UserProfileDetails"] == null ? new UserProfiles() : ((UserProfiles)System.Web.HttpContext.Current.Session["UserProfileDetails"])); }
            set { System.Web.HttpContext.Current.Session["UserProfileDetails"] = value; }
        }
        public BaseHomeController()
        {
            this.PageTitle = "::App DashBoard  ::";
            this.AppName = ":: App DashBoard  ::";
            ViewBag.PageTitle = this.PageTitle;
            ViewBag.AppTitle = this.PageTitle;
            ViewBag.AppName = this.AppName;
            ViewBag.Title = this.PageTitle;
            PageSize = 25;
            
        }
             
       
        #region  Comman Usese in Controlls
        private const String APP_Culture_LangEntry = "language";
        public static String APP_Culture_Name { get { return "APP_Culture"; } }
        public string PageTitle { get; set; }
        public string AppName { get; set; }
        public int FirstItemOnPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int LastItemOnPage { get; set; }
        public int PageCount { get; set; }
        public static int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public string SortDirection { get; set; }
        public string SortExpression { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string UserName()
        {
            if (User == null || User.Identity == null)
                return string.Empty;
            return User.Identity.Name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMsg"></param>
        protected void ErrorMessage(string errorMsg)
        {
            this.ViewBag.ErrorMessage = errorMsg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="succeedMsg"></param>
        protected void SucceedMessage(string succeedMsg)
        {
            this.ViewBag.SucceedMessage = succeedMsg;
        }

        #endregion Comman Usese in Controlls


         #region Comman Usese Application
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> AllAreasNames()
        {
            var areas = new List<string>();
            var areaNames = RouteTable.Routes.OfType<Route>().Where(d => d.DataTokens != null && d.DataTokens.ContainsKey("area")).Select(r => r.DataTokens["area"]).ToArray();
            foreach (string DataType in areaNames)
            {
                areas.Add(DataType);
            }
            return areas;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> AllControllerNames()
        {
            List<string> controllerNames = new List<string>();
            GetSubClasses<Controller>().ForEach(type => controllerNames.Add(type.Name));
            return controllerNames;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public List<string> AllActionsNames(string controllerName)
        {
            var types = from a in AppDomain.CurrentDomain.GetAssemblies()
                        from t in a.GetTypes()
                        where typeof(IController).IsAssignableFrom(t) && string.Equals(controllerName + "Controller", t.Name, StringComparison.OrdinalIgnoreCase)
                        select t;

            var controllerType = types.FirstOrDefault();
            if (controllerType == null)
            {
                return Enumerable.Empty<string>().ToList();
            }
            return new ReflectedControllerDescriptor(controllerType).GetCanonicalActions().Select(x => x.ActionName).ToList();
        }


        #endregion Comman Usese Application



        #region Culture
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies[APP_Culture_Name];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages
            // Validate culture name
            cultureName = App.Base.Culture.CultureHelper.GetImplementedCulture(cultureName); // This is safe
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            // culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies[APP_Culture_Name];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie(APP_Culture_Name);
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home", new { area = "", controller = "" });
        }
        public ActionResult SetAppCulture(HttpResponseBase httpRequestBase, string culture)
        {
            // Validate input
            // culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies[APP_Culture_Name];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie(APP_Culture_Name);
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            httpRequestBase.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home", new { area = "", controller = "" });
        }



        #endregion Culture

        protected override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            Exception ex = filterContext.Exception;
            ExceptionLog(ex);
            System.Type ExceptionType = filterContext.Exception.GetType();
            var exception = Server.GetLastError();
            //if (ExceptionType.Name == "DbUpdateException")
            //{
            //    ViewData["ErrorMessage"] = filterContext.Exception.InnerException.Message;
            //    this.View("DatabaseException", filterContext.Exception).ExecuteResult(this.ControllerContext);
            //}
            //else
            //{
            //    ViewData["ErrorMessage"] = filterContext.Exception.Message;
            //    this.View("ApplicationException", filterContext.Exception).ExecuteResult(this.ControllerContext);
            //}

            //if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    return;
            //}
            if (IsAjax(filterContext))
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    //Data = new { error = true, message = "Sorry, an error occurred while processing your request." }
                    Data = new { error = true, message = filterContext.Exception.Message }
                };
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            else
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = this.RedirectToAction("Error", "Home", new { area = "",controller="" });
            }
            base.OnException(filterContext);
        }
        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        public void ExceptionLog(System.Exception filterContext)
        {
            ErrorsLog.ErrorsLogInstance.ManageException(filterContext);
            //RedirectToAction("Error", "Home");
        }

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CultureAttribute : ActionFilterAttribute
    {
        private const String APP_Culture_LangEntry = "language";
        public String APP_Culture_Name { get; set; }
        public static String APP_Culture_CookieName { get { return "APP_Culture_CookieName"; } }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var culture = APP_Culture_Name;
            if (String.IsNullOrEmpty(culture))
                culture = GetSavedCultureOrDefault(filterContext.RequestContext.HttpContext.Request);
            // Set culture on current thread
            SetCultureOnThread(culture);
            // Proceed as usual
            base.OnActionExecuting(filterContext);
        }

        public static void SavePreferredCulture(HttpResponseBase response, String language, Int32 expireDays = 1)
        {
            var cookie = new HttpCookie(APP_Culture_CookieName) { Expires = DateTime.Now.AddDays(expireDays) };
            cookie.Values[APP_Culture_LangEntry] = language;
            response.Cookies.Add(cookie);
        }

        public static String GetSavedCultureOrDefault(HttpRequestBase httpRequestBase)
        {
            var culture = "";
            var cookie = httpRequestBase.Cookies[APP_Culture_CookieName];
            if (cookie != null)
                culture = cookie.Values[APP_Culture_LangEntry];
            return culture;
        }

        private static void SetCultureOnThread(String language)
        {
            var cultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }

    public interface IPagingDataList
    {
        // Summary:
        //     One-based index of the first item in the paged subset.
        int FirstItemOnPage { get; set; }
        //
        // Summary:
        //     Returns true if this is NOT the last subset within the superset.
        bool HasNextPage { get; set; }
        //
        // Summary:
        //     Returns true if this is NOT the first subset within the superset.
        bool HasPreviousPage { get; set; }
        //
        // Summary:
        //     Returns true if this is the first subset within the superset.
        bool IsFirstPage { get; set; }
        //
        // Summary:
        //     Returns true if this is the last subset within the superset.
        bool IsLastPage { get; set; }
        //
        // Summary:
        //     One-based index of the last item in the paged subset.
        int LastItemOnPage { get; set; }
        //
        // Summary:
        //     Total number of subsets within the superset.
        int PageCount { get; set; }
        //
        // Summary:
        //     Maximum size any individual subset.
        int PageSize { get; set; }
        //
        // Summary:
        //     Total number of objects contained within the superset.
        int TotalItemCount { get; set; }
        //
        // Summary:
        //     Total number of objects contained within the superset.
        string SortDirection { get; set; }
        //
        // Summary:
        //     Total number of objects contained within the superset.
        string SortExpression { get; set; }
        //
        // Summary:
        //     Total number of objects contained within the superset.
        string PageTitle { get; set; }
    }

    public class RedirectingAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (1==1)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }
}
