using System.Collections.Generic;
using yevgeller2.Models;

namespace yevgeller2.ViewModels
{
    public class ProjectViewViewModel
    {
        public List<Project> Projects { get; set; }
        public bool IsAuthenticated { get; set; }

        //public string Year { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public string Technology { get; set; }
        //public string Url { get; set; }
        //public string TagsForDisplay { get; set; }
        //public bool IsAuthenticated { get; set; }
    }
}

