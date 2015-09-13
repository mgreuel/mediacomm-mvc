using System.Linq;
using System.Security.Principal;

using MediaCommMvc.Web.Forum.ViewModels;
using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Models.Forum.Models;

using Raven.Client;

namespace MediaCommMvc.Web.Forum
{
    public class ForumStorage
    {
        private readonly IDocumentSession ravenSession;

        public ForumStorage(IDocumentSession ravenSession)
        {
            this.ravenSession = ravenSession;
        }

        public TopicDetailsViewModel GetTopicDetailsViewModel(int id, int pageNumber, string currentUsername)
        {
            // todo move projections to ravendb
            Topic topic = this.ravenSession.Query<Topic>().Single(t => t.Id == Topic.NumericIdToId(id));
            var viewModel = new TopicDetailsViewModel
            {
                Title = topic.Title,
                PageNumber = pageNumber,
                Poll = new ShowPollViewModel(topic.Poll, currentUsername),
                Posts = topic.Posts
                .OrderBy(post => post.Created)
                .Skip((pageNumber - 1) * ForumOptions.PostsPerPage)
                .Take(ForumOptions.PostsPerPage)
                .Select(post => new PostViewModel(post))
                .ToList(),
                TotalNumberOfPosts = topic.Posts.Count
            };

            return viewModel;
        }
    }
}