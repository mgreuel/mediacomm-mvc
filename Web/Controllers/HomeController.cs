using System.Web.Mvc;

namespace MediaCommMvc.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return this.View();
        }

        public virtual ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public virtual ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}