using System.Web.Mvc;

namespace MediaCommMvc.Web.Areas.DummyT4Mvc
{
    public class DummyT4MvcAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DummyT4Mvc";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DummyT4Mvc_default",
                "DummyT4Mvc/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}