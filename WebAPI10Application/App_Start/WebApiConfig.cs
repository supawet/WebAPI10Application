using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
/*
using Owin;
using Microsoft.Owin;
using WebAPI07Application.Providers;
using Microsoft.Owin.Security.OAuth;
*/

using System.Web.Http.Cors;

namespace WebAPI10Application
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services  
            // Configure Web API to use only bearer token authentication.  
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            var cors = new EnableCorsAttribute("*", "*", "*");

            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            */

            config.Routes.MapHttpRoute(
                name: "LOG",
                routeTemplate: "api/LOG/{id}/{action}/{subaction}",
                defaults: new { controller = "LOG", id = RouteParameter.Optional, subaction = RouteParameter.Optional }//,
                //constraints: new { id="length(2)"}
            );
        }
    }
}
