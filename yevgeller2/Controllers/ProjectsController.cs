﻿using Microsoft.AspNet.Identity;
using System;
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
        string userId;

        public ProjectsController()
        {
            _db = new ApplicationDbContext();
            if (User is null) userId = "temp user Id";
            else userId = User.Identity.GetUserId() ?? "temp user Id"; //somehow it works in the ProjectsApiController, but not here
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
                Tags = allTags,
                TagsSelectItems = tagsChoice,
                IdNo = r.Next(1, System.Int32.MaxValue-1)
            };
            return patvm;
        }

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
            if (patvm.CandidateTags.Length > 0)
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

        public ActionResult Edit(int id)
        {
            Random r = new Random();

            Project project = _db.Projects.Where(x => x.Id == id).FirstOrDefault();
            List<Tag> projectTags = _db.Projects
                .Where(x => x.Id == id)
                .SelectMany(p => p.Tags)
                .ToList();

            ProjectAndTagsViewModel patvm = new ProjectAndTagsViewModel
            {
                Project = project, 
                Tags = projectTags, //will determine the buttons
                CandidateTags = string.Empty, //should be empty, no new tags here
                IdNo = r.Next(1, int.MaxValue-1), //need to get 
                SelectedTags = null,
                TagsSelectItems = null
            };

            return View(patvm);
        }

        [HttpPost]
        public ActionResult Edit(int id, ProjectAndTagsViewModel patvm)
        {

            return RedirectToAction("Index", "Projects");
        }

        protected override void Dispose(bool disposing)
        {
            _db = null;
            base.Dispose(disposing);
        }
    }
}