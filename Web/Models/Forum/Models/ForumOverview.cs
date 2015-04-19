using System.Collections.Generic;

using MediaCommMvc.Web.ViewModels;

namespace MediaCommMvc.Web.Models.Forum.Models
{
    public class ForumOverview
    {
        public List<TopicOverviewViewModel> TopicsForCurrentPage { get; set; }

        public int TotalNumberOfTopics { get; set; }
    }
}