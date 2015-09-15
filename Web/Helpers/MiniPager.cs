using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using MediaCommMvc.Web.Forum.ViewModels;
using MediaCommMvc.Web.Infrastructure;

namespace MediaCommMvc.Web.Helpers
{
    public static class UrlHelperMiniPager
    {
        public static MvcHtmlString MiniPager(this UrlHelper urlHelper,TopicOverviewViewModel topic, Func<int, string> linkBuilder)
        {
            const string FormatNormal = "<span class='topic-pager hidden-xs'><a class='btn btn-default btn-xs' href='{0}'>{1}</a></span>";
            StringBuilder pagerBuilder = new StringBuilder();

            if (!topic.ReadByCurrentUser)
            {
                pagerBuilder.AppendFormat(
                    FormatNormal,
                    urlHelper.Action(MVC.Forum.FirstNewPostInTopic().AddRouteValue("topicId", topic.Id)),
                    Resources.Forums.GotoFirstNewPost);
            }

            int numberOfPages = (int)Math.Ceiling(topic.PostCount / (double)ForumOptions.TopicsPerPage);

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
