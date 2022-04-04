using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataDeserialized
{
    [Serializable]
    public class DsComments
    {
        public class Rootobject
        {
            public Response response { get; set; }
        }

        public class Response
        {
            public int count { get; set; }
            public Item[] items { get; set; }
        }

        public class Item
        {
            public int id { get; set; }
            public int from_id { get; set; }
            public int date { get; set; }
            public string text { get; set; }
        }

    }
}
