using Core;
using Core.Forum.Models;
using Core.Forum.ViewModels;

using MediaCommMvc.Web.ViewModels.Forum;

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
    }
}