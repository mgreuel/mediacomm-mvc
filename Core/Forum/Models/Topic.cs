using System;
using System.Collections.Generic;

namespace Core.Forum.Models
{
    public class Topic
    {
        public Topic()
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

        // Todo make private/protected // http://blog.oneunicorn.com/2012/03/26/code-first-data-annotations-on-non-public-properties/ 
        public string ExcludedUsersStorage { get; set; }

        public IEnumerable<string> ExcludedUserNames
        {
            get
            {
                return this.ExcludedUsersStorage.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }

            set
            {
                this.ExcludedUsersStorage = string.Join(",", value);
            }
        }

        public Dictionary<string, DateTime> LastAccessTimes { get; set; }

        public List<Post> Posts { get; set; }

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