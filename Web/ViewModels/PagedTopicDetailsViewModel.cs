using System.Linq;

using Core;
using Core.Forum;
using Core.Forum.ViewModels;

using PagedList;

namespace MediaCommMvc.Web.ViewModels
{
    public class PagedTopicDetailsViewModel
    {
        private readonly TopicDetailsViewModel topicDetails;

        private const int PostsPerPage = 25;


        public PagedTopicDetailsViewModel(TopicDetailsViewModel topicDetails, int page)
        {
            this.topicDetails = topicDetails;
            this.PagedPosts = new PagedList<PostViewModel>(topicDetails.Posts, page, PostsPerPage);
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
