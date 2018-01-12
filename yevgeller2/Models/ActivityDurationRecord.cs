using System;
using System.Collections.Generic;

namespace yevgeller2.Models
{
    public class ActivityDurationRecord
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int DurationInSeconds { get; set; }
        public string RecordType { get; set; }
        public string UserId { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}