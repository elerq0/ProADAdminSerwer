﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProfessoftWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "http://www");
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:44329");
            Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            if (Request.HttpMethod == "OPTIONS")
            {
                Response.Headers.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin, Origin, Content-Type, X-Auth-Token");
                Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
                
                Response.Headers.Add("Access-Control-Max-Age", "1728000");
                Response.End();
            }
        }
    }
}
