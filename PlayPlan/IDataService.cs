using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan
{
    public interface IDataService
    {
        IEnumerable<Topic> GetTopicsAll();
        Task SaveTopicCommentsAsync(IEnumerable<TopicComment> topicComments, DateTime dateTime);
        Task RemoveTopicCommentsAsync(DateTime dateTime);
        IEnumerable<TopicComment> GetTopicCommentsFiltered(DateTime dateTime);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<IEnumerable<SettingsData>> GetSettingsDataAsync();
        void PersonAddNew(Person person);
        void PersonRemove(Person person);
        void SettingsSave(SettingsData settingsData);
        string GetAuthUrl();
        public SettingsData GetSettingsData();
        void SaveTopics(IEnumerable<Topic> topics);

        public Task<List<TopicComment>> GetCommentsAsync(int topicID, DateTime commentDate);
        public Task<List<ExcelReport>> ExcelReportAsync(DateTime dateFrom, DateTime dateTo);

    }
}
