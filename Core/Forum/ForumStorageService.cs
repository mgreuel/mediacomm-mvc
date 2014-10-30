using System.Collections.Generic;
using System.Linq;

using Core.Forum.Commands;
using Core.Forum.Models;
using Core.Forum.ViewModels;

namespace Core.Forum
{
    public class ForumStorageService
    {
        // todo refactor to more fine granular locking
        private readonly object lockObject = new object();

        private Dictionary<int, TopicDetails> topicDetails = new Dictionary<int, TopicDetails>();

        private Dictionary<int, TopicOverview> topicOverviews = new Dictionary<int, TopicOverview>();

        public int AddTopic(CreateTopicCommand createTopicCommand)
        {
            int id;

            lock (this.lockObject)
            {
                id = this.topicDetails.DefaultIfEmpty().Max(t => t.Key) + 1;

                this.topicDetails.Add(id, createTopicCommand.ToTopicDetails(id));

                this.topicOverviews.Add(id, createTopicCommand.ToTopicOverview(id));
            }

            return id;
        }

        public ForumOverview GetForumOverview(int page, int topicsPerPage, string currentUser)
        {
            lock (this.lockObject)
            {
                ForumOverview forumOverview = new ForumOverview
                                                  {
                                                      TopicsForCurrentPage =
                                                          this.topicOverviews.Select(pair => pair.Value).OrderByDescending(topic => topic.LastPostTime)
                                                          .Skip((page - 1) * topicsPerPage)
                                                          .Take(topicsPerPage)
                                                          .Select(topic => new TopicOverviewViewModel(topic, currentUser))
                                                          .ToList(), 
                                                      TotalNumberOfTopics = this.topicOverviews.Count
                                                  };

                return forumOverview;
            }
        }

        public TopicDetailsViewModel GetTopicDetailsViewModel(int id, int page, int postsPerPage, string currentUser)
        {
            lock (this.lockObject)
            {
                return new TopicDetailsViewModel(this.topicDetails[id], page, postsPerPage, currentUser);
            }
        }

        public void AddReply(AddReplyCommand addReplyCommand)
        {
            lock (this.lockObject)
            {
                TopicDetails topicDetail = this.topicDetails[addReplyCommand.TopicId];

                int id = topicDetail.Posts.Max(model => model.Id) + 1;
                topicDetail.Posts.Add(
                    new Post { AuthorName = addReplyCommand.AuthorName, Created = addReplyCommand.Created, Id = id, Text = addReplyCommand.Text });

                TopicOverview topicOverview = this.topicOverviews[addReplyCommand.TopicId];
                topicOverview.PostCount = topicOverview.PostCount + 1;
                topicOverview.LastPostAuthor = addReplyCommand.AuthorName;
                topicOverview.LastPostTime = addReplyCommand.Created;
            }
        }
    }
}