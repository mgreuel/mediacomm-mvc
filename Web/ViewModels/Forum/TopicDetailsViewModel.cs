using PagedList;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class TopicDetailsViewModel
    {
        public IPagedList<PostViewModel> Posts { get; set; }

        public string Title { get; set; }

        public int Id { get; set; }
    }
}
