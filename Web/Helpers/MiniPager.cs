using System;
using System.Text;
using System.Web.Mvc;

using MediaCommMvc.Web.Features.Forum;
using MediaCommMvc.Web.Features.Forum.ViewModels;

namespace MediaCommMvc.Web.Helpers
{
    public static class UrlHelperMiniPager
    {
        public static MvcHtmlString MiniPager(this UrlHelper urlHelper,TopicOverviewViewModel topic, Func<int, string> linkBuilder)
        {
            //<i class="glyphicon glyphicon-play-circle"></i>
            const string FormatNormal = "<span class='topic-pager hidden-xs'><a class='btn btn-default btn-xs' href='{0}'>{1}</a></span>";
            StringBuilder pagerBuilder = new StringBuilder();

            if (!topic.ReadByCurrentUser)
            {
                pagerBuilder.AppendFormat(
                    FormatNormal,
                    urlHelper.Action(MVC.Forum.FirstNewPostInTopic().AddRouteValue("topicId", topic.Id)),
                     Resources.Forums.GotoFirstNewPost + " <i class='glyphicon glyphicon-play'></i>");
            }

            int numberOfPages = (int)Math.Ceiling(topic.PostCount / (double)ForumOptions.PostsPerPage);

            if (numberOfPages <= 1)
            {
                return MvcHtmlString.Empty;
            }

            for (int page = 1; page <= numberOfPages; page++)
            {
                pagerBuilder.AppendFormat(FormatNormal, linkBuilder(page), page);
            }


            return MvcHtmlString.Create(pagerBuilder.ToString());
        }
    }
}
