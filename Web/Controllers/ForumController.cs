using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Forum;
using MediaCommMvc.Web.Forum.ViewModels;
using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Models.Forum.Commands;
using MediaCommMvc.Web.Models.Forum.Models;

using Microsoft.AspNet.Identity;

using PagedList;

using Raven.Client;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class ForumController : RavenController
    {
        private readonly ForumStorageReader forumStorageReader;

        private readonly ForumStorageWriter forumStorageWriter;

        private readonly UserStorage userStorage;

        public ForumController(UserStorage userStorage, ForumStorageReader forumStorageReader, ForumStorageWriter forumStorageWriter)
            : base(userStorage)
        {
            this.userStorage = userStorage;
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
            this.forumStorageWriter.SavePollAnswer(answer);

            return new EmptyResult();
        }

        public virtual ActionResult Index(int page)
        {
            ForumOverview forumOverview = this.GetForumOverview(page, ForumOptions.TopicsPerPage, this.User.Identity.Name);

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

            //this.UpdatePost(viewModel.ToSavePostCommand());
            TopicPageRoutedata topicPage = null; //this.GetTopicPageRouteDataForPost(viewModel.PostId, ForumOptions.PostsPerPage);

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

            this.AddReply(viewModel.ToAddReplyCommand(this.User.Identity.Name));

            TopicPageRoutedata topicPage = null; // this.GetRouteDataForLastTopicPage(viewModel.TopicId, ForumOptions.PostsPerPage);

            return
                this.RedirectToAction(
                    MVC.Forum.Topic().AddRouteValues(new { id = topicPage.TopicId, name = topicPage.TopicTitle, page = topicPage.PageNumber }));
        }

        public virtual ActionResult Topic(string id, int page)
        {
            TopicDetailsViewModel topicDetailsViewModel = this.forumStorageReader.GetTopicDetailsViewModel(id, page, this.User.Identity.Name);

            // todo: mark as read on last page loading

            return this.View(topicDetailsViewModel);
        }

        public ForumOverview GetForumOverview(int page, int topicsPerPage, string currentUsername)
        {
            RavenQueryStatistics stats;

            ForumOverview forumOverview = new ForumOverview
            {
                TopicsForCurrentPage =
                    this.RavenSession.Query<Topic>()
                        .Statistics(out stats)
                        .Where(t => !t.ExcludedUserNames.Contains(currentUsername))
                        .OrderByDescending(topic => topic.LastPostTime)
                        .Skip((page - 1) * topicsPerPage)
                        .Take(topicsPerPage)
                        // todo move the transformation to raven db
                        .ToList()
                        .Select(topic => new TopicOverviewViewModel(topic, currentUsername))
                        .ToList(),
                TotalNumberOfTopics = stats.TotalResults
            };

            return forumOverview;
        }

        [HttpPost]
        public virtual ActionResult MarkTopicAsRead(string id)
        {
            this.forumStorageWriter.MarkTopicAsRead(id, this.User.Identity.Name);

            return new EmptyResult();
        }

        public void AddReply(AddReplyCommand addReplyCommand)
        {
            //Post post = new Post
            //{
            //    AuthorName = addReplyCommand.AuthorName,
            //    Created = addReplyCommand.Created,
            //    TopicId = addReplyCommand.TopicId,
            //    Text = addReplyCommand.Text
            //};

            //using (DbContextTransaction transaction = this.databaseContext.Database.BeginTransaction())
            //{
            //    Topic topic = this.databaseContext.Topics.Single(overview => overview.TopicId == addReplyCommand.TopicId);

            //    post.IndexInTopic = topic.PostCount;

            //    topic.PostCount = topic.PostCount + 1;
            //    topic.LastPostAuthor = addReplyCommand.AuthorName;
            //    topic.LastPostTime = addReplyCommand.Created;

            //    this.databaseContext.SaveChanges();
            //    transaction.Commit();
            //}

            //this.databaseContext.Posts.Add(post);
            //this.databaseContext.SaveChanges();
        }
    }
}