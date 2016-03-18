using MediaCommMvc.Web.Features.Forum.Models;
using MediaCommMvc.Web.Features.Forum.ViewModels;

namespace MediaCommMvc.Web.Helpers
{
    public static class Icons
    {
        public static string TopicIconClass(TopicOverviewViewModel topic)
        {
            if (topic.DisplayPriority == TopicDisplayPriority.Sticky)
            {
                return topic.ReadByCurrentUser ? "glyphicon glyphicon-exclamation-sign text-muted" : "glyphicon glyphicon-exclamation-sign text-primary";
            }

            if (!topic.ReadByCurrentUser)
            {
                return "glyphicon glyphicon-eye-open text-primary";
            }


            return "glyphicon glyphicon-eye-close text-muted";
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