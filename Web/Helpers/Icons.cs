using MediaCommMvc.Web.Forum.Models;
using MediaCommMvc.Web.Forum.ViewModels;

namespace MediaCommMvc.Web.Helpers
{
    public static class Icons
    {
        public static string TopicIconClass(TopicOverviewViewModel topic)
        {
            if (!topic.ReadByCurrentUser)
            {
                return "glyphicon glyphicon-eye-open";
            }

            if (topic.DisplayPriority == TopicDisplayPriority.Sticky)
            {
                return "glyphicon glyphicon-exclamation-sign";
            }

            return "glyphicon glyphicon-eye-close";
        }

        public static string TopicIconTitle(TopicOverviewViewModel topic)
        {
            if (!topic.ReadByCurrentUser)
            {
                return @Resources.Forums.NewPosts;
            }

            if (topic.DisplayPriority == TopicDisplayPriority.Sticky)
            {
                if (!topic.ReadByCurrentUser)
                {
                    return @Resources.Forums.StickyNewPosts;
                }

                return @Resources.Forums.StickyNoNewPosts;
            }

            return @Resources.Forums.NoNewPosts;
        }
    }
}