using System.Collections.Generic;

using Core.Forum.Models;

namespace Core.Forum.ViewModels
{
    public class TopicOverviewViewModel
    {
        public TopicOverviewViewModel(TopicOverview topicOverview, string currentUser)
        {
            this.CreatedBy = topicOverview.CreatedBy;
            this.DisplayPriority = topicOverview.DisplayPriority;

            string[] tempExcludedUsers = new string[topicOverview.ExcludedUserNames.Count];
            topicOverview.ExcludedUserNames.CopyTo(tempExcludedUsers);
            this.ExcludedUsernames = tempExcludedUsers;

            this.Id = topicOverview.Id;
            this.LastPostAuthor = topicOverview.LastPostAuthor;
            this.LastPostTime = string.Format("{0:g}", topicOverview.LastPostTime);
            this.PostCount = topicOverview.PostCount;
            this.Title = topicOverview.Title;
            this.ReadByCurrentUser = topicOverview.AllPostsReadByUser(currentUser);
        }

        public IEnumerable<string> ExcludedUsernames { get; set; }

        public string CreatedBy { get; set; }

        public int PostCount { get; set; }

        public string LastPostTime { get; set; }

        public string LastPostAuthor { get; set; }

        public string Title { get; set; }

        public bool ReadByCurrentUser { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        public int Id { get; set; }
    }
}