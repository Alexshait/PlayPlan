using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataModel
{
    public class ExcelReport
    {
 
        public DateTime DateComment { get; set; }
        public string PersonName { get; set; }
        public string TopicTitle { get; set; }
        public string CommentFrom { get; set; }
        public string Participant { get; set; }
        public string Comment { get; set; }
        public DateTime DateInput { get; set; }
    }
}
