using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class ForumStorageService
    {
        private readonly object lockObject = new object();

        private Dictionary<int, TopicOverviewViewModel> topicOverviewViewModels = new Dictionary<int, TopicOverviewViewModel>();

        private Dictionary<int, TopicDetails> topics = new Dictionary<int, TopicDetails>();

        public int AddTopic(CreateTopicCommand createTopicCommand)
        {
            int id;

            lock (this.lockObject)
            {
                id = this.topics.DefaultIfEmpty().Max(t => t.Key) + 1;

                // todo: introduce post model
                this.topics.Add(
                    id,
                    new TopicDetails
                        {
                            Id = id,
                            Title = createTopicCommand.Title,
                            Posts =
                                new List<PostViewModel>
                                    {
                                        new PostViewModel
                                            {
                                                AuthorName = createTopicCommand.AuthorName, 
                                                Created = createTopicCommand.TimeStamp, 
                                                Id = 1, 
                                                Text = createTopicCommand.Text
                                            }
                                    }
                        });

                this.topicOverviewViewModels.Add(
                    id,
                    new TopicOverviewViewModel
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

        public ForumOverview GetForumOverview(int page, int topicsPerPage)
        {
            ForumOverview forumOverview = new ForumOverview
                                              {
                                                  // Todo: Copy to make thread safe
                                                  TopicsForCurrentPage =
                                                      this.topicOverviewViewModels.Select(pair => pair.Value).OrderByDescending(topic => topic.LastPostTime)
                                                      .Skip((page - 1) * topicsPerPage)
                                                      .Take(topicsPerPage)
                                                      .ToList(),
                                                  TotalNumberOfTopics = this.topicOverviewViewModels.Count
                                              };

            return forumOverview;
        }

        public TopicDetails GetTopicDetailsViewModel(int id)
        {
            // Todo: Copy to make thread safe
            return this.topics[id];
        }

        public void AddReply(AddReplyCommand addReplyCommand)
        {
            lock (this.lockObject)
            {
                TopicDetails topicDetails = this.topics[addReplyCommand.TopicId];

                // create post model
                int id = topicDetails.Posts.Max(model => model.Id) + 1;
                topicDetails.Posts.Add(new PostViewModel { AuthorName = addReplyCommand.AuthorName, Created = addReplyCommand.Created, Id = id, Text = addReplyCommand.Text});

                TopicOverviewViewModel topicOverviewViewModel = this.topicOverviewViewModels[addReplyCommand.TopicId];
                topicOverviewViewModel.PostCount = topicOverviewViewModel.PostCount + 1;
                topicOverviewViewModel.LastPostAuthor = addReplyCommand.AuthorName;
                topicOverviewViewModel.LastPostTime = addReplyCommand.Created;
            }
        }
    }
}