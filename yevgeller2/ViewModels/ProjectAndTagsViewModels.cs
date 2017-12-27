using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using yevgeller2.Models;

namespace yevgeller2.ViewModels
{
    public class ProjectAndTagsViewModel
    {
        public Project Project { get; set; }
        public List<Tag> Tags { get; set; }

        [Display(Name ="Select one or more tags")]
        public List<SelectListItem> TagsSelectItems { get; set; }

        [Display(Name ="Select one or more tags")]
        public IEnumerable<string> SelectedTags { get; set; }
    }
}