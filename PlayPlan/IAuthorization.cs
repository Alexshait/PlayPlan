 using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan
{
    public interface IAuthorization
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExiration { get; set; }
        public string ResponseAuthUrl { set; }
        public bool AuthorizationIsSuccess { get; set; }
        //public static VkAuthorization GetInstance(IDataService ds);
    }
}
