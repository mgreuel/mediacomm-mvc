using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Infrastructure;

using Raven.Client;

namespace MediaCommMvc.Web.Controllers
{
    public abstract partial class RavenController : Controller
    {
        private readonly UserStorage userStorage;

        public RavenController(UserStorage userStorage, Config config)
        {
            this.userStorage = userStorage;
            this.Config = config;
        }

        private IDocumentSession RavenSession { get; set; }

        protected Config Config { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            this.RavenSession = DocumentStoreContainer.CurrentSession;

            if (filterContext.IsChildAction)
            {
                return;
            }

            this.ViewBag.Sitename = this.Config.Sitename;
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

        protected void SaveUserVisit()
        {
            var user = this.userStorage.GetUser(this.User.Identity.Name);

            if (user == null)
            {
                // can happen directly after registation, when the index wasn't updated yet
                return;
            }

            user.LastVisit = DateTime.UtcNow;
            this.userStorage.SaveUser(user);
        }
    }
}