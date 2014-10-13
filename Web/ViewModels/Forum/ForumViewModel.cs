using System.Collections.Generic;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class ForumViewModel
    {
        public IEnumerable<TopicOverviewViewModel> Topics { get; set; }
    }
}
