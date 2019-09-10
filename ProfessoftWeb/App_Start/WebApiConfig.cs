using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProfessoftWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Konfiguracja i usługi składnika Web API

            // Trasy składnika Web API
            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*") { SupportsCredentials = true };
            config.EnableCors(cors);
            
            config.MapHttpAttributeRoutes();

        }
    }
}
