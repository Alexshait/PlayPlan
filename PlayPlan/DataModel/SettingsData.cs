using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.DataModel
{
    public class SettingsData
    {
        [Key]
        public int ID { get; set; }
        public int ApiID { get; set; }
        public int GroupID { get; set; }
        public string VkApiVer { get; set; }
        public string ApiUrl { get; set; }
        public string GroupName { get; set; }
        public string WindowWidth { get; set; }
        public string WindowHeigth { get; set; }
    }
}
