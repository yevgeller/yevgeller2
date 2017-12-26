using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace yevgeller2.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Year { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        [MaxLength(128)]
        public string Technology { get; set; }

        public string Url { get; set; }

        public List<Tag> Tags { get; set; }

        public string TagsForDisplay
        {
            get
            {
                if (Tags == null)
                    return "no tags";

                string result = string.Empty;

                foreach (string name in Tags
                    .OrderBy(x => x.Name)
                    .Select(x => x.Name))
                {
                    result += name + "; ";
                }
                return result.Substring(0, result.Length - 1);
            }
        }
    }
}