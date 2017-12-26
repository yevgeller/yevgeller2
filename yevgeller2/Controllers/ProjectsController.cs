using System.Collections.Generic;
using System.Data.Entity;
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
            var myProjects = _db.Projects.Include(p => p.Tags);
            return View(myProjects);
        }

        public ActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if(!ModelState.IsValid)
            {
                //return here the same stuff as it is for the Create when it's encountered the first time
            }

            var newProject = new Project
            {
                Name = project.Name,
                Description = project.Description,
                Technology = project.Technology,
                Url = project.Url,
                Year = project.Year,
                Tags = new List<Tag> { new Tag { Name = "C#" }, new Tag { Name = "ASP.Net" } }
            };

            _db.Projects.Add(newProject);
            _db.SaveChanges();

            return RedirectToAction("Index", "Projects");
        }

        protected override void Dispose(bool disposing)
        {
            _db = null;
            base.Dispose(disposing);
        }
    }
}