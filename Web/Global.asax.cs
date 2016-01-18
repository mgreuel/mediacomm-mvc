using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using MediaCommMvc.Web.Infrastructure;

namespace MediaCommMvc.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
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

            FluentScheduler.TaskManager.UnobservedTaskException += (taskExceptionInformation, _) => Elmah.ErrorSignal.Get(null).Raise(taskExceptionInformation.Task.Exception);
        }

        private void EnsureConfig()
        {
            Config config = DocumentStoreContainer.CurrentRequestSession.Load<Config>(Config.ConfigId);
            config = SetupWebsiteConfig(config);

            MailConfig mailConfig = DocumentStoreContainer.CurrentRequestSession.Load<MailConfig>(MailConfig.MailConfigId);
            mailConfig = SetupMailConfig(mailConfig);

            DocumentStoreContainer.CurrentRequestSession.Store(mailConfig);
            DocumentStoreContainer.CurrentRequestSession.Store(config);
            DocumentStoreContainer.CurrentRequestSession.SaveChanges();
        }

        private static MailConfig SetupMailConfig(MailConfig mailConfig)
        {
            if (mailConfig == null)
            {
                mailConfig = new MailConfig { MailFrom = "na@local.host", Username = "dev", Password = "dev1", SmtpHost = "localhost" };
            }

            return mailConfig;
        }

        private static Config SetupWebsiteConfig(Config config)
        {
            string defaultPhotoPath = @"C:\temp\Absolutmoments\Photos";
            string defaultSitename = "Absolutmoments";

            if (config == null)
            {
                config = new Config
                {
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

            return config;
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
