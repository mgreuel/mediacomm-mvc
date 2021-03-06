using System.Net;
using System.Web.Hosting;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Infrastructure;

using Microsoft.Owin.Security;

using Raven.Client;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MediaCommMvc.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MediaCommMvc.Web.App_Start.NinjectWebCommon), "Stop")]

namespace MediaCommMvc.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDocumentSession>().ToMethod(context => DocumentStoreContainer.CurrentRequestSession);

            kernel.Bind<Config>().ToMethod(context => DocumentStoreContainer.CurrentRequestSession.Load<Config>(Config.ConfigId));

            kernel.Bind<IAuthenticationManager>().ToMethod(context => HttpContext.Current.GetOwinContext().Authentication);

            kernel.Bind<Func<UserStorage>>().ToMethod(context => () => context.Kernel.Get<UserStorage>());

            // the nlog instance is static anyway
            kernel.Bind<ILogger>().To<NLogLogger>().InSingletonScope();
        }        
    }
}
