using System.Collections.Generic;

namespace MediaCommMvc.Web.Features.Forum.ViewModels
{
    public class ForumOverview
    {
        public List<TopicOverviewViewModel> TopicsForCurrentPage { get; set; }

        public int TotalNumberOfTopics { get; set; }
    }
}