using System.Web.Mvc;

using MvcThrottle;

namespace MediaCommMvc.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            var throttleFilter = new ThrottlingFilter
            {
                Policy = new ThrottlePolicy(perSecond:1, perMinute: 5)
                {
                    IpThrottling = true
                },
                Repository = new CacheRepository()
            };

            filters.Add(throttleFilter);
        }
    }
}
