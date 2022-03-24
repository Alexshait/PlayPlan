using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataModel
{
    public class TopicComment
    {
        [Key]
        public int Topic_ID { get; set; }
        public DateTime DateComment { get; set; }
        public int PersonID { get; set; }
        public string? TopicFrom { get; set; }
        public string? Participants { get; set; }
        public string? Comment { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateInput { get; set; }
    }
}
