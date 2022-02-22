using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataModel
{
    internal class SettingsData
    {
        public int ID { get; set; }
        public int ApiID { get; set; }
        public int GroupID { get; set; }
        public string? TopicParsePhrases { get; set; }
        public string? VkApiVer { get; set; }
        public string? ApiUrl { get; set; }
        public string? GroupName { get; set; }
        public string? PersonFieldName { get; set; }

    }
}
