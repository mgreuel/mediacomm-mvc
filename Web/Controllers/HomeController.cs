using System;
using System.Web.Mvc;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Forum;
using MediaCommMvc.Web.Features.Forum.ViewModels;
using MediaCommMvc.Web.Features.Home;
using MediaCommMvc.Web.Infrastructure;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class HomeController : RavenController
    {
        private readonly ForumStorageReader forumStorageReader;

        public HomeController(UserStorage userStorage, Config config, ForumStorageReader forumStorageReader)
            : base(userStorage, config)
        {
            this.forumStorageReader = forumStorageReader;
        }

        public virtual ActionResult Index()
        {
            this.SaveUserVisit();

            ForumOverview forumOverview = this.forumStorageReader.GetForumOverview(page: 1, topicsPerPage: 10, currentUsername: this.User.Identity.Name);

            var viewModel = new HomeViewModel { ForumOverview = forumOverview };

            return this.View(viewModel);
        }

        public virtual ActionResult TestError()
        {
            throw new Exception("test error");
        }
    }
}