using Core.Forum.ViewModels;

using PagedList;

namespace MediaCommMvc.Web.ViewModels
{
    public class PagedTopicDetailsViewModel
    {
        private readonly TopicDetailsViewModel topicDetails;

        public PagedTopicDetailsViewModel(TopicDetailsViewModel topicDetails)
        {
            this.topicDetails = topicDetails;
            this.PagedPosts = new StaticPagedList<PostViewModel>(topicDetails.PostsForCurrentPage, topicDetails.PageNumber, topicDetails.PostsPerPage, topicDetails.TotalNumberOfPosts);
        }

        public IPagedList<PostViewModel> PagedPosts { get; set; }

        public TopicDetailsViewModel TopicDetails
        {
            get
            {
                return this.topicDetails;
            }
        }
    }
}
