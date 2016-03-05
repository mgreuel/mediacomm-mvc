using System.Collections.Generic;
using System.Web.Mvc;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Forum;
using MediaCommMvc.Web.Features.Forum.Notifications;
using MediaCommMvc.Web.Features.Forum.ViewModels;
using MediaCommMvc.Web.Helpers;
using MediaCommMvc.Web.Infrastructure;

using PagedList;

namespace MediaCommMvc.Web.Controllers
{
    [RoutePrefix("forum")]
    [Route("{action=index}")]
    [Authorize]
    public partial class ForumController : RavenController
    {
        private readonly ForumStorageReader forumStorageReader;

        private readonly ForumStorageWriter forumStorageWriter;

        private readonly ForumNotificationSender forumNotificationSender;

        public ForumController(UserStorage userStorage, 
            ForumStorageReader forumStorageReader, 
            ForumStorageWriter forumStorageWriter, 
            Config config, 
            ForumNotificationSender forumNotificationSender)
            : base(userStorage, config)
        {
            this.forumStorageReader = forumStorageReader;
            this.forumStorageWriter = forumStorageWriter;
            this.forumNotificationSender = forumNotificationSender;
        }

        [HttpPost]
        [Route("AddApproval")]
        public virtual ActionResult AddApproval(string topicId, int postIndex)
        {
            this.forumStorageWriter.AddApproval(topicId, postIndex, this.User.Identity.Name);

            return new EmptyResult();
        }

        [HttpPost]
        [Route("AnswerPoll")]
        public virtual ActionResult AnswerPoll(PollUserAnswerInput answer)
        {
            this.forumStorageWriter.SavePollAnswer(answer, this.User.Identity.Name);

            TopicPageRoutedata topicPage = this.forumStorageReader.GetRouteDataForLastTopicPage(answer.TopicId);

            return this.RedirectToPost(topicPage);
        }

        [Route("{forumPage=1}")]
        public virtual ActionResult Index(int forumPage)
        {
            this.SaveUserVisit();
            ForumOverview forumOverview = this.forumStorageReader.GetForumOverview(forumPage, ForumOptions.TopicsPerPage, this.User.Identity.Name);

            StaticPagedList<TopicOverviewViewModel> topics = new StaticPagedList<TopicOverviewViewModel>(
                forumOverview.TopicsForCurrentPage,
                forumPage,
                ForumOptions.TopicsPerPage,
                forumOverview.TotalNumberOfTopics);

            return this.View(new ForumViewModel { Topics = topics });
        }

        [Route("CreateTopic")]
        public virtual ActionResult CreateTopic()
        {
            IList<SelectListItem> allUsers = this.GetSelectListOfAllUsernames();

            return this.View(MVC.Forum.Views.EditTopic, new EditTopicViewModel { AllUserNames = allUsers });
        }

        [Route("EditTopic/{id?}")]
        [HttpPost]
        public virtual ActionResult EditTopic(EditTopicViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel.AllUserNames = this.GetSelectListOfAllUsernames();
                return this.View(MVC.Forum.Views.EditTopic, viewModel);
            }

            string topicId = this.forumStorageWriter.SaveTopic(viewModel, this.User.Identity.Name);

            if (string.IsNullOrEmpty(viewModel.Id))
            {
                this.forumNotificationSender.SendNewTopicNotifications(topicId);
            }

            return this.RedirectToAction(MVC.Forum.Topic().AddRouteValues(new { id = topicId, name = UrlEncoder.ToFriendlyUrl(viewModel.Title) }));
        }

        [Route("EditPost/{topicId}/{postIndex}")]
        public virtual ActionResult EditPost(string topicId, int postIndex)
        {
            EditPostViewModel viewModel = this.forumStorageReader.GetEditPostViewModel(topicId, postIndex, this.User.Identity.Name);
            return this.View(viewModel);
        }

        [Route("EditTopic/{id}")]
        public virtual ActionResult EditTopic(string id)
        {
            EditTopicViewModel viewModel = this.forumStorageReader.GetEditTopicViewModel(id, this.User.Identity.Name);
            viewModel.AllUserNames = this.GetSelectListOfAllUsernames();
            return this.View(viewModel);
        }

        [Route("EditPost/{topicId}/{postIndex}")]
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
            string url = this.Url.Action(MVC.Forum.Topic().AddRouteValues(new { id = topicPage.TopicId, name = UrlEncoder.ToFriendlyUrl(topicPage.TopicTitle), topicPage = topicPage.PageNumber }));

            url += $"#{topicPage.PostIndex}";

            return this.Redirect(url);
        }

        [Route("FirstNewPost/{topicId}")]
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

            this.forumNotificationSender.SendNewReplyNotifications(viewModel.TopicId, topicPage.PostIndex);

            return this.RedirectToPost(topicPage);
        }

        [Route("{id}/{name}/{topicPage=1}")]
        public virtual ActionResult Topic(string id, int topicPage)
        {
            TopicDetailsViewModel topicDetailsViewModel = this.forumStorageReader.GetTopicDetailsViewModel(id, topicPage, this.User.Identity.Name);

            return this.View(topicDetailsViewModel);
        }
        
        [HttpPost]
        [Route("MarkTopicAsRead")]
        public virtual ActionResult MarkTopicAsRead(string id)
        {
            this.forumStorageWriter.MarkTopicAsRead(id, this.User.Identity.Name);

            return new EmptyResult();
        }
    }
}