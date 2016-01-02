using System;
using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Forum;
using MediaCommMvc.Web.Features.Forum.ViewModels;
using MediaCommMvc.Web.Features.Home;
using MediaCommMvc.Web.Features.Photos;
using MediaCommMvc.Web.Features.Photos.ViewModels;
using MediaCommMvc.Web.Infrastructure;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class HomeController : RavenController
    {
        private readonly ForumStorageReader forumStorageReader;

        private readonly PhotoMetaDataStorage photoMetaDataStorage;

        public HomeController(UserStorage userStorage, Config config, ForumStorageReader forumStorageReader, PhotoMetaDataStorage photoMetaDataStorage)
            : base(userStorage, config)
        {
            this.forumStorageReader = forumStorageReader;
            this.photoMetaDataStorage = photoMetaDataStorage;
        }

        public virtual ActionResult Index()
        {
            this.SaveUserVisit();

            ForumOverview forumOverview = this.forumStorageReader.GetForumOverview(page: 1, topicsPerPage: 10, currentUsername: this.User.Identity.Name);

            IList<PhotoAlbumItemViewModel> newestAlbums = this.photoMetaDataStorage.GetNewestAlbums(count: 6);

            var viewModel = new HomeViewModel { ForumOverview = forumOverview, PhotosAlbums = newestAlbums };

            return this.View(viewModel);
        }

        public virtual ActionResult TestError()
        {
            throw new Exception("test error");
        }
    }
}