using PagedList;

namespace MediaCommMvc.Web.Features.Forum.ViewModels
{
    public class ForumViewModel
    {
        public IPagedList<TopicOverviewViewModel> Topics { get; set; }
    }
}
