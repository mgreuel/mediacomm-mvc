using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using AutoMapper;

using MediaCommMvc.Web.Infrastructure;

namespace MediaCommMvc.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public const string ConfigId = "Config";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.RegisterMappings();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            DocumentStoreContainer.Initialize();
            this.EnsureConfig();
        }

        private void EnsureConfig()
        {
            Config config = DocumentStoreContainer.CurrentSession.Load<Config>(ConfigId);

            if (config == null)
            {
                DocumentStoreContainer.CurrentSession.Store(new Config
                {
                    Id = ConfigId,
                    Sitename = "Absolutmoments",
                    PhotoStorageRootFolder = @"C:\temp\Absolutmoments\Photos"
                });

                DocumentStoreContainer.CurrentSession.SaveChanges();
            }
        }
    }
}
