using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan
{
    internal class Authorize : IAuthorization
    {
        private ISettings _setting;
        public Authorize(ISettings setting)
        {
            _setting = setting;
        }

        public string AccessToken => throw new NotImplementedException();

        public string GetAccessToken()
        {
            throw new NotImplementedException();
        }

        public string GetUrlAuth(SettingsData setting)
        {
            return $"{setting.ApiUrl}authorize?client_id={setting.ApiID}&display=page&scope=friends&response_type=token&{setting.VkApiVer}";
        }
    }
}
