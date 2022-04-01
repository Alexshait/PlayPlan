using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Net.Http;
using System.Diagnostics;

namespace PlayPlan
{
    public class VkAuthorization : IAuthorization
    {
        private static VkAuthorization _instance;

        private readonly IDataService _ds;
        private string _authUrl;
        private bool _authorizationIsSuccess = false;
        protected VkAuthorization(IDataService ds)
        {
            _ds = ds;
            _authUrl = _ds.GetAuthUrl();
        }

        private string _accessToken;
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; } }

        private DateTime _accessTokenExpiration;

        public DateTime AccessTokenExiration
        {
            get { return _accessTokenExpiration; }
            set { _accessTokenExpiration = value; }
        }
        public string AuthUrl { get { return _authUrl; } }
        public string ResponseAuthUrl 
        { 
            set 
            { 
                try
                {
                    string TokenMarker = "#access_token=";
                    string[] SplitedStr = value.Substring(value.LastIndexOf(TokenMarker) + TokenMarker.Length).Split('&');
                    AccessToken = SplitedStr[0];
                    string ExpiresMarker = "expires_in=";
                    int ExpireInSec = int.Parse(SplitedStr[1].Substring(ExpiresMarker.Length));
                    AccessTokenExiration = DateTime.Now.AddSeconds(ExpireInSec);
                    _authorizationIsSuccess = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    _authorizationIsSuccess = false;
                }
            }
        }

        public bool AuthorizationIsSuccess 
        { 
            get 
            {
                if (_authorizationIsSuccess && DateTime.Now <= AccessTokenExiration)
                {
                    return true;
                }
                else
                {
                    _authorizationIsSuccess = false ;
                    return false;
                }
            }
            set 
            {
                _authorizationIsSuccess = value;
            } 
        }

        public static VkAuthorization GetInstance(IDataService ds)
        {
            if (_instance == null) _instance = new VkAuthorization(ds);
            return _instance;
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
