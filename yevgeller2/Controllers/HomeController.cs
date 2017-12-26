using System.Web.Mvc;
using yevgeller2.Models;

namespace yevgeller2.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Here is how you can get a hold of me";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _db = null;
            base.Dispose(disposing);
        }
    }
}