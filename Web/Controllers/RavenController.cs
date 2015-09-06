using System.Web.Mvc;

using MediaCommMvc.Web.Infrastructure;

using Raven.Client;

namespace MediaCommMvc.Web.Controllers
{
    public abstract partial class RavenController : Controller
    {
        public IDocumentSession RavenSession { get; protected set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.RavenSession = DocumentStoreContainer.CurrentSession;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            using (this.RavenSession)
            {
                if (filterContext.Exception != null)
                {
                    return;
                }

                this.RavenSession?.SaveChanges();
            }
        }
    }
}