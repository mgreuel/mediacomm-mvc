using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Features.Forum.Models;
using MediaCommMvc.Web.Features.Forum.ViewModels;

using Raven.Client;

namespace MediaCommMvc.Web.Features.Forum
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

            foreach (PollAnswer answer in topic.Poll.Answers)
            {
                // todo: Check whether this should be moved to the model
                bool newAnswerValue = userAnswer.CheckedAnswers.Contains(answer.Id);

                if (newAnswerValue && !answer.Usernames.Contains(username))
                {
                    answer.Usernames.Add(username);
                }
                else if (!newAnswerValue && answer.Usernames.Contains(username))
                {
                    answer.Usernames.Remove(username);
                }
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
            var topic = new Topic
                              {
                                  ExcludedUserNames = viewModel.ExcludedUserNames ?? new List<string>(),
                                  Title = viewModel.Title,
                                  IsWiki = viewModel.IsWiki,
                                  Posts =
                                      new List<Post>
                                          {
                                              new Post
                                                  {
                                                      AuthorName = currentUsername,
                                                      CreatedAt = DateTime.UtcNow,
                                                      IndexInTopic = 0,
                                                      Text = viewModel.Text,
                                                  }
                                          }
                              };

            topic.DisplayPriority = viewModel.IsSticky ? TopicDisplayPriority.Sticky : TopicDisplayPriority.None;

            if (!viewModel.Poll.IsEmpty())
            {
                topic.Poll = viewModel.Poll.ToNewPoll();
            }
            
            this.ravenSession.Store(topic);
            return topic;
        }

        private Topic UpdateTopic(EditTopicViewModel viewModel)
        {
            var topic = this.ravenSession.Load<Topic>(viewModel.Id);
            topic.Title = viewModel.Title;
            topic.PostsInOrder.First().Text = viewModel.Text;
            topic.ExcludedUserNames = viewModel.ExcludedUserNames ?? new List<string>();

            if(viewModel.Poll.IsEmpty())
            {
                topic.Poll = null;
            }
            else
            {
                if (topic.Poll == null)
                {
                    topic.Poll = viewModel.Poll.ToNewPoll();
                }
                else
                {
                    viewModel.Poll.UpdatePoll(topic.Poll);
                }
            }

            topic.IsWiki = viewModel.IsWiki;
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
