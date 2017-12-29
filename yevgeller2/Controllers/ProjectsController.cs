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
            ProjectAndTagsViewModel patvm = CreateProjectAndTagsViewModelForEntry();

            return View(patvm);
        }

        private ProjectAndTagsViewModel CreateProjectAndTagsViewModelForEntry()
        {
            System.Random r = new System.Random();
            List<Tag> allTags = _db.Tags.OrderBy(x => x.Name).ToList();
            List<SelectListItem> tagsChoice = new List<SelectListItem>();
            SelectListGroup group1 = new SelectListGroup { Name = "group1" };
            foreach (var t in allTags)
            {
                SelectListItem tag = new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Name,
                    Selected = false,
                    Disabled = false
                    //Group = group1
                };

                tagsChoice.Add(tag);
            }

            ProjectAndTagsViewModel patvm = new ProjectAndTagsViewModel
            {
                Tags = allTags,
                TagsSelectItems = tagsChoice,
                IdNo = r.Next(System.Int32.MaxValue-1)
            };
            return patvm;
        }

        [HttpPost]
        public ActionResult Create(ProjectAndTagsViewModel patvm)
        {
            if (!ModelState.IsValid || patvm.SelectedTags == null)
            {
                ProjectAndTagsViewModel pvm = CreateProjectAndTagsViewModelForEntry();
                return View(pvm);
            }

            List<Tag> selectedTags = new List<Tag>();
            List<Tag> allTags = _db.Tags.ToList();
            //List<Tag> newTags = new List<Tag>();

            List<string> candidateTags = patvm.CandidateTags.Split(';').ToList();
            foreach(string candidate in candidateTags)
            {
                Tag mayBe = allTags
                    .Where(x => x.Name.Equals(candidate, System.StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();

                if(mayBe == null)
                {
//                    _db.Tags.Add(new Tag { Name = candidate });
                    selectedTags.Add(new Tag { Name = candidate });
                }
            }


            foreach(string sli in patvm.SelectedTags)
            {
                Tag t = allTags.Where(x => x.Name == sli).FirstOrDefault();
                if (t != null) selectedTags.Add(t);
            }

            var newProject = new Project
            {
                Name = patvm.Project.Name,
                Description = patvm.Project.Description,
                Technology = patvm.Project.Technology,
                Url = patvm.Project.Url,
                Year = patvm.Project.Year,
                Tags = selectedTags 
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