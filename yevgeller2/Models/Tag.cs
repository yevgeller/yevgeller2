using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace yevgeller2.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        
        public virtual ICollection<Project> Projects { get; set; }

    }
}