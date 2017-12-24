using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yevgeller2.Models
{
    public class MyProjectTag
    {
        [Key]
        [Column(Order=1)]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Key]
        [Column(Order =2)]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}