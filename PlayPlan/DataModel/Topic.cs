using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataModel
{
    public class Topic
    {
        [Key]
        public int TopicID { get; set; }
        public string? TopicTitle { get; set; }
        public int TopicCreatedBy { get; set; }
        public int TopicUpdatedBy { get; set; }
        public DateTime TopicDateCreate { get; set; }
        public DateTime TopicDateUpdate { get; set; }
        public int TopicCommentsAmount { get; set; }
        public List<TopicComment> TopicComments { get; } = new();
    }
}
