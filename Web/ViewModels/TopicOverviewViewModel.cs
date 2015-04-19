using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Models.Forum.Models;

namespace MediaCommMvc.Web.ViewModels
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

            this.Id = topic.TopicId;
            this.LastPostAuthor = topic.LastPostAuthor;
            this.LastPostTime = string.Format("{0:g}", topic.LastPostTime);
            this.PostCount = topic.PostCount;
            this.Title = topic.Title;
            this.ReadByCurrentUser = topic.AllPostsReadByUser(currentUser);

            if (!this.ReadByCurrentUser)
            {
                this.TopicTitleCssClass = "unread";
            }
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

        public string TopicTitleCssClass { get; set; }
    }
}