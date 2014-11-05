using System;
using System.Collections.Generic;

namespace Core.Forum.Models
{
    public class TopicOverview
    {
        public TopicOverview()
        {
            this.ExcludedUserNames = new List<string>();
            this.LastAccessTimes = new Dictionary<string, DateTime>();
        }

        public string CreatedBy { get; set; }

        public int TopicId { get; set; }

        public string LastPostAuthor { get; set; }

        public DateTime LastPostTime { get; set; }

        public int PostCount { get; set; }

        public string Title { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        public List<string> ExcludedUserNames { get; set; }

        public Dictionary<string, DateTime> LastAccessTimes { get; set; }

        public bool AllPostsReadByUser(string username)
        {
            return this.LastAccessTimeForUser(username) > this.LastPostTime;
        }

        private DateTime LastAccessTimeForUser(string user)
        {
            if (this.LastAccessTimes.ContainsKey(user))
            {
                return this.LastAccessTimes[user];
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}