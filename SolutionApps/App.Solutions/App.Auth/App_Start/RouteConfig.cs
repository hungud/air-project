using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Auth
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           
            routes.MapRoute(
               name: "Login",
               url: "Login",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
           );

            //routes.MapRoute(
            //      name: "ForgetPassword",
            //      url: "ForgetPassword",
            //      defaults: new { controller = "Account", action = "Login" }
            //  );
            //routes.MapRoute(
            //     name: "ResetPassword",
            //     url: "ResetPassword",
            //     defaults: new { controller = "Account", action = "Login" }
            // );

            //routes.MapRoute(
            //    name: "Register",
            //    url: "Register",
            //    defaults: new { controller = "Account", action = "Login" }
            //);
        }
    }
}
