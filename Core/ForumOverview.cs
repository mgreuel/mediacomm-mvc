using System.Collections.Generic;

namespace Core
{
    public class ForumOverview
    {
        public List<TopicOverviewViewModel> TopicsForCurrentPage { get; set; }

        public int TotalNumberOfTopics { get; set; }
    }
}