using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace yevgeller2.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Indicate year or years")]
        [MaxLength(32)]
        public string Year { get; set; }

        [Required(ErrorMessage ="Project name is required")]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Briefly describe the \"masterpiece\"")]
        [MaxLength(1024)]
        public string Description { get; set; }

        [Required(ErrorMessage ="Indicate the main technology or technologies used")]
        [MaxLength(128)]
        public string Technology { get; set; }

        public string Url { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        //fake "delete" option
        public bool IsHidden { get; set; }

        public string TagsForDisplay
        {
            get
            {
                if (Tags == null)
                    return "no tags";

                if (!Tags.Any())
                    return "no tags";

                string result = String.Join("; ", Tags.OrderBy(x => x.Name).Select(x => x.Name));

                //foreach (string name in Tags
                //    .OrderBy(x => x.Name)
                //    .Select(x => x.Name))
                //{
                //    result += name + "; ";
                //}


                return result; //.Substring(0, result.Length - 1);
            }
        }

        public string ShowStatus
        {
            get
            {
                if (IsHidden)
                    return "Hidden";
                return "Visible";
            }
        }

        public bool ShowUrl
        {
            get
            {
                if (Url.IndexOf('/') > -1) return true;

                return false;
            }
        }
    }
}