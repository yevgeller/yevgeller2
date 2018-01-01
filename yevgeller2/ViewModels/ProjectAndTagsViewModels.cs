using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using yevgeller2.Models;

namespace yevgeller2.ViewModels
{
    public class ProjectAndTagsViewModel
    {
        //the Project
        public Project Project { get; set; }

        //its Tags
        public List<Tag> Tags { get; set; }

        //NOT NEEDED: this is for the dropdown
        [Display(Name ="Select one or more tags")]
        public List<SelectListItem> TagsSelectItems { get; set; }

        //NOT NEEDED: this is for the dropdown
        [Display(Name ="Select one or more tags")]
        public IEnumerable<string> SelectedTags { get; set; }

        //String to add new tags, semicolon-separated
        [Display(Name ="Other tags")]
        public string CandidateTags { get; set; }

        //This is a session identifier to store tags in the 
        //temp database, a random number
        public int IdNo { get; set; }
    }
}