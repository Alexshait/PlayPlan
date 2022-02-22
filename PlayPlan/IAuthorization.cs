using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan
{
    internal interface IAuthorization
    {
        public string AccessToken { get; }
        string GetUrlAuth(SettingsData setting);

        string GetAccessToken();

    }
}
