using PagedList;

namespace MediaCommMvc.Web.Forum.ViewModels
{
    public class ForumViewModel
    {
        public IPagedList<TopicOverviewViewModel> Topics { get; set; }
    }
}
