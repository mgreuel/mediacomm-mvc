using Core.Forum.Models;

namespace Core.Forum.ViewModels
{
    public class TopicPageRoutedata
    {
        public TopicPageRoutedata(Post post, int postsPerPage)
        {
            this.TopicId = post.TopicId;
            this.TopicTitle = post.Topic.Title;

            this.PageNumber = (post.IndexInTopic / postsPerPage) + 1;
        }

        public int TopicId { get; set; }

        public string TopicTitle { get; set; }

        public int PageNumber { get; set; }
    }
}