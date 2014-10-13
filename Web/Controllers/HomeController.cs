using System.Web.Mvc;

namespace MediaCommMvc.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return this.View();
        }
    }
}