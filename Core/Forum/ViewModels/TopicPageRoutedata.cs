using Core.Forum.Models;

namespace Core.Forum.ViewModels
{
    public class TopicPageRoutedata
    {
        public int TopicId { get; set; }

        public string TopicTitle { get; set; }

        public int PageNumber { get; set; }

        public static TopicPageRoutedata FromPost(Post post, int postsPerPage)
        {
            return new TopicPageRoutedata
                       {
                           PageNumber = (post.IndexInTopic / postsPerPage) + 1,
                           TopicId = post.TopicId,
                           TopicTitle = post.Topic.Title
                       };
        }

        public static TopicPageRoutedata LastPageOfTopic(Topic topic, int postsPerPage)
        {
            return new TopicPageRoutedata
                       {
                           PageNumber = ((topic.PostCount - 1) / postsPerPage) + 1,
                           TopicId = topic.TopicId,
                           TopicTitle = topic.Title
                       };
        }
    }
}