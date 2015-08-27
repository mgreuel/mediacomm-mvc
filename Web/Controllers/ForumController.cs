using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Models.Forum.Models;
using MediaCommMvc.Web.ViewModels;
using MediaCommMvc.Web.ViewModels.Forum;

using Microsoft.AspNet.Identity;

using PagedList;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class ForumController : Controller
    {
        private readonly EfForumStorageService efForumStorageService;

        public ForumController(EfForumStorageService efForumStorageService)
        {
            this.efForumStorageService = efForumStorageService;
        }

        [HttpPost]
        public virtual ActionResult AddApproval(int postId)
        {
            this.efForumStorageService.AddApproval(postId, this.User.Identity.GetUserName());

            return new EmptyResult();
        }

        [HttpPost]
        public virtual ActionResult AnswerPoll(PollUserAnswerInput answer)
        {
            this.efForumStorageService.SavePollAnswer(answer);

            return new EmptyResult();
        }

        public virtual ActionResult Index(int page)
        {
            ForumOverview forumOverview = this.efForumStorageService.GetForumOverview(page, ForumOptions.TopicsPerPage, this.User.Identity.GetUserName());

            StaticPagedList<TopicOverviewViewModel> topics = new StaticPagedList<TopicOverviewViewModel>(
                forumOverview.TopicsForCurrentPage,
                page,
                ForumOptions.TopicsPerPage,
                forumOverview.TotalNumberOfTopics);

            return this.View(new ForumViewModel { Topics = topics });
        }

        public virtual ActionResult CreateTopic()
        {
            IEnumerable<SelectListItem> allUsers = this.efForumStorageService.GetAllUserNames().Select(
                u => new SelectListItem { Text = u, Value = u }).ToList();

            return this.View(MVC.Forum.Views.EditTopic, new EditTopicWebViewModel { AllUserNames = allUsers });
        }

        [HttpPost]
        public virtual ActionResult EditTopic(EditTopicWebViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                IEnumerable<SelectListItem> allUsers = this.efForumStorageService.GetAllUserNames().Select(
                u => new SelectListItem { Text = u, Value = u }).ToList();

                viewModel.AllUserNames = allUsers;
                return this.View(MVC.Forum.Views.EditTopic, viewModel);
            }

            int topicId;

            if (viewModel.Id == 0)
            {
                topicId = this.efForumStorageService.AddTopic(viewModel.ToCreateTopicCommand(this.User.Identity.GetUserName()));
            }
            else
            {
                this.efForumStorageService.UpdateTopic(viewModel.ToUpdateTopicCommand());
                topicId = viewModel.Id;
            }

            return this.RedirectToAction(MVC.Forum.Topic().AddRouteValues(new { id = topicId, name = viewModel.Subject }));
        }


        public virtual ActionResult EditPost(int id)
        {
            EditPostViewModel viewModel = this.efForumStorageService.GetEditPostViewModel(id);
            return this.View(viewModel);
        }

        public virtual ActionResult EditTopic(int id)
        {
            EditTopicWebViewModel viewModel = this.efForumStorageService.GetEditTopicViewModel(id);
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
            TopicPageRoutedata topicPage = this.efForumStorageService.GetTopicPageRouteDataForPost(viewModel.PostId, ForumOptions.PostsPerPage);

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

            TopicPageRoutedata topicPage = this.efForumStorageService.GetRouteDataForLastTopicPage(viewModel.TopicId, ForumOptions.PostsPerPage);

            return
                this.RedirectToAction(
                        MVC.Forum.Topic().AddRouteValues(new { id = topicPage.TopicId, name = topicPage.TopicTitle, page = topicPage.PageNumber }));
        }

        public virtual ActionResult Topic(int id, int page)
        {
            TopicDetailsViewModel topicDetails = this.efForumStorageService.GetTopicDetailsViewModelAndMarkTopicAsRead(id, page, ForumOptions.PostsPerPage, this.User);
            var viewModel = new PagedTopicDetailsViewModel(topicDetails);

            return this.View(viewModel);
        }
    }
}