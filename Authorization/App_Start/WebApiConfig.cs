﻿using Authorization.Filters;
using System.Web.Http;

namespace Authorization
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //GlobalConfiguration.Configuration.Filters.Add(new ApiAuthenticationFilter());
        }
    }
}
