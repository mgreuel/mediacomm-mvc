using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Forum;
using MediaCommMvc.Web.Forum.ViewModels;
using MediaCommMvc.Web.Helpers;
using MediaCommMvc.Web.Infrastructure;

using PagedList;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class ForumController : RavenController
    {
        private readonly ForumStorageReader forumStorageReader;

        private readonly ForumStorageWriter forumStorageWriter;

        public ForumController(UserStorage userStorage, ForumStorageReader forumStorageReader, ForumStorageWriter forumStorageWriter)
            : base(userStorage)
        {
            this.forumStorageReader = forumStorageReader;
            this.forumStorageWriter = forumStorageWriter;
        }

        [HttpPost]
        public virtual ActionResult AddApproval(string topicId, int postIndex)
        {
            this.forumStorageWriter.AddApproval(topicId, postIndex, this.User.Identity.Name);

            return new EmptyResult();
        }

        [HttpPost]
        public virtual ActionResult AnswerPoll(PollUserAnswerInput answer)
        {
            this.forumStorageWriter.SavePollAnswer(answer, this.User.Identity.Name);

            TopicPageRoutedata topicPage = this.forumStorageReader.GetRouteDataForLastTopicPage(answer.TopicId);

            return this.RedirectToPost(topicPage);
        }

        public virtual ActionResult Index(int page)
        {
            ForumOverview forumOverview = this.forumStorageReader.GetForumOverview(page, ForumOptions.TopicsPerPage, this.User.Identity.Name);

            StaticPagedList<TopicOverviewViewModel> topics = new StaticPagedList<TopicOverviewViewModel>(
                forumOverview.TopicsForCurrentPage,
                page,
                ForumOptions.TopicsPerPage,
                forumOverview.TotalNumberOfTopics);

            return this.View(new ForumViewModel { Topics = topics });
        }

        public virtual ActionResult CreateTopic()
        {
            IEnumerable<SelectListItem> allUsers = this.GetSelectListOfAllUsernames();

            return this.View(MVC.Forum.Views.EditTopic, new EditTopicViewModel { AllUserNames = allUsers });
        }

        [HttpPost]
        public virtual ActionResult EditTopic(EditTopicViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel.AllUserNames = this.GetSelectListOfAllUsernames();
                return this.View(MVC.Forum.Views.EditTopic, viewModel);
            }

            string topicId = this.forumStorageWriter.SaveTopic(viewModel, this.User.Identity.Name);

            return this.RedirectToAction(MVC.Forum.Topic().AddRouteValues(new { id = topicId, name = viewModel.Title }));
        }

        public virtual ActionResult EditPost(string topicId, int postIndex)
        {
            EditPostViewModel viewModel = this.forumStorageReader.GetEditPostViewModel(topicId, postIndex);
            return this.View(viewModel);
        }

        public virtual ActionResult EditTopic(string id)
        {
            EditTopicViewModel viewModel = this.forumStorageReader.GetEditTopicViewModel(id);
            viewModel.AllUserNames = this.GetSelectListOfAllUsernames();
            return this.View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult EditPost(EditPostViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            this.forumStorageWriter.UpdatePost(viewModel);

            TopicPageRoutedata topicPage = this.forumStorageReader.GetRouteDataForPost(viewModel.TopicId, viewModel.PostIndex);

            return this.RedirectToPost(topicPage);
        }

        private ActionResult RedirectToPost(TopicPageRoutedata topicPage)
        {
            string url = this.Url.Action(MVC.Forum.Topic().AddRouteValues(new { id = topicPage.TopicId, name = UrlEncoder.ToFriendlyUrl(topicPage.TopicTitle), page = topicPage.PageNumber }));

            if (topicPage.PostIndex != null)
            {
                url += $"#{topicPage.PostIndex}";
            }

            return this.Redirect(url);
        }

        public virtual ActionResult FirstNewPostInTopic(string topicId)
        {
            TopicPageRoutedata topicPage = this.forumStorageReader.GetRouteDataForFirstNewPost(topicId, this.User.Identity.Name);

            return this.RedirectToPost(topicPage);
        }

        [HttpPost]
        public virtual ActionResult Reply(ReplyViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.forumStorageWriter.AddReply(viewModel, this.User.Identity.Name);
            }

            TopicPageRoutedata topicPage = this.forumStorageReader.GetRouteDataForLastTopicPage(viewModel.TopicId);

            return this.RedirectToPost(topicPage);
        }

        public virtual ActionResult Topic(string id, int page)
        {
            TopicDetailsViewModel topicDetailsViewModel = this.forumStorageReader.GetTopicDetailsViewModel(id, page, this.User.Identity.Name);

            return this.View(topicDetailsViewModel);
        }
        
        [HttpPost]
        public virtual ActionResult MarkTopicAsRead(string id)
        {
            this.forumStorageWriter.MarkTopicAsRead(id, this.User.Identity.Name);

            return new EmptyResult();
        }
    }
}