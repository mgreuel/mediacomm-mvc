using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

using Core.Forum.Commands;
using Core.Forum.Models;
using Core.Forum.ViewModels;

using MediaCommMvc.Web.Infrastructure.Database;
using MediaCommMvc.Web.ViewModels.Forum;

namespace MediaCommMvc.Web.Infrastructure
{
    public class EfForumStorageService
    {
        private readonly ApplicationDbContext databaseContext;

        // todo refactor to more fine granular locking
        private readonly object lockObject = new object();

        public EfForumStorageService(ApplicationDbContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public int AddTopic(CreateTopicCommand createTopicCommand)
        {
            TopicDetails topicDetails = createTopicCommand.ToTopicDetails();
            this.databaseContext.TopicDetails.Add(topicDetails);

            // this populates the TopicId
            this.databaseContext.SaveChanges();

            this.databaseContext.TopicOverviews.Add(createTopicCommand.ToTopicOverview(topicDetails.TopicId));
            this.databaseContext.SaveChanges();

            return topicDetails.TopicId;
        }

        public ForumOverview GetForumOverview(int page, int topicsPerPage, string currentUser)
        {
            ForumOverview forumOverview = new ForumOverview
                                              {
                                                  TopicsForCurrentPage =
                                                      this.databaseContext.TopicOverviews
                                                      .OrderByDescending(topic => topic.LastPostTime)
                                                      .Skip((page - 1) * topicsPerPage)
                                                      .Take(topicsPerPage)
                                                      .ToList()
                                                      .Select(topic => new TopicOverviewViewModel(topic, currentUser))
                                                      .ToList(), 
                                                  TotalNumberOfTopics = this.databaseContext.TopicOverviews.Count()
                                              };

            return forumOverview;
        }

        public TopicDetailsViewModel GetTopicDetailsViewModel(int id, int page, int postsPerPage, IPrincipal currentUser)
        {
            TopicDetails topicDetails = this.databaseContext.TopicDetails
                .Include(topic => topic.Posts)
                .Single(details => details.TopicId == id);

            return new TopicDetailsViewModel(topicDetails, page, postsPerPage, currentUser);
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

            using (var transaction = this.databaseContext.Database.BeginTransaction())
            {
                TopicOverview topicOverview = this.databaseContext.TopicOverviews.Single(overview => overview.TopicId == addReplyCommand.TopicId);
                topicOverview.PostCount = topicOverview.PostCount + 1;
                topicOverview.LastPostAuthor = addReplyCommand.AuthorName;
                topicOverview.LastPostTime = addReplyCommand.Created;

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

        public TopicPageRoutedata GetTopicPageRouteDataForPost(int postId)
        {
            // Todo add TopicDetails navigation peroperty
            Post post = this.databaseContext.Posts.Single(p => p.Id == postId);

            return new TopicPageRoutedata
        }
    }
}