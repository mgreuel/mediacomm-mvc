using System;
using System.Web.Mvc;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Infrastructure;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class HomeController : RavenController
    {

        public HomeController(UserStorage userStorage, Config config)
            : base(userStorage, config)
        {
        }

        public virtual ActionResult Index()
        {
            this.SaveUserVisit();

            return this.View();
        }

        public virtual ActionResult TestError()
        {
            throw new Exception("test error");
        }
    }
}