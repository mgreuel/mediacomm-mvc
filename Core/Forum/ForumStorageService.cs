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

        private Dictionary<int, TopicOverview> topicOverviews = new Dictionary<int, TopicOverview>();

        private Dictionary<int, TopicDetails> topicDetails = new Dictionary<int, TopicDetails>();
        

        public int AddTopic(CreateTopicCommand createTopicCommand)
        {
            int id;

            lock (this.lockObject)
            {
                id = this.topicDetails.DefaultIfEmpty().Max(t => t.Key) + 1;

                // todo: introduce post model
                this.topicDetails.Add(
                    id,
                    new TopicDetails
                        {
                            Id = id,
                            Title = createTopicCommand.Title,
                            Posts =
                                new List<Post>
                                    {
                                        new Post
                                            {
                                                AuthorName = createTopicCommand.AuthorName, 
                                                Created = createTopicCommand.TimeStamp, 
                                                Id = 1, 
                                                Text = createTopicCommand.Text
                                            }
                                    }
                        });

                this.topicOverviews.Add(
                    id,
                    new TopicOverview
                        {
                            CreatedBy = createTopicCommand.AuthorName,
                            Id = id,
                            LastPostAuthor = createTopicCommand.AuthorName,
                            LastPostTime = createTopicCommand.TimeStamp,
                            PostCount = 1,
                            Title = createTopicCommand.Title
                        });
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
                                                          .Select(topic => this.MapTopicOverViewToViewModel(topic, currentUser))
                                                          .ToList(),
                                                      TotalNumberOfTopics = this.topicOverviews.Count
                                                  };

                return forumOverview;
            }
        }

        private TopicOverviewViewModel MapTopicOverViewToViewModel(TopicOverview topic, string currentUser)
        {
            return new TopicOverviewViewModel
                       {
                           CreatedBy = topic.CreatedBy,
                           DisplayPriority = topic.DisplayPriority,
                           ExcludedUsernames = topic.ExcludedUserNames,
                           Id = topic.Id,
                           LastPostAuthor = topic.LastPostAuthor,
                           LastPostTime = string.Format("{0:g}", topic.LastPostTime),
                           PostCount = topic.PostCount,
                           Title = topic.Title,
                           // todo ReadByCurrentUser = topic.
                       };
        }

        public TopicDetailsViewModel GetTopicDetailsViewModel(int id)
        {
            // Todo: Copy to make thread safe
            lock (this.lockObject)
            {
                return new TopicDetailsViewModel(this.topicDetails[id]);
            }
        }

        public void AddReply(AddReplyCommand addReplyCommand)
        {
            lock (this.lockObject)
            {
                TopicDetails topicDetails = this.topicDetails[addReplyCommand.TopicId];

                // create post model
                int id = topicDetails.Posts.Max(model => model.Id) + 1;
                topicDetails.Posts.Add(new Post { AuthorName = addReplyCommand.AuthorName, Created = addReplyCommand.Created, Id = id, Text = addReplyCommand.Text});

                TopicOverview topicOverview = this.topicOverviews[addReplyCommand.TopicId];
                topicOverview.PostCount = topicOverview.PostCount + 1;
                topicOverview.LastPostAuthor = addReplyCommand.AuthorName;
                topicOverview.LastPostTime = addReplyCommand.Created;
            }
        }
    }
}