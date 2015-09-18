using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Forum.Models;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class TopicOverviewViewModel
    {
        public TopicOverviewViewModel(Topic topic, string currentUser)
        {
            this.CreatedBy = topic.CreatedBy;
            this.DisplayPriority = topic.DisplayPriority;

            if (topic.ExcludedUserNames != null && topic.ExcludedUserNames.Any())
            {
                string[] tempExcludedUsers = new string[topic.ExcludedUserNames.Count()];
                topic.ExcludedUserNames.ToList().CopyTo(tempExcludedUsers);
                this.ExcludedUsernames = tempExcludedUsers;
            }
            else
            {
                this.ExcludedUsernames = new List<string>();
            }

            this.Id = topic.Id;
            this.LastPostAuthor = topic.LastPostAuthor;
            this.LastPostTime = $"{topic.LastPostTime.ToLocalTime():g}";
            this.PostCount = topic.PostCount;
            this.Title = topic.Title;
            this.ReadByCurrentUser = topic.AllPostsReadByUser(currentUser);
            this.CreatedAt = $"{topic.CreatedAt.ToLocalTime():g}";

            this.HasPoll = topic.Poll != null && topic.Poll.Answers.Any();

            if (!this.ReadByCurrentUser)
            {
                this.TopicTitleCssClass = "unread";
            }
        }

        public TopicOverviewViewModel()
        {
        }

        public IEnumerable<string> ExcludedUsernames { get; }

        public string CreatedBy { get; }

        public int PostCount { get; }

        public string LastPostTime { get; }

        public string LastPostAuthor { get; }

        public string Title { get; }

        public bool ReadByCurrentUser { get; }

        public TopicDisplayPriority DisplayPriority { get; }

        public string Id { get; }

        public string TopicTitleCssClass { get; }

        public bool HasPoll { get; }

        public string CreatedAt { get; set; }
    }
}