using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;

using MediaCommMvc.Web.Infrastructure.Database;
using MediaCommMvc.Web.Models.Forum.Commands;
using MediaCommMvc.Web.Models.Forum.Models;
using MediaCommMvc.Web.ViewModels;
using MediaCommMvc.Web.ViewModels.Forum;

using Microsoft.AspNet.Identity;

namespace MediaCommMvc.Web.Infrastructure
{
    public class EfForumStorageService
    {
        private readonly ApplicationDbContext databaseContext;

        public EfForumStorageService(ApplicationDbContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public int AddTopic(CreateTopicCommand createTopicCommand)
        {
            Topic topic = createTopicCommand.ToTopic();
            this.databaseContext.Topics.Add(topic);
            this.databaseContext.SaveChanges();

            return topic.TopicId;
        }

        public ForumOverview GetForumOverview(int page, int topicsPerPage, string currentUser)
        {
            ForumOverview forumOverview = new ForumOverview
                                              {
                                                  TopicsForCurrentPage =
                                                      this.databaseContext.Topics
                                                      .Where(t => !t.ExcludedUsersStorage.Contains(currentUser))
                                                      .OrderByDescending(topic => topic.LastPostTime)
                                                      .Skip((page - 1) * topicsPerPage)
                                                      .Take(topicsPerPage)
                                                      .ToList()
                                                      .Select(topic => new TopicOverviewViewModel(topic, currentUser))
                                                      .ToList(),
                                                  TotalNumberOfTopics = this.databaseContext.Topics.Count()
                                              };

            return forumOverview;
        }

        public TopicDetailsViewModel GetTopicDetailsViewModelAndMarkTopicAsRead(int id, int page, int postsPerPage, IPrincipal currentUser)
        {
            Topic topic = this.databaseContext.Topics
                .Include(t => t.Posts)
                .Single(details => details.TopicId == id);

            topic.MarkTopicAsRead(currentUser.Identity.GetUserName());
            this.databaseContext.SaveChanges();

            return new TopicDetailsViewModel(topic, page, postsPerPage, currentUser);
        }

        public void AddReply(AddReplyCommand addReplyCommand)
        {
            Post post = new Post
                            {
                                AuthorName = addReplyCommand.AuthorName,
                                Created = addReplyCommand.Created,
                                TopicId = addReplyCommand.TopicId,
                                Text = addReplyCommand.Text
                            };

            using (DbContextTransaction transaction = this.databaseContext.Database.BeginTransaction())
            {
                Topic topic = this.databaseContext.Topics.Single(overview => overview.TopicId == addReplyCommand.TopicId);

                post.IndexInTopic = topic.PostCount;

                topic.PostCount = topic.PostCount + 1;
                topic.LastPostAuthor = addReplyCommand.AuthorName;
                topic.LastPostTime = addReplyCommand.Created;

                this.databaseContext.SaveChanges();
                transaction.Commit();
            }

            this.databaseContext.Posts.Add(post);
            this.databaseContext.SaveChanges();
        }

        public EditPostViewModel GetEditPostViewModel(int id)
        {
            Post post = this.databaseContext.Posts.Single(p => p.Id == id);
            return new EditPostViewModel(post);
        }

        public void UpdatePost(UpdatePostCommand updatePostCommand)
        {
            Post post = this.databaseContext.Posts.Single(p => p.Id == updatePostCommand.PostId);
            post.Text = updatePostCommand.Text;
            this.databaseContext.SaveChanges();
        }

        public TopicPageRoutedata GetTopicPageRouteDataForPost(int postId, int postsPerTopic)
        {
            Post post = this.databaseContext.Posts.Include(p => p.Topic).Single(p => p.Id == postId);
            return TopicPageRoutedata.FromPost(post, postsPerTopic);
        }

        public TopicPageRoutedata GetRouteDataForLastTopicPage(int topicId, int postsPerPage)
        {
            Topic topic = this.databaseContext.Topics.Single(t => t.TopicId == topicId);
            return TopicPageRoutedata.LastPageOfTopic(topic, postsPerPage);
        }

        public void AddApproval(int postId, string userName)
        {
            Post post = this.databaseContext.Posts.Single(p => p.Id == postId);
            post.AddApproval(userName);
            this.databaseContext.SaveChanges();
        }

        public EditTopicWebViewModel GetEditTopicViewModel(int id)
        {
            Topic topic = this.databaseContext.Topics.Single(t => t.TopicId == id);
            IEnumerable<SelectListItem> allUserNames =
                this.databaseContext.Users.ToList().Select(
                    u =>
                    new SelectListItem
                        {
                            Text = u.UserName,
                            Value = u.UserName,
                            Selected = topic.ExcludedUserNames.Contains(u.UserName, StringComparer.OrdinalIgnoreCase)
                        });

            return new EditTopicWebViewModel
                       {
                           AllUserNames = allUserNames,
                           ExcludedUserNames = topic.ExcludedUserNames,
                           Subject = topic.Title,
                           Text = topic.Posts.First().Text,
                           Poll = new CreatePollViewModel(topic.Poll)
                       };
        }

        public void UpdateTopic(UpdateTopicCommand toUpdateTopicCommand)
        {
            Topic topic = this.databaseContext.Topics.Single(t => t.TopicId == toUpdateTopicCommand.Id);
            topic.Title = toUpdateTopicCommand.Title;
            topic.Posts.First().Text = toUpdateTopicCommand.Text;
            topic.ExcludedUserNames = toUpdateTopicCommand.ExcludedUserNames;
            this.databaseContext.SaveChanges();
        }

        public IList<string> GetAllUserNames()
        {
            return this.databaseContext.Users.Select(u => u.UserName).ToList();
        }

        public void SavePollAnswer(PollUserAnswerInput userAnswer)
        {
            Topic topic = this.databaseContext.Topics.Single(t => t.TopicId == userAnswer.TopicId);

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

            this.databaseContext.SaveChanges();
        }
    }
}