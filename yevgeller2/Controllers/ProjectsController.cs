using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yevgeller2.Models;

namespace yevgeller2.Controllers
{
    public class ProjectsController : Controller
    {
        ApplicationDbContext _db;

        public ProjectsController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: Projects
        public ActionResult Index()
        {
            var myProjects = _db.MyProjects;
            return View(myProjects);
        }

        protected override void Dispose(bool disposing)
        {
            _db = null;
            base.Dispose(disposing);
        }
    }
}