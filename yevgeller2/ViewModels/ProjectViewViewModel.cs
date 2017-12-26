using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yevgeller2.ViewModels
{
    public class ProjectViewViewModel
    {
        public string Year { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public string Url { get; set; }
        public string TagsForDisplay { get; set; }
    }
}