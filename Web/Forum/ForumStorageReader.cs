using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Forum.ViewModels;
using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Models.Forum.Models;

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
                Id = topic.Id,
                Title = topic.Title,
                PageNumber = pageNumber,
                Posts = topic.Posts
                .OrderBy(post => post.Created)
                .Skip((pageNumber - 1) * ForumOptions.PostsPerPage)
                .Take(ForumOptions.PostsPerPage)
                .Select(post => new PostViewModel(post))
                .ToList(),
                TotalNumberOfPosts = topic.Posts.Count
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
                Text = topic.Posts.OrderBy(p => p.IndexInTopic).First().Text
            };
        }

        public EditPostViewModel GetEditPostViewModel(string topicId, int postIndex)
        {
            // todo move projection to raven db
            Post post = this.ravenSession.Load<Topic>(topicId).Posts.Single(p => p.IndexInTopic == postIndex);
            return new EditPostViewModel { PostIndex = postIndex, Text = post.Text, TopicId = topicId };
        }
    }
}