using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataDeserialized
{
    [DataContract]
    public class DsTopics
    {
        public class Rootobject
        {
            public Response response { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public Item[] items { get; set; }
            public int default_order { get; set; }
            public int can_add_topics { get; set; }
        }

        public class Item
        {
            public int id { get; set; }
            public string title { get; set; }
            public int created { get; set; }
            public int created_by { get; set; }
            public int updated { get; set; }
            public int updated_by { get; set; }
            public int is_closed { get; set; }
            public int is_fixed { get; set; }
            public int comments { get; set; }
        }

    }
}
