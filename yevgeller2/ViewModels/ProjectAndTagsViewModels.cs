using System.Collections.Generic;
using System.Web.Mvc;
using yevgeller2.Models;

namespace yevgeller2.ViewModels
{
    public class ProjectAndTagsViewModel
    {
        public Project Project { get; set; }
        public List<Tag> Tags { get; set; }
        public List<SelectListItem> TagsSelectItems { get; set; }
        public IEnumerable<string> SelectedTags { get; set; }
    }
}