using System.Collections.Generic;

using Core.Forum.ViewModels;

namespace Core.Forum.Models
{
    public class ForumOverview
    {
        public List<TopicOverviewViewModel> TopicsForCurrentPage { get; set; }

        public int TotalNumberOfTopics { get; set; }
    }
}