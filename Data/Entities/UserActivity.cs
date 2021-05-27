using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KLFGroup.Data.Entities
{
    public class UserActivity
    {
        public int Id { get; set; }
        public int Occurence { get; set; }
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public DateTime FirstOccurence { get; set; }
        public DateTime LastOccurence { get; set; }
        public string UserId { get; set; }
    }
}

