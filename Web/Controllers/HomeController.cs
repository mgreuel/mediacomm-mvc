using System.Web.Mvc;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return this.View();
        }
    }
}