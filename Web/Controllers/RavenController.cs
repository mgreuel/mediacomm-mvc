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

        protected List<SelectListItem> GetSelectListOfAllUsernames()
        {
            return this.userStorage.GetAllUsernames().Select(u => new SelectListItem { Text = u, Value = u }).ToList();
        }
    }
}