using Core;

using PagedList;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class ForumViewModel
    {
        public IPagedList<TopicOverviewViewModel> Topics { get; set; }
    }
}
