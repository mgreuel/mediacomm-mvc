using System;
using System.Collections.Generic;

namespace Core.Forum.Models
{
    public class TopicOverview
    {
        public string CreatedBy { get; set; }

        public int Id { get; set; }

        public string LastPostAuthor { get; set; }

        public DateTime LastPostTime { get; set; }

        public int PostCount { get; set; }

        public string Title { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        public IEnumerable<string> ExcludedUserNames { get; set; }
    }
}