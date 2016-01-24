using System;
using System.Collections.Generic;
using System.Linq;

using Raven.Imports.Newtonsoft.Json;

namespace MediaCommMvc.Web.Features.Forum.Models
{
    public class Topic
    {
        private IList<string> excludedUserNames;

        public Topic()
        {
            this.ExcludedUserNames = new List<string>();
            this.LastAccessTimes  = new Dictionary<string, DateTime>();
            this.Posts = new List<Post>();
            this.Poll = new Poll();
        }

        public string CreatedBy => this.PostsInOrder.First().AuthorName;

        public Poll Poll { get; set; }

        public string Id { get; set; }

        // Probably not required when using ravendb index
        public string LastPostAuthor => this.PostsInOrder.Last().AuthorName;

        // Probably not required when using ravendb index
        public DateTime LastPostTime => this.PostsInOrder.Last().CreatedAt;

        public int PostCount => this.Posts.Count;

        public string Title { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        public IList<string> ExcludedUserNames
        {
            get
            {
                return this.excludedUserNames;
            }
            set
            {
                this.excludedUserNames = value ?? new List<string>();
            }
        }

        public List<Post> Posts { get; set; }

        [JsonIgnore]
        public IOrderedEnumerable<Post> PostsInOrder
        {
            get
            {
                return this.Posts.OrderBy(p => p.IndexInTopic);
            }
        }

        public bool IsWiki { get; set; }

        private Dictionary<string, DateTime> LastAccessTimes { get; set; }

        public DateTime CreatedAt => this.PostsInOrder.First().CreatedAt;

        public bool AllPostsReadByUser(string username)
        {
            // The millisecond is added to account for lower resolution of string storage of access times
            return this.LastAccessTimeForUser(username).AddMilliseconds(1) >= this.LastPostTime;
        }

        public void MarkTopicAsRead(string username)
        {
            this.LastAccessTimes[username] = DateTime.UtcNow;
        }

        public Post FirstUnreadPostForUser(string username)
        {
            DateTime lastAccessTime = this.LastAccessTimeForUser(username);

            // If all posts have been read, we just return the newest one
            return this.PostsInOrder.FirstOrDefault(p => p.CreatedAt > lastAccessTime) ?? this.PostsInOrder.Last();
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