using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using System.Threading;

namespace PlayPlan
{
    internal class AuthorizationInfo : IAuthorization
    {
        private TimeSpan AuthorizationTimeOut = TimeSpan.FromSeconds(5);
        private readonly IDataService _setting;
        public string url;
        public AuthorizationInfo(IDataService setting)
        {
            _setting = setting;
            url = GetUrlAuth();
        }

        public string AccessToken;

        public async Task<string> GetAccessToken()
        {
            string urlAuth = GetUrlAuth();
            //var result = await RequestTokenAsync(urlAuth).WaitAsync(AuthorizationTimeOut);
            AccessToken = " ";
            return string.Empty;
        }

        public string GetUrlAuth()
        {
            return $"{_setting.GetVKUrl()}authorize?client_id={_setting.GetApiId()}&display=page&response_type=token&{_setting.GetVer()}";
        }

        //private async Task<string> RequestTokenAsync(string url, CancellationToken cancelToken = default(CancellationToken))
        //{
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        var response = await httpClient.GetAsync(url);
        //        var tt = await response.Content.ReadAsStringAsync();
        //        return tt;
        //    }
        //}
    }
}
