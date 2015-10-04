using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Infrastructure;

using Raven.Client;


namespace MediaCommMvc.Web.Controllers
{
    public abstract partial class RavenController : Controller
    {
        private readonly UserStorage userStorage;

        private IDocumentSession RavenSession { get; set; }

        public RavenController(UserStorage userStorage)
        {
            this.userStorage = userStorage;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            this.RavenSession = DocumentStoreContainer.CurrentSession;

            if (filterContext.IsChildAction)
            {
                return;
            }

            this.Config = this.RavenSession.Load<Config>(MvcApplication.ConfigId);
            this.ViewBag.Config = this.Config;
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

        protected List<SelectListItem> GetSelectListOfAllUsernames()
        {
            return this.userStorage.GetAllUsernames().Select(u => new SelectListItem { Text = u, Value = u }).ToList();
        }

        protected Config Config { get; private set; }
    }
}