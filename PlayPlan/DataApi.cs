using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using PlayPlan.DataModel;
using PlayPlan.ViewModels;
using PlayPlan.DataDeserialized;
using System.IO;
using System.Diagnostics;
using System.Windows;

namespace PlayPlan
{
    public static class DataApi
    {
        private enum ApiRequest
        {
            TOPICS,
            USER,
            COMMENTS
        }

        private static async Task<DsTopics.Item[]> GetTopicsAsync(string accessToken, SettingsData settingsData)
        {
            DsTopics.Item[] result = null;
            string url = GetRequestApiUrl(ApiRequest.TOPICS, accessToken, settingsData);
            if (url == String.Empty) return result;
            string Content;
            using (HttpClient httpClient = new HttpClient())
            {
                var Response = await httpClient.GetAsync(url);
                Content = await Response.Content.ReadAsStringAsync();
            }
            if (Content != null)
            {
                var jsonFormater = new DataContractJsonSerializer(typeof(DsTopics.Rootobject));
                MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(Content));
                if (jsonFormater.ReadObject(memStream) is DsTopics.Rootobject dsTopics)
                {
                    result = dsTopics.response.items;
                }
            }
            return result;
        }

        public static void RunGetTopics(string accessToken, SettingsData settingsData, MainViewModel _mainViewModel)
        {
            //CancellationTokenSource ct = new CancellationTokenSource();
            //ct.CancelAfter(5000);
            Task.Run(() =>
            {
                Task<DsTopics.Item[]> task = GetTopicsAsync(accessToken, settingsData);
                task.ContinueWith(t =>
                {
                    try
                    {
                        _mainViewModel.Topics = new ObservableCollection<Topic>(DsMapping.MapTopics(t.Result));
                        _mainViewModel.OnPropertyChanged(nameof(_mainViewModel.Topics));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        _mainViewModel.IsLoading = false;
                    }

                });
            }
            );
        }

        private static string GetRequestApiUrl(ApiRequest apiRewuest, string accessToken, SettingsData settingsData, int parameter = 0)
        {
            string result = String.Empty;
            switch (apiRewuest) 
            {
                case ApiRequest.TOPICS:
                    {
                        result += @"https://api.vk.com/method/board.getTopics";
                        result += "?group_id=" + settingsData.GroupID;
                        result += "&access_token=" + accessToken;
                        result += "&v=" + settingsData.VkApiVer;
                        break;
                    }
                case ApiRequest.USER:
                    {
                        result += @"https://api.vk.com/method/users.get";
                        result += "?user_id=" + parameter.ToString();
                        result += "&access_token=" + accessToken;
                        result += "&v=" + settingsData.VkApiVer;
                        break;
                    }
                case ApiRequest.COMMENTS:
                    {
                        result += @"https://api.vk.com/method/board.getComments";
                        result += "?group_id=" + settingsData.GroupID;
                        result += "&topic_id=" + parameter.ToString();
                        result += "&access_token=" + accessToken;
                        result += "&v=" + settingsData.VkApiVer;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return result;
        }
    }
}
