using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Forum.Models;
using MediaCommMvc.Web.Forum.ViewModels;
using MediaCommMvc.Web.Helpers;
using MediaCommMvc.Web.Infrastructure;

using Raven.Client;

namespace MediaCommMvc.Web.Forum
{
    public class ForumStorageReader
    {
        private readonly IDocumentSession ravenSession;

        public ForumStorageReader(IDocumentSession ravenSession)
        {
            this.ravenSession = ravenSession;
        }

        public TopicDetailsViewModel GetTopicDetailsViewModel(string id, int pageNumber, string currentUsername)
        {
            // todo move projections to ravendb
            Topic topic = this.ravenSession.Load<Topic>(id);
            var viewModel = new TopicDetailsViewModel
            {
                ExcludedUsernames = topic.ExcludedUserNames,
                Id = topic.Id,
                Title = topic.Title,
                PageNumber = pageNumber,
                Posts = topic.PostsInOrder
                    .Skip((pageNumber - 1) * ForumOptions.PostsPerPage)
                    .Take(ForumOptions.PostsPerPage)
                    .Select(post => new PostViewModel(post))
                    .ToList(),
                TotalNumberOfPosts = topic.PostCount
            };

            if (topic.Poll != null && topic.Poll.Answers.Any())
            {
                viewModel.Poll = new ShowPollViewModel(topic.Poll, currentUsername);
            }

            return viewModel;
        }

        public EditTopicViewModel GetEditTopicViewModel(string id)
        {
            // todo decide whetehr polls may be edited
            Topic topic = this.ravenSession.Load<Topic>(id);

            return new EditTopicViewModel
            {
                ExcludedUserNames = topic.ExcludedUserNames ?? new List<string>(),
                Title = topic.Title,
                Text = topic.PostsInOrder.First().Text
            };
        }

        public EditPostViewModel GetEditPostViewModel(string topicId, int postIndex)
        {
            // todo move projection to raven db
            Post post = this.ravenSession.Load<Topic>(topicId).Posts.Single(p => p.IndexInTopic == postIndex);
            return new EditPostViewModel { PostIndex = postIndex, Text = post.Text, TopicId = topicId };
        }

        public ForumOverview GetForumOverview(int page, int topicsPerPage, string currentUsername)
        {
            RavenQueryStatistics stats;

            ForumOverview forumOverview = new ForumOverview
            {
                TopicsForCurrentPage =
                    this.ravenSession.Query<Topic>()
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

        public TopicPageRoutedata GetRouteDataForLastTopicPage(string topicId)
        {
            var topic = this.ravenSession.Load<Topic>(topicId);

            return new TopicPageRoutedata
            {
                PageNumber = ((topic.PostCount - 1) / ForumOptions.PostsPerPage) + 1,
                TopicId = topic.Id,
                TopicTitle = topic.Title
            };
        }

        public TopicPageRoutedata GetRouteDataForPost(string topicId, int postIndex)
        {
            var topic = this.ravenSession.Load<Topic>(topicId);
            var post = topic.Posts.Single(p => p.IndexInTopic == postIndex);

            return new TopicPageRoutedata
            {
                PageNumber = (post.IndexInTopic / ForumOptions.PostsPerPage) + 1,
                TopicId = topicId,
                TopicTitle = topic.Title
            };
        }

        public TopicPageRoutedata GetRouteDataForFirstNewPost(string topicId, string username)
        {
            var topic = this.ravenSession.Load<Topic>(topicId);
            Post post = topic.FirstUnreadPostForUser(username);

            return new TopicPageRoutedata { TopicId = topicId, PageNumber = (post.IndexInTopic / ForumOptions.PostsPerPage) + 1, TopicTitle = topic.Title };
        }
    }
}