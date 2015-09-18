using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Forum.Models;
using MediaCommMvc.Web.Forum.ViewModels;

using Raven.Abstractions.Data;
using Raven.Client;

namespace MediaCommMvc.Web.Forum
{
    public class ForumStorageWriter
    {
        private readonly IDocumentSession ravenSession;

        public ForumStorageWriter(IDocumentSession ravenSession)
        {
            this.ravenSession = ravenSession;
        }

        public void SavePollAnswer(PollUserAnswerInput userAnswer, string username)
        {
            Topic topic = this.ravenSession.Load<Topic>(userAnswer.TopicId);

            int index = 0;

            // if the ordering is changed, make sure to also change it in the view model
            foreach (PollAnswer answer in topic.Poll.Answers.OrderBy(a => a.Text))
            {
                // todo: Check whether this should be moved to the model
                bool newAnswerValue = userAnswer.CheckedAnswers.Contains(index);

                if (newAnswerValue && !answer.Usernames.Contains(username))
                {
                    answer.Usernames.Add(username);
                }
                else if (!newAnswerValue && answer.Usernames.Contains(username))
                {
                    answer.Usernames.Remove(username);
                }

                index = index + 1;
            }
        }

        public string SaveTopic(EditTopicViewModel viewModel, string currentUsername)
        {
            Topic topic;

            if (viewModel.Id != null)
            {
                topic = this.UpdateTopic(viewModel);
            }
            else
            {
                topic = this.CreateTopic(viewModel, currentUsername);
            }

            return topic.Id;
        }

        private Topic CreateTopic(EditTopicViewModel viewModel, string currentUsername)
        {
            Topic topic;
            topic = new Topic
                        {
                            ExcludedUserNames = viewModel.ExcludedUserNames,
                            Title = viewModel.Title,
                            Poll = viewModel.Poll.ToPoll(),
                            Posts =
                                new List<Post>
                                    {
                                        new Post
                                            {
                                                AuthorName = currentUsername,
                                                CreatedAt = DateTime.UtcNow,
                                                IndexInTopic = 0,
                                                Text = viewModel.Text
                                            }
                                    }
                        };

            topic.DisplayPriority = viewModel.IsSticky ? TopicDisplayPriority.Sticky : TopicDisplayPriority.None;

            this.ravenSession.Store(topic);
            return topic;
        }

        private Topic UpdateTopic(EditTopicViewModel viewModel)
        {
            Topic topic;
            topic = this.ravenSession.Load<Topic>(viewModel.Id);
            topic.Title = viewModel.Title;
            topic.PostsInOrder.First().Text = viewModel.Text;
            topic.ExcludedUserNames = viewModel.ExcludedUserNames;

            topic.DisplayPriority = viewModel.IsSticky ? TopicDisplayPriority.Sticky : TopicDisplayPriority.None;
            return topic;
        }

        public void AddApproval(string topicId, int postIndex, string username)
        {
            var topic = this.ravenSession.Load<Topic>(topicId);
            topic.Posts.Single(p => p.IndexInTopic == postIndex).Approvals.Add(username);
        }

        public void MarkTopicAsRead(string topicId, string currentUsername)
        {
            var topic = this.ravenSession.Load<Topic>(topicId);
            topic.MarkTopicAsRead(currentUsername);
        }

        public void AddReply(ReplyViewModel viewModel, string currentUsername)
        {
            var post = new Post
            {
                AuthorName = currentUsername,
                CreatedAt = DateTime.UtcNow,
                Text = viewModel.Text
            };

            var topic = this.ravenSession.Load<Topic>(viewModel.TopicId);
            post.IndexInTopic = topic.Posts.Max(p => p.IndexInTopic) + 1;

            topic.Posts.Add(post);
        }

        public void UpdatePost(EditPostViewModel viewModel)
        {
            var topic = this.ravenSession.Load<Topic>(viewModel.TopicId);
            topic.Posts.Single(p => p.IndexInTopic == viewModel.PostIndex).Text = viewModel.Text;
        }
    }
}
