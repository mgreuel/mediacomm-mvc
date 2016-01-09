using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using AutoMapper;

using FluentScheduler;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Forum.Notifications;
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

            TaskManager.Initialize(new ForumNotificationSender(() => new UserStorage(DocumentStoreContainer.CreateNewSession)));
        }

        private void EnsureConfig()
        {
            Config config = DocumentStoreContainer.CurrentRequestSession.Load<Config>(ConfigId);

            string defaultPhotoPath = @"C:\temp\Absolutmoments\Photos";
            string defaultSitename = "Absolutmoments";

            if (config == null)
            {
                config = new Config
                {
                    Id = ConfigId,
                    Sitename = defaultSitename,
                    PhotoStorageRootFolder = defaultPhotoPath,
                    RegistrationCode = RandomString(6),
                    BaseUrl = "http://replace.me"
                };
            }
            else
            {
                config.PhotoStorageRootFolder = config.PhotoStorageRootFolder ?? defaultPhotoPath;
                config.Sitename = config.Sitename ?? defaultSitename;
                config.RegistrationCode = config.RegistrationCode ?? RandomString(6);
                config.BaseUrl = config.BaseUrl ?? "http://replace.me";
            }

            DocumentStoreContainer.CurrentRequestSession.Store(config);
            DocumentStoreContainer.CurrentRequestSession.SaveChanges();
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
