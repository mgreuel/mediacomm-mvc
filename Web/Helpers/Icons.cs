using MediaCommMvc.Web.Models.Forum;
using MediaCommMvc.Web.ViewModels.Forum;

namespace MediaCommMvc.Web.Helpers
{
    public static class Icons
    {
        public static string TopicIconClass(TopicOverviewViewModel topic)
        {
            if (!topic.ReadByCurrentUser)
            {
                return "icon-eye-open";
            }

            if (topic.DisplayPriority == TopicDisplayPriority.Sticky)
            {
                return "icon-exclamation-sign";
            }

            return "icon-eye-close";
        }
    }
}