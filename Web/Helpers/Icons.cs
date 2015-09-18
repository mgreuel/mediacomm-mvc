using MediaCommMvc.Web.Forum.Models;
using MediaCommMvc.Web.Forum.ViewModels;

namespace MediaCommMvc.Web.Helpers
{
    public static class Icons
    {
        public static string TopicIconClass(TopicOverviewViewModel topic)
        {
            if (topic.DisplayPriority == TopicDisplayPriority.Sticky)
            {
                return "glyphicon glyphicon-exclamation-sign";
            }

            if (!topic.ReadByCurrentUser)
            {
                return "glyphicon glyphicon-eye-open";
            }


            return "glyphicon glyphicon-eye-close";
        }

        public static string TopicIconTitle(TopicOverviewViewModel topic)
        {
            if (topic.DisplayPriority == TopicDisplayPriority.Sticky)
            {
                return !topic.ReadByCurrentUser ? @Resources.Forums.StickyNewPosts : @Resources.Forums.StickyNoNewPosts;
            }

            return !topic.ReadByCurrentUser ? @Resources.Forums.NewPosts : @Resources.Forums.NoNewPosts;
        }
    }
}