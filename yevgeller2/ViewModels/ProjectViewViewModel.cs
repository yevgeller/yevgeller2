using System.Collections.Generic;
using yevgeller2.Models;

namespace yevgeller2.ViewModels
{
    public class ProjectViewViewModel
    {
        public List<Project> Projects { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<Project> HiddenProjects { get; set; }
    }
}

