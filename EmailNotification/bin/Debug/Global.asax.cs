using Heroic.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Helpdesk
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HeroicAutoMapperConfigurator.LoadMapsFromCallerAndReferencedAssemblies();

            ViewEngines.Engines.Add(new CustomRazrViewEngine());
        }
    }

    public class CustomRazrViewEngine : RazorViewEngine
    {
        private static readonly string[] NewPartialViewFormats =
        {
            "~/Views/{1}/Partials/{0}.cshtml",
            "~/Views/Shared/UserMenus/{0}.cshtml"
        };

        public CustomRazrViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
        }

    }
}
