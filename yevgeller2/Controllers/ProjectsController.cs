using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using yevgeller2.Models;
using yevgeller2.ViewModels;

namespace yevgeller2.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        ApplicationDbContext _db;
        string userId;

        public ProjectsController()
        {
            _db = new ApplicationDbContext();

            if (User is null) //something fishy is going on here: User is not initialized even when the user is authenticated
            {
                var user = System.Threading.Thread.CurrentPrincipal;
                if (user != null)
                {
                    userId = user.Identity.GetUserId();
                }
            }
            else
            {
                userId = User.Identity.GetUserId();// ?? "temp user Id"; //somehow it works in the ProjectsApiController, but not here
            }

            if (userId == null) userId = "temp user Id";
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Project> projects = _db.Projects
                .Include(a => a.Tags)
                .ToList();

            ProjectViewViewModel pvvm = new ProjectViewViewModel
            {
                Projects = projects.Where(p=>p.IsHidden == false).ToList(),
                IsAuthenticated = User.Identity.IsAuthenticated,
                HiddenProjects = projects.Where(p=>p.IsHidden).ToList()
            };

            return View(pvvm);
        }

        [Authorize]
        public ActionResult Create()
        {
            ProjectAndTagsViewModel patvm = CreateProjectAndTagsViewModelForEntry();

            return View(patvm);
        }

        private ProjectAndTagsViewModel CreateProjectAndTagsViewModelForEntry()
        {
            Random r = new Random();
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
                ProjectTags = allTags,
                AllTags = allTags,
                TagsSelectItems = tagsChoice,
                IdNo = r.Next(1, System.Int32.MaxValue - 1)
            };
            return patvm;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(ProjectAndTagsViewModel patvm)
        {
            if (!ModelState.IsValid || patvm.IdNo == 0)// || patvm.SelectedTags == null)
            {
                ProjectAndTagsViewModel pvm = CreateProjectAndTagsViewModelForEntry();
                return View(pvm);
            }
            List<Tag> selectedTags = new List<Tag>();
            List<Tag> existingTags = _db.Tags.ToList();

            //first, figure out new candidate tags
            if (!String.IsNullOrWhiteSpace(patvm.CandidateTags))
            {
                bool newTagsAdded = false;
                List<string> candidateTags = patvm.CandidateTags.Split(';').ToList();
                foreach (string candidate in candidateTags)
                {
                    if (candidate.Trim().Length > 0)
                    {
                        Tag mayBe = existingTags
                            .Where(x => x.Name.Equals(candidate, System.StringComparison.InvariantCultureIgnoreCase))
                            .FirstOrDefault();

                        if (mayBe == null)
                        {
                            Tag newTag = new Tag { Name = candidate };
                            //                    _db.Tags.Add(new Tag { Name = candidate });
                            selectedTags.Add(newTag);
                            _db.Tags.Add(newTag);
                            newTagsAdded = true;
                        }
                    }
                }
                if (newTagsAdded) _db.SaveChanges();
            }
            //then get selected tags that already existi
            List<Tag> selectedExistingTags = _db.GetExistingSelectedTags(userId, patvm.IdNo).ToList();
            //merge them together
            List<Tag> selectedTagsReadyForPosting = selectedTags
                .Union(selectedExistingTags)
                .ToList();
            //construct new Project object
            var newProject = new Project
            {
                Name = patvm.Project.Name,
                Description = patvm.Project.Description,
                Technology = patvm.Project.Technology,
                Url = patvm.Project.Url,
                Year = patvm.Project.Year,
                Tags = selectedTagsReadyForPosting
            };

            _db.Projects.Add(newProject);
            _db.SaveChanges();

            return RedirectToAction("Index", "Projects");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Random r = new Random();

            Project project = _db.Projects.Where(x => x.Id == id).FirstOrDefault();

            List<Tag> projectTags = _db.Projects
                .Where(x => x.Id == id)
                .SelectMany(p => p.Tags)
                .ToList();

            ProjectAndTagsViewModel patvm = CreateProjectAndTagsViewModelForEntry();

            patvm.Project = project;
            patvm.ProjectTags = projectTags;
            patvm.AllTags = _db.Tags.ToList();

            return View(patvm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, ProjectAndTagsViewModel patvm)
        {
            List<Tag> allTags = _db.Tags.ToList();
            //find the project
            Project thisProject = _db.Projects.Where(x => x.Id == id).FirstOrDefault();

            if (thisProject == null)
                throw new ArgumentNullException("Project");

            List<Tag> thisProjectsTags_old = thisProject.Tags.ToList();
            List<Tag> thisProjectsTags_new = new List<Tag>(); //make a clean list of tags
            bool flag = false;

            //First, let's deal with the typed in tags
            if (!String.IsNullOrWhiteSpace(patvm.CandidateTags))
            {
                List<string> typedInCandidateTagStrings = patvm
                    .CandidateTags
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                foreach (string s in typedInCandidateTagStrings)
                {
                    if (s.Trim().Length > 0)
                    {
                        Tag t = new Tag { Name = s.Trim() };
                        if (!allTags.Contains(t))
                        {
                            Tag newlyAddedTag = _db.Tags.Add(t);
                            thisProjectsTags_new.Add(newlyAddedTag);
                            flag = true;
                        }
                        else
                        {
                            Tag newlyAddedTag = allTags
                                .Where(x => x.Name.ToLowerInvariant().Equals(t.Name.ToLowerInvariant()))
                                .FirstOrDefault();
                            if (newlyAddedTag != null)
                            {
                                thisProjectsTags_new.Add(newlyAddedTag);
                            }
                        }
                    }
                }

                if (flag) _db.SaveChanges();
            }

            //now let's deal with the tags changed by clicking buttons
            //Get the changed tags, compare with the see if the user changed any labels
            List<string> changedTagsByButtonClicking = _db.TempStorageTags
                .Where(t => t.IdNo == patvm.IdNo)
                .GroupBy(x => x.Name)
                .Where(c => c.Count() % 2 == 1)
                .Select(c => c.Key)
                .ToList();

            foreach (string s in changedTagsByButtonClicking)
            {
                Tag t = allTags
                    .Where(x => x.Name.ToLowerInvariant().Equals(s.Trim().ToLowerInvariant()))
                    .FirstOrDefault();

                if (t != null)
                {
                    if (!thisProjectsTags_old.Contains(t))
                    {
                        thisProjectsTags_new.Add(t);
                    }
                    else //test from here
                    {
                        thisProjectsTags_old.Remove(t);
                    }
                }
            }

            foreach(Tag t in thisProjectsTags_old)
            {
                if (!thisProjectsTags_new.Contains(t))
                    thisProjectsTags_new.Add(t);
            } //to here

            thisProject.Name = patvm.Project.Name;
            thisProject.Technology = patvm.Project.Technology;
            thisProject.Url = patvm.Project.Url;
            thisProject.Year = patvm.Project.Year;
            thisProject.Description = patvm.Project.Description;
            thisProject.Tags = thisProjectsTags_new;

            //save changes
            _db.SaveChanges();
            return RedirectToAction("Index", "Projects");

        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit2(int id, ProjectAndTagsViewModel patvm)
        {
            List<Tag> allTags = _db.Tags.ToList();

            //Get the typed tags
            List<string> typedInTags = new List<string>();
            if (patvm.CandidateTags != null)
                typedInTags = patvm.CandidateTags.Split(';').ToList();

            //Get the changed tags, compare with the see if the user changed any labels
            List<string> changedTagsByButtonClicking = _db.TempStorageTags
                .Where(t => t.IdNo == patvm.IdNo)
                .GroupBy(x => x.Name)
                .Where(c => c.Count() % 2 == 1)
                .Select(c => c.Key)
                .ToList();

            List<string> potentialNewTags = typedInTags.Union(changedTagsByButtonClicking).ToList();
            List<Tag> tagsChangedForThisProject = new List<Tag>(); //use this as the base for the projectTags

            //check if any tags are new
            bool newTagsRecorded = false;
            foreach (string candidateNewTag in potentialNewTags)
            {
                if (!String.IsNullOrWhiteSpace(candidateNewTag))
                {
                    Tag t = new Tag { Name = candidateNewTag.Trim() };
                    tagsChangedForThisProject.Add(t);

                    if (!allTags.Contains(t))
                    {
                        _db.Tags.Add(t);
                        newTagsRecorded = true;
                    }
                }
            }

            //if new tags found, save those
            if (newTagsRecorded)
            {
                _db.SaveChanges();
            }

            allTags = _db.Tags.ToList(); //refresh tags

            //update project properties
            Project p = _db.Projects.Where(x => x.Id == id).FirstOrDefault();
            if (p != null)
            {
                List<Tag> tags = p.Tags.ToList();
                /*if the Tag is in the projectTags, it needs to be removed
                  since the user clicked it. If it is not there, it is new and needs
                  to be added */
                foreach (Tag t in tagsChangedForThisProject)
                {
                    if (tags.Contains(t)) 
                    {
                        tags.Remove(t);
                    }
                    else
                    {
                        Tag existingTagWithTheSameName = allTags
                            .Where(x => x.Name.Trim().ToLower()
                                .CompareTo(t.Name.Trim().ToLower()) == 0)
                            .FirstOrDefault();
                        if (existingTagWithTheSameName != null)
                            tags.Add(allTags.Where(x => x.Name == t.Name).First());
                    }
                }

                foreach (Tag t in p.Tags)
                    if (t.Id == 0)
                        throw new Exception("new tag");

                p.Name = patvm.Project.Name;
                p.Technology = patvm.Project.Technology;
                p.Url = patvm.Project.Url;
                p.Year = patvm.Project.Year;
                p.Description = patvm.Project.Description;
                p.Tags = tags;
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Projects");
        }

        [Authorize]
        [HttpPost]
        public void HideProject(int id)
        {
            Project p = _db.Projects
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if(p!=null)
            {
                p.IsHidden = !p.IsHidden;
            }

            _db.SaveChanges();

            //return RedirectToAction("Index", "Projects");
        }

        [AllowAnonymous]
        public ActionResult FunTimer()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _db = null;
            base.Dispose(disposing);
        }
    }
}