using System;
using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.Models
{
    public class Topic
    {
        //private const string AccessTimeValueSeparator = ",";

        //private const string AccessTimeItemSeparator = ";;";

        //private static readonly Regex AccessTimeRegex = new Regex("([^;]+?)" + AccessTimeValueSeparator + "([^;]+)");

        public Topic()
        {
            this.ExcludedUserNames = new List<string>();
            this.LastAccessTimes  = new Dictionary<string, DateTime>();
        }

        public string CreatedBy { get; set; }


        public Poll Poll { get; set; }

        public string Id { get; set; }

        // Probably not required when using ravendb index
        public string LastPostAuthor { get; set; }

        // Probably not required when using ravendb index
        public DateTime LastPostTime { get; set; }

        // Probably not required when using ravendb index
        public int PostCount { get; set; }

        public string Title { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        //public string LastAccessTimesStorage { get; set; }

        public IEnumerable<string> ExcludedUserNames { get; set; }

        public List<Post> Posts { get; set; }

        private Dictionary<string, DateTime> LastAccessTimes { get; set; }

        //private Dictionary<string, DateTime> LastAccessTimes1
        //{
        //    get
        //    {
        //        Dictionary<string, DateTime> lastAccessTimes = new Dictionary<string, DateTime>();

        //        MatchCollection matchCollection = AccessTimeRegex.Matches(this.LastAccessTimesStorage);
        //        foreach (Match match in matchCollection)
        //        {
        //            lastAccessTimes.Add(match.Groups[1].Value, DateTime.Parse(match.Groups[2].Value));
        //        }

        //        return lastAccessTimes;
        //    }

        //    set
        //    {
        //        IEnumerable<string> accessTimesStrings =
        //            value.Select(
        //                pair => pair.Key + AccessTimeValueSeparator + pair.Value.ToString(
        //                    "yyyy-MM-dd HH:mm:ss.fff", 
        //                    CultureInfo.InvariantCulture));
        //        this.LastAccessTimesStorage = string.Join(AccessTimeItemSeparator, accessTimesStrings);
        //    }
        //}

        public bool AllPostsReadByUser(string username)
        {
            // The millisecond is added to account for lower resolution of string storage of access times
            return this.LastAccessTimeForUser(username).AddMilliseconds(1) >= this.LastPostTime;
        }

        public void MarkTopicAsRead(string username)
        {
            this.LastAccessTimes[username] = DateTime.UtcNow;
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