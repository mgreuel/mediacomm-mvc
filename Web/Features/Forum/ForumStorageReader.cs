using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Features.Forum.Models;
using MediaCommMvc.Web.Features.Forum.ViewModels;

using Raven.Client;
using Raven.Client.Linq;

namespace MediaCommMvc.Web.Features.Forum
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

            if (topic.ExcludedUserNames.Any(u => u.Equals(currentUsername, StringComparison.OrdinalIgnoreCase)))
            {
                throw new UnauthorizedAccessException($"{currentUsername} is not allowed to access {topic.Title}");
            }

            var viewModel = new TopicDetailsViewModel
            {
                ExcludedUsernames = topic.ExcludedUserNames,
                Id = topic.Id,
                Title = topic.Title,
                PageNumber = pageNumber,
                Posts = topic.PostsInOrder
                    .Skip((pageNumber - 1) * ForumOptions.PostsPerPage)
                    .Take(ForumOptions.PostsPerPage)
                    .Select(post => new PostViewModel(post, topic.IsWiki))
                    .ToList(),
                TotalNumberOfPosts = topic.PostCount
            };

            if (topic.Poll != null && topic.Poll.Answers.Any())
            {
                viewModel.Poll = new ShowPollViewModel(topic.Poll, currentUsername);
            }

            return viewModel;
        }

        public EditTopicViewModel GetEditTopicViewModel(string id, string currentUsername)
        {
            Topic topic = this.ravenSession.Load<Topic>(id);

            if (topic.ExcludedUserNames.Any(u => u.Equals(currentUsername, StringComparison.OrdinalIgnoreCase)))
            {
                throw new UnauthorizedAccessException($"{currentUsername} is not allowed to access {topic.Title}");
            }

            var viewModel = new EditTopicViewModel
            {
                ExcludedUserNames = topic.ExcludedUserNames ?? new List<string>(),
                Title = topic.Title,
                Text = topic.PostsInOrder.First().Text,
                IsSticky = topic.DisplayPriority == TopicDisplayPriority.Sticky,
                IsWiki = topic.IsWiki,
                Id = topic.Id
            };

            if (topic.Poll != null)
            {
                viewModel.Poll = new EditPollViewModel { Answers = topic.Poll?.Answers?.Select(a => new EditPollAnswerViewModel { Id = a.Id, Text = a.Text }).ToList(), Question = topic.Poll?.Question };
            }

            return viewModel;
        }

        public EditPostViewModel GetEditPostViewModel(string topicId, int postIndex, string currentUsername)
        {
            // todo move projection to raven db
            var topic = this.ravenSession.Load<Topic>(topicId);

            if (topic.ExcludedUserNames.Any(u => u.Equals(currentUsername, StringComparison.OrdinalIgnoreCase)))
            {
                throw new UnauthorizedAccessException($"{currentUsername} is not allowed to access {topic.Title}");
            }

            Post post = topic.Posts.Single(p => p.IndexInTopic == postIndex);
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
                        .Where(t => !t.ExcludedUserNames.ContainsAny(new[] { currentUsername }))
                        .OrderByDescending(topic => topic.DisplayPriority)
                        .ThenByDescending(topic => topic.LastPostTime)
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
                TopicTitle = topic.Title,
                PostIndex =  topic.Posts.Max(p => p.IndexInTopic)
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
                TopicTitle = topic.Title,
                PostIndex = postIndex
            };
        }

        public TopicPageRoutedata GetRouteDataForFirstNewPost(string topicId, string username)
        {
            var topic = this.ravenSession.Load<Topic>(topicId);
            Post post = topic.FirstUnreadPostForUser(username);

            return new TopicPageRoutedata { TopicId = topicId, PageNumber = (post.IndexInTopic / ForumOptions.PostsPerPage) + 1, TopicTitle = topic.Title, PostIndex = post.IndexInTopic};
        }
    }
}