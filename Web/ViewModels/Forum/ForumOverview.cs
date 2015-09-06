using System.Collections.Generic;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class ForumOverview
    {
        public List<TopicOverviewViewModel> TopicsForCurrentPage { get; set; }

        public int TotalNumberOfTopics { get; set; }
    }
}