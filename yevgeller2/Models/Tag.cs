using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace yevgeller2.Models
{
    [DebuggerDisplay("{Id}: {Name}")]
    public class Tag : IEquatable<Tag>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        
        public virtual ICollection<Project> Projects { get; set; }

        public bool Equals(Tag other)
        {
            int result = String.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
            if (result == 0) return true;

            return false;
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            Tag t = other as Tag;

            if (t == null) return false;
            return Equals(t);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}