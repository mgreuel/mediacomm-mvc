using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Forum.Models
{
    public class Topic
    {
        private const string AccessTimeValueSeparator = ",";

        private const string AccessTimeItemSeparator = ";;";

        private static readonly Regex AccessTimeRegex = new Regex("([^;]+?)" + AccessTimeValueSeparator + "([^;]+)");

        public Topic()
        {
            this.ExcludedUserNames = new List<string>();
            this.ExcludedUsersStorage = string.Empty;
            this.LastAccessTimesStorage = string.Empty;
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

        public string LastAccessTimesStorage { get; set; }

        public IEnumerable<string> ExcludedUserNames
        {
            get
            {
                return this.ExcludedUsersStorage.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }

            set
            {
                if (value != null)
                {
                    this.ExcludedUsersStorage = string.Join(",", value);
                }
            }
        }

        public List<Post> Posts { get; set; }

        public bool AllPostsReadByUser(string username)
        {
            // The millisecond is added to account for lower resolution of string storage of access times
            return this.LastAccessTimeForUser(username).AddMilliseconds(1) >= this.LastPostTime;
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

        private Dictionary<string, DateTime> LastAccessTimes
        {
            get
            {
                Dictionary<string, DateTime> lastAccessTimes = new Dictionary<string, DateTime>();

                MatchCollection matchCollection = AccessTimeRegex.Matches(this.LastAccessTimesStorage);
                foreach (Match match in matchCollection)
                {
                    lastAccessTimes.Add(match.Groups[1].Value, DateTime.Parse(match.Groups[2].Value));
                }

                return lastAccessTimes;
            }

            set
            {
                IEnumerable<string> accessTimesStrings =
                    value.Select(
                        pair => pair.Key + AccessTimeValueSeparator + pair.Value.ToString(
                            "yyyy-MM-dd HH:mm:ss.fff",
                            CultureInfo.InvariantCulture));
                this.LastAccessTimesStorage = string.Join(AccessTimeItemSeparator, accessTimesStrings);
            }
        }

        public void MarkTopicAsRead(string username)
        {
            Dictionary<string, DateTime> lastAccessTimes = this.LastAccessTimes;
            lastAccessTimes[username] = DateTime.UtcNow;
            this.LastAccessTimes = lastAccessTimes;
        }
    }
}