using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using Resources;

namespace MediaCommMvc.Web.Helpers
{
    public class MiniPager
    {
        public static MvcHtmlString Build(int numberOfItems, int itemsPerPage, Func<int, string> linkBuilder)
        {
            int numberOfPages = (int)Math.Ceiling(numberOfItems / (double)itemsPerPage);

            if (numberOfPages <= 1)
            {
                return MvcHtmlString.Empty;
            }

            StringBuilder pagerBuilder = new StringBuilder();

            const string FormatNormal = "<span><a class='btn btn-default btn-xs' href='{0}'>{1}</a></span>";

            for (int page = 1; page <= numberOfPages; page++)
            {
                pagerBuilder.AppendFormat(FormatNormal, linkBuilder(page), page);
            }


            return MvcHtmlString.Create(pagerBuilder.ToString());
        }
    }
}
