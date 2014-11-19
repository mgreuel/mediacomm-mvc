using System.Web.Mvc;

using Core.Forum.Models;
using Core.Forum.ViewModels;

using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.ViewModels.Forum;

using Microsoft.AspNet.Identity;

using PagedList;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class ForumController : Controller
    {
        private const int PostsPerPage = 15;

        private const int TopicsPerPage = 15;

        private readonly EfForumStorageService efForumStorageService;

        public ForumController(EfForumStorageService efForumStorageService)
        {
            this.efForumStorageService = efForumStorageService;
        }

        public virtual ActionResult Index(int page)
        {
            ForumOverview forumOverview = this.efForumStorageService.GetForumOverview(page, TopicsPerPage, this.User.Identity.GetUserName());

            StaticPagedList<TopicOverviewViewModel> topics = new StaticPagedList<TopicOverviewViewModel>(
                forumOverview.TopicsForCurrentPage, 
                page, 
                TopicsPerPage, 
                forumOverview.TotalNumberOfTopics);

            return this.View(new ForumViewModel { Topics = topics });
        }

        public virtual ActionResult CreateTopic()
        {
            return this.View(MVC.Forum.Views.EditTopic, new EditTopicWebViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreateTopic(EditTopicWebViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(MVC.Forum.Views.EditTopic, viewModel);
            }

            int topicId = this.efForumStorageService.AddTopic(viewModel.ToCreateTopicCommand(this.User.Identity.GetUserName()));

            return this.RedirectToAction(MVC.Forum.Topic().AddRouteValue("id", topicId));
        }


        public virtual ActionResult EditPost(int id)
        {
            // todo: Add edit topic (decision on the client)
            EditPostViewModel viewModel = this.efForumStorageService.GetEditPostViewModel(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult EditPost(EditPostViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            this.efForumStorageService.UpdatePost(viewModel.ToSavePostCommand());
            TopicPageRoutedata topicPage = this.efForumStorageService.GetTopicPageRouteDataForPost(viewModel.PostId, PostsPerPage);

            return
                this.RedirectToAction(
                        MVC.Forum.Topic().AddRouteValues(new { id = topicPage.TopicId, name = topicPage.TopicTitle, page = topicPage.PageNumber }));
        }

        [HttpPost]
        public virtual ActionResult Reply(ReplyViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            this.efForumStorageService.AddReply(viewModel.ToAddReplyCommand(this.User.Identity.GetUserName()));

            TopicPageRoutedata topicPage = this.efForumStorageService.GetRouteDataForLastTopicpage(viewModel.TopicId, PostsPerPage);

            return
                this.RedirectToAction(
                        MVC.Forum.Topic().AddRouteValues(new { id = topicPage.TopicId, name = topicPage.TopicTitle, page = topicPage.PageNumber }));
        }

        public virtual ActionResult Topic(int id, int page)
        {
            TopicDetailsViewModel topicDetails = this.efForumStorageService.GetTopicDetailsViewModel(id, page, PostsPerPage, this.User);
            var viewModel = new PagedTopicDetailsViewModel(topicDetails);

            return this.View(viewModel);
        }
    }
}