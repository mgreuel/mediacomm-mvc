using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Models.Forum.Models;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class TopicOverviewViewModel
    {
        public TopicOverviewViewModel(Topic topic, string currentUser)
        {
            this.CreatedBy = topic.CreatedBy;
            this.DisplayPriority = topic.DisplayPriority;

            string[] tempExcludedUsers = new string[topic.ExcludedUserNames.Count()];
            topic.ExcludedUserNames.ToList().CopyTo(tempExcludedUsers);
            this.ExcludedUsernames = tempExcludedUsers;

            this.Id = topic.NumericId;
            this.LastPostAuthor = topic.LastPostAuthor;
            this.LastPostTime = $"{topic.LastPostTime:g}";
            this.PostCount = topic.PostCount;
            this.Title = topic.Title;
            this.ReadByCurrentUser = topic.AllPostsReadByUser(currentUser);

            this.HasPoll = topic.Poll != null;

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

        public int Id { get; }

        public string TopicTitleCssClass { get; }

        public bool HasPoll { get; }
    }
}