using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yevgeller2.Models
{
    public class MyProjectTag
    {
        [Key]
        [Column(Order=1)]
        public int MyProjecTId { get; set; }
        public MyProject MyProject { get; set; }

        [Key]
        [Column(Order =2)]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}