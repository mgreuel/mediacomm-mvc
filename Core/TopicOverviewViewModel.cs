using System;
using System.Collections.Generic;

using Core.Models.Forum;

namespace Core
{
    public class TopicOverviewViewModel
    {
        public IEnumerable<string> ExcludedUsernames { get; set; }

        public string CreatedBy { get; set; }

        public int PostCount { get; set; }

        public DateTime LastPostTime { get; set; }

        public string LastPostAuthor { get; set; }

        public string Title { get; set; }

        public bool ReadByCurrentUser { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        public int Id { get; set; }
    }
}