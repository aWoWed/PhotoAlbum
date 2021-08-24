using System.Web.Mvc;

namespace Photo_album.Controllers
{
    public class User : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}