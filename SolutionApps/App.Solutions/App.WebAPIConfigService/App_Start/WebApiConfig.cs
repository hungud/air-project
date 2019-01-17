using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Elmah.Contrib.WebApi;

namespace App.WebAPIConfigService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true, PreflightMaxAge=3600 };
            config.EnableCors(cors);
            //config.EnableCors();

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "ApiById",routeTemplate: "api/{controller}/{id}",defaults: new { id = RouteParameter.Optional },constraints: new { id = @"^[0-9]+$" });

            config.Routes.MapHttpRoute(name: "ApiByName",routeTemplate: "api/{controller}/{action}/{name}",defaults: null,constraints: new { name = @"^[a-z]+$" });

            config.Routes.MapHttpRoute(name: "ApiByAction", routeTemplate: "api/{controller}/{action}",defaults: new { action = "Get" });

            config.Routes.MapHttpRoute("DefaultApiWithAction", "Api/{controller}/{action}");

            config.Routes.MapHttpRoute(name: "DefaultApi",routeTemplate: "api/{controller}/{id}",defaults: new { id = RouteParameter.Optional });
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());
            config.Filters.Add(new ElmahHandleWebApiErrorAttribute());
        }
    }
}
