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

        private static async Task<DsTopicComments> GetCommentsAsync(string accessToken, SettingsData settingsData, int topicId)
        {
            DsTopicComments result = new DsTopicComments();
            string url = GetRequestApiUrl(ApiRequest.COMMENTS, accessToken, settingsData, topicId);
            string Content;
            using (HttpClient httpClient = new HttpClient())
            {
                var Response = await httpClient.GetAsync(url);
                Content = await Response.Content.ReadAsStringAsync();
            }
            if (Content != null)
            {
                var jsonFormater = new DataContractJsonSerializer(typeof(DsComments.Rootobject));
                MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(Content));
                if (jsonFormater.ReadObject(memStream) is DsComments.Rootobject dsTComments)
                {
                    result = new DsTopicComments() { TopicId = topicId, Items = dsTComments.response.items };
                }

            }
            
            return result;
        }

        /// <summary>
        /// Getting authors of comments  by from_id
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="settingsData"></param>
        /// <param name="topicComments"></param>
        /// <returns></returns>
        private static async Task<IEnumerable<DsUsers.Response>> GetAuthorAsync(string accessToken, SettingsData settingsData, DsTopicComments topicComments)
        {
            IEnumerable<DsUsers.Response> result = null;
            int ItemsAmount = topicComments.Items.Count();
            string users_csv = null;
            foreach (DsComments.Item tc in topicComments.Items)
            {
                users_csv = (users_csv == null) ? tc.from_id.ToString() : users_csv + "," + tc.from_id.ToString();
            }
            string url = GetRequestApiUrl(ApiRequest.USER, accessToken, settingsData, users_csv);
            string Content;
            using (HttpClient httpClient = new HttpClient())
            {
                var Response = await httpClient.GetAsync(url);
                Content = await Response.Content.ReadAsStringAsync();
            }
            if (Content != null)
            {
                var jsonFormater = new DataContractJsonSerializer(typeof(DsUsers.Rootobject));
                MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(Content));
                if (jsonFormater.ReadObject(memStream) is DsUsers.Rootobject dsUser)
                {
                    result = dsUser.response.ToList();
                }
            }
            return result;
        }

        public static void RunGetComments(string accessToken, SettingsData settingsData, MainViewModel _mainViewModel, int topicId)
        {
            //CancellationTokenSource ct = new CancellationTokenSource();
            //ct.CancelAfter(5000);
            DsTopicComments dsTopicComments = null;
            Task.Run(() =>
            {
                Task<DsTopicComments> task = GetCommentsAsync(accessToken, settingsData, topicId);
                task.ContinueWith(t =>
                {
                    try
                    {
                        dsTopicComments = t.Result;
                        _mainViewModel.Comments = new ObservableCollection<TopicComment>(DsMapping.MapComments(dsTopicComments));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("RunGetComments() - GetCommentsAsync():" + ex.Message);
                    }

                }).ContinueWith(async tt =>
                {
                    try
                    {
                        var tsk = await GetAuthorAsync(accessToken, settingsData, dsTopicComments);
                        _mainViewModel.Authors = tsk;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("RunGetComments() - GetAuthorAsync():" + ex.Message);
                    }
                    finally
                    {
                        _mainViewModel.IsLoading = false;
                    }
                });
            }
            );
        }

        public static void FillAuthorNames(IEnumerable<DsUsers.Response> authors, ref ObservableCollection<TopicComment> topicComments)
        {
            foreach (var comment in topicComments)
            {
                var person = authors.Where(a => a.id == comment.UserID).FirstOrDefault();
                if (person != null)
                {
                    comment.CommentFrom = person.first_name + " " + person.last_name;
                }
                
            }
        }

        private static string GetRequestApiUrl(ApiRequest apiRewuest, string accessToken, SettingsData settingsData, Object parameter = null)
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
