using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

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
            var settingData = _setting.GetSettingsData();
            if(settingData == null)
            {
                MessageBox.Show("Отсутсвуют необходимые настройки. Укажите данные в разделе 'Настройки'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return $"{settingData.ApiUrl}authorize?client_id={settingData.ApiID}&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&{settingData.VkApiVer}";
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
