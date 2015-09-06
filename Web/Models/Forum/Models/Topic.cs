using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace MediaCommMvc.Web.Models.Forum.Models
{
    public enum TopicDisplayPriority
    {
        None = 0, 

        Sticky = 10
    }

    public class PollAnswer
    {
        public string Text { get; set; }

        public IList<string> Usernames { get; set; }
    }

    public class Post
    {
        public Post()
        {
            this.ApprovalStorage = string.Empty;
        }

        public string AuthorName { get; set; }

        public DateTime Created { get; set; }

        public int Id { get; set; }

        public string Text { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }

        public IEnumerable<string> Approvals
        {
            get
            {
                return this.ApprovalStorage.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        // Todo make private/protected // http://blog.oneunicorn.com/2012/03/26/code-first-data-annotations-on-non-public-properties/ */
        public string ApprovalStorage { get; set; }

        public int IndexInTopic { get; set; }

        public void AddApproval(string username)
        {
            List<string> approvals = new List<string>(this.Approvals);
            approvals.Add(username);
            this.ApprovalStorage = string.Join((string)",", (IEnumerable<string>)approvals.Distinct());
        }
    }

    public class Poll
    {
        public string Question { get; set; }

        public IEnumerable<PollAnswer> Answers { get; set; }
    }

    public class Topic
    {
        private const string AccessTimeValueSeparator = ",";

        private const string AccessTimeItemSeparator = ";;";

        private static readonly Regex AccessTimeRegex = new Regex("([^;]+?)" + AccessTimeValueSeparator + "([^;]+)");

        public Topic()
        {
            this.ExcludedUserNames = new List<string>();
            //this.ExcludedUsersStorage = string.Empty;
            this.LastAccessTimesStorage = string.Empty;
        }

        public string CreatedBy { get; set; }

        public string PollStorage { get; set; }

        public Poll Poll { get; set; }

        public string Id { get; set; }

        public int NumericId => int.Parse(this.Id.Split('/')[1]);

        public string LastPostAuthor { get; set; }

        public DateTime LastPostTime { get; set; }

        public int PostCount { get; set; }

        public string Title { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        // Todo make private/protected // http://blog.oneunicorn.com/2012/03/26/code-first-data-annotations-on-non-public-properties/ 
        //public string ExcludedUsersStorage { get; set; }

        public string LastAccessTimesStorage { get; set; }

        public IEnumerable<string> ExcludedUserNames { get; set; }

        public List<Post> Posts { get; set; }

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

        public bool AllPostsReadByUser(string username)
        {
            // The millisecond is added to account for lower resolution of string storage of access times
            return this.LastAccessTimeForUser(username).AddMilliseconds(1) >= this.LastPostTime;
        }

        public void MarkTopicAsRead(string username)
        {
            Dictionary<string, DateTime> lastAccessTimes = this.LastAccessTimes;
            lastAccessTimes[username] = DateTime.UtcNow;
            this.LastAccessTimes = lastAccessTimes;
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