using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace yevgeller2.Models
{
    [DebuggerDisplay("{Id}: {Name}")]
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        
        public virtual ICollection<Project> Projects { get; set; }

    }
}