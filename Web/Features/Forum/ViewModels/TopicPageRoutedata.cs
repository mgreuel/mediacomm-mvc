namespace MediaCommMvc.Web.Features.Forum.ViewModels
{
    public class TopicPageRoutedata
    {
        public string TopicId { get; set; }

        public string TopicTitle { get; set; }

        public int PageNumber { get; set; }

        public int? PostIndex { get; set; }
    }
}