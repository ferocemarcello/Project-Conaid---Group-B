using System.Web.Mvc;

namespace WebServer.Controllers.web
{
    public class HomeController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
    }
}