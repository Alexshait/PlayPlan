using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataDeserialized
{
    public class DsTopicComments
    {
        public int TopicId { get; set; }
        public DsComments.Item[] Items { get; set; }
    }
}
