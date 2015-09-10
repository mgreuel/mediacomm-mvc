using System.Collections.Generic;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class ForumOverview
    {
        public List<TopicOverviewViewModel> TopicsForCurrentPage { get; set; }

        public int TotalNumberOfTopics { get; set; }
    }
}