using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
                string allTags = String.Join("; ", Tags);
                return allTags.Substring(0, allTags.Length - 1);
            }
        }
    }
}