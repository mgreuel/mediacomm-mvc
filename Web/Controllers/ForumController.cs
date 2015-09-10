using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web.Mvc;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Forum;
using MediaCommMvc.Web.Forum.ViewModels;
using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Models.Forum.Commands;
using MediaCommMvc.Web.Models.Forum.Models;
using MediaCommMvc.Web.ViewModels;

using PagedList;

using Raven.Client;
using Raven.Client.Indexes;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class ForumController : RavenController
    {
        private readonly UserStorage userStorage;

        private readonly ForumStorage forumStorage;

        public ForumController(UserStorage userStorage, ForumStorage forumStorage) : base(userStorage)
        {
            this.userStorage = userStorage;
            this.forumStorage = forumStorage;
        }

        [HttpPost]
        public virtual ActionResult AddApproval(int postId)
        {
            //this.AddApproval(postId, this.User.Identity.GetUserName());

            return new EmptyResult();
        }

        [HttpPost]
        public virtual ActionResult AnswerPoll(PollUserAnswerInput answer)
        {
            this.SavePollAnswer(answer);

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

            return this.View(MVC.Forum.Views.EditTopic, new EditTopicWebViewModel { AllUserNames = allUsers });
        }

        [HttpPost]
        public virtual ActionResult EditTopic(EditTopicWebViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel.AllUserNames = this.GetSelectListOfAllUsernames();
                return this.View(MVC.Forum.Views.EditTopic, viewModel);
            }

            int topicId;

            if (viewModel.Id == 0)
            {
                topicId = this.AddTopic(viewModel.ToCreateTopicCommand(this.User.Identity.Name));
            }
            else
            {
                //this.UpdateTopic(viewModel.ToUpdateTopicCommand());
                topicId = viewModel.Id;
            }

            return this.RedirectToAction(MVC.Forum.Topic().AddRouteValues(new { id = topicId, name = viewModel.Subject }));
        }


        public virtual ActionResult EditPost(int id)
        {
            EditPostViewModel viewModel = this.GetEditPostViewModel(id);
            return this.View(viewModel);
        }

        public virtual ActionResult EditTopic(int id)
        {
            EditTopicWebViewModel viewModel = null;//this.GetEditTopicViewModel(id);
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
            TopicPageRoutedata topicPage = null;//this.GetTopicPageRouteDataForPost(viewModel.PostId, ForumOptions.PostsPerPage);

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

        public virtual ActionResult Topic(int id, int page)
        {
            TopicDetailsViewModel topicDetailsViewModel = this.forumStorage.GetTopicDetailsViewModel(id, page, this.User.Identity.Name);

            // todo: mark as read on last page loading

            return this.View(topicDetailsViewModel);
        }

        public void SavePollAnswer(PollUserAnswerInput userAnswer)
        {

            Topic topic = null;//this.RavenSession.Query<Topic>().Single(t => t.Id == userAnswer.TopicId);

            int index = 0;

            // if the ordering is changed, make sure to also change it in the view model
            foreach (PollAnswer answer in topic.Poll.Answers.OrderBy(a => a.Text))
            {
                // todo: Check whether this should be moved to the model
                bool newAnswerValue = userAnswer.CheckedAnswers.Contains(index);

                if (newAnswerValue && !answer.Usernames.Contains(userAnswer.Username))
                {
                    answer.Usernames.Add(userAnswer.Username);
                }
                else if (!newAnswerValue && answer.Usernames.Contains(userAnswer.Username))
                {
                    answer.Usernames.Remove(userAnswer.Username);
                }

                index = index + 1;
            }
        }

        public int AddTopic(CreateTopicCommand createTopicCommand)
        {
            Topic topic = createTopicCommand.ToTopic();

            this.RavenSession.Store(topic);

            return topic.NumericId;
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

        public TopicDetailsViewModel GetTopicDetailsViewModelAndMarkTopicAsRead(int id, int page, int postsPerPage, IPrincipal currentUser)
        {
            //Topic topic = this.databaseContext.Topics
            //    .Include(t => t.Posts)
            //    .Single(details => details.TopicId == id);

            //topic.MarkTopicAsRead(currentUser.Identity.GetUserName());
            //this.databaseContext.SaveChanges();

            //return new TopicDetailsViewModel(topic, page, postsPerPage, currentUser);

            return null;
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

        public EditPostViewModel GetEditPostViewModel(int id)
        {
            //Post post = this.databaseContext.Posts.Single(p => p.Id == id);
            //return new EditPostViewModel(post);

            return null;
        }

        //public void UpdatePost(UpdatePostCommand updatePostCommand)
        //{
        //    Post post = this.databaseContext.Posts.Single(p => p.Id == updatePostCommand.PostId);
        //    post.Text = updatePostCommand.Text;
        //}

        //public TopicPageRoutedata GetTopicPageRouteDataForPost(int postId, int postsPerTopic)
        //{
        //    Post post = this.databaseContext.Posts.Include(p => p.Topic).Single(p => p.Id == postId);
        //    return TopicPageRoutedata.FromPost(post, postsPerTopic);
        //}

        //public TopicPageRoutedata GetRouteDataForLastTopicPage(int topicId, int postsPerPage)
        //{
        //    Topic topic = this.databaseContext.Topics.Single(t => t.TopicId == topicId);
        //    return TopicPageRoutedata.LastPageOfTopic(topic, postsPerPage);
        //}

        //public void AddApproval(int postId, string userName)
        //{
        //    Post post = this.databaseContext.Posts.Single(p => p.Id == postId);
        //    post.AddApproval(userName);
        //    this.databaseContext.SaveChanges();
        //}

        //public EditTopicWebViewModel GetEditTopicViewModel(int id)
        //{
        //    Topic topic = this.session.Query<Topic>().Single(t => t.TopicId == id);

        //    IEnumerable<SelectListItem> allUserNames =
        //        this.databaseContext.Users.ToList().Select(
        //            u =>
        //            new SelectListItem
        //            {
        //                Text = u.UserName,
        //                Value = u.UserName,
        //                Selected = topic.ExcludedUserNames.Contains(u.UserName, StringComparer.OrdinalIgnoreCase)
        //            });

        //    return new EditTopicWebViewModel
        //    {
        //        AllUserNames = allUserNames,
        //        ExcludedUserNames = topic.ExcludedUserNames,
        //        Subject = topic.Title,
        //        Text = topic.Posts.First().Text,
        //        Poll = new CreatePollViewModel(topic.Poll)
        //    };
        //}

        //public void UpdateTopic(UpdateTopicCommand toUpdateTopicCommand)
        //{
        //    Topic topic = this.session.Query<Topic>().Single(t => t.TopicId == toUpdateTopicCommand.Id);
        //    topic.Title = toUpdateTopicCommand.Title;
        //    topic.Posts.First().Text = toUpdateTopicCommand.Text;
        //    topic.ExcludedUserNames = toUpdateTopicCommand.ExcludedUserNames;
        //}

        //public IList<string> GetAllUserNames()
        //{

        //    //return this.databaseContext.Users.Select(u => u.UserName).ToList();
        //    return null;
        //}

    }
}