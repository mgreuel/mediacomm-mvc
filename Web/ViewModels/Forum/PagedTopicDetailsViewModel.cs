// --------------------------------------------------------------------------------------------------------------------
using PagedList;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class PagedTopicDetailsViewModel
    {
        public PagedTopicDetailsViewModel(TopicDetailsViewModel topicDetails)
        {
            this.TopicDetails = topicDetails;
            this.PagedPosts = new StaticPagedList<PostViewModel>(topicDetails.PostsForCurrentPage, topicDetails.PageNumber, topicDetails.PostsPerPage, topicDetails.TotalNumberOfPosts);
        }

        public IPagedList<PostViewModel> PagedPosts { get; }

        public TopicDetailsViewModel TopicDetails { get; }

    }
}
