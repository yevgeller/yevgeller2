using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using yevgeller2.Models;
using yevgeller2.ViewModels;

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
            List<Project> projects = _db.Projects
                .Include(a => a.Tags)
                .ToList();

            ProjectViewViewModel pvvm = new ProjectViewViewModel
            {
                Projects = projects,
                IsAuthenticated = User.Identity.IsAuthenticated
            };

            //foreach(Project p in projects)
            //{
            //    //re-do this with Automapper or something
            //    ProjectViewViewModel projViewModelItem = new ProjectViewViewModel
            //    {
            //        Year = p.Year,
            //        Name = p.Name,
            //        Description = p.Description,
            //        Technology = p.Technology,
            //        Url = p.Url,
            //        TagsForDisplay = p.TagsForDisplay,
            //        IsAuthenticated = User.Identity.IsAuthenticated
            //    };

            //    pv.Add(projViewModelItem);
            //}
            return View(pvvm);
        }

        public ActionResult Create()
        {
            List<Tag> allTags = _db.Tags.ToList();
            List<SelectListItem> tagsChoice = new List<SelectListItem>();
            SelectListGroup group1 = new SelectListGroup { Name = "group1" };
            foreach (var t in allTags)
            {
                SelectListItem tag = new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Name,
                    Selected = false,
                    Disabled = false,
                    Group = group1
                };

                tagsChoice.Add(tag);
            }

            ProjectAndTagsViewModel patvm = new ProjectAndTagsViewModel
            {
                Tags = allTags,
                TagsSelectItems = tagsChoice

            };

            return View(patvm);
        }

        [HttpPost]
        public ActionResult Create(ProjectAndTagsViewModel patvm)
        {
            if(!ModelState.IsValid)
            {
                //return here the same stuff as it is for the Create when it's encountered the first time
            }

            var newProject = new Project
            {
                Name = patvm.Project.Name,
                Description = patvm.Project.Description,
                Technology = patvm.Project.Technology,
                Url = patvm.Project.Url,
                Year = patvm.Project.Year,
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