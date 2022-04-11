using Microsoft.EntityFrameworkCore;
using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlayPlan
{
    internal class DataService : IDataService
    {
        private readonly string _dbPath;
        public DataService(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task RemoveTopicCommentsAsync(DateTime dateTime)
        {
            using (var db = new PlayPlanContext(_dbPath))
            {

                List<TopicComment> toRemove = db.TopicComments.Where(i => i.DateInput == dateTime).ToList();
                db.TopicComments.RemoveRange(toRemove);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            var result = new List<Person>();
            using (var db = new PlayPlanContext(_dbPath))
            {
                var qry = await db.Persons.ToListAsync();
                if (qry != null)
                {
                    result = qry;
                }
            }
            return result;
        }


        public string GetAuthUrl()
        {
            var settingData = GetSettingsDataAsync().Result.FirstOrDefault();
            if (settingData == null)
            {
                MessageBox.Show("Отсутсвуют необходимые настройки. Укажите данные в разделе 'Настройки'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return $"{settingData.ApiUrl}authorize?client_id={settingData.ApiID}&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&{settingData.VkApiVer}";
        }

        public async Task<List<TopicComment>> GetCommentsAsync(int topicID)
        {
            var result = new List<TopicComment>();
            using (var db = new PlayPlanContext(_dbPath))
            {
                var qry = await db.TopicComments.Where(i => i.Topic_ID == topicID).ToListAsync();
                if (qry != null)
                {
                    result = qry;
                }
            }
            return result;
        }

        public async Task<IEnumerable<SettingsData>> GetSettingsDataAsync()
        {
            var result = new List<SettingsData>();
            using (var db = new PlayPlanContext(_dbPath))
            {
                var qry = await db.Settings.Where(i => i.ID == 1).ToListAsync();
                if (qry != null)
                {
                    result = qry;
                }
            }
            return result;
        }

        public IEnumerable<TopicComment> GetTopicCommentsFiltered(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetTopicsAll()
        {
            using (var db = new PlayPlanContext(_dbPath))
            {
                if (db.Topics.FirstOrDefault() == null) return Enumerable.Empty<Topic>();
                return new List<Topic>(db.Topics.ToList());
            }
        }

        public void PersonAddNew(Person person)
        {
            using (var db = new PlayPlanContext(_dbPath))
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }

        public void PersonRemove(Person person)
        {
            using (var db = new PlayPlanContext(_dbPath))
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
        }

        public void SettingsSave(SettingsData settingsData)
        {
            using (var db = new PlayPlanContext(_dbPath))
            {
                var rec = db.Settings.FirstOrDefault(i => i.ID == 1);
                if (rec != null)
                {
                    rec.ApiID = settingsData.ApiID;
                    rec.GroupID = settingsData.GroupID;
                    rec.GroupName = settingsData.GroupName;
                    rec.VkApiVer = settingsData.VkApiVer;
                    rec.ApiUrl = settingsData.ApiUrl;
                }
                else
                {
                    db.Add(settingsData);
                }
                db.SaveChanges();
            }
        }

        public SettingsData GetSettingsData()
        {
            var settingData = GetSettingsDataAsync().Result.FirstOrDefault();
            if (settingData == null)
            {
                MessageBox.Show("Отсутсвуют необходимые настройки. Укажите данные в разделе 'Настройки'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return settingData;
        }

        public async Task SaveTopicCommentsAsync(IEnumerable<TopicComment> topicComments, DateTime dateTime)
        {
            foreach(var topicComment in topicComments)
            {
                topicComment.DateInput = DateTime.Now;
                topicComment.DateComment = dateTime;
            }
            using (var db = new PlayPlanContext(_dbPath))
            {
                db.TopicComments.AddRange(topicComments);
                await db.SaveChangesAsync();
            }
        }

        public void SaveTopics(IEnumerable<Topic> topics)
        {
            
            using (var db = new PlayPlanContext(_dbPath))
            {
                foreach (var topic in topics)
                {
                    var topicRec = db.Topics.Where(r => r.TopicID == topic.TopicID).FirstOrDefault();
                    if (topicRec != null)
                    {
                        topicRec.TopicTitle = topic.TopicTitle;
                        topicRec.TopicDateCreate = topic.TopicDateCreate;
                        topicRec.TopicDateUpdate = topic.TopicDateUpdate;
                        topicRec.TopicUpdatedBy = topic.TopicUpdatedBy;
                    }
                    else
                    {
                        db.Topics.Add(topic);
                    }
                    db.SaveChanges();
                }
                
            }
        }

        public async Task<List<ExcelReport>> ExcelReportAsync(DateTime dateFrom, DateTime dateTo)
        {
            List<ExcelReport> result = new List<ExcelReport>();
            using (var db = new PlayPlanContext(_dbPath))
            {
                var qry = from tc in db.TopicComments
                          join tp in db.Topics on tc.Topic_ID equals tp.TopicID
                          where tc.DateComment >= dateFrom.Date && tc.DateComment <= dateTo
                          select new ExcelReport() { 
                            DateComment = tc.DateComment,
                            TopicTitle = tp.TopicTitle,
                            Participant = tc.Participants ?? "",
                            Comment = tc.Comment ?? "",
                            CommentFrom = tc.CommentFrom,
                            DateInput = tc.DateInput 
                          };

                result = await qry.ToListAsync();
            }
            
            return result;
        }
    }
}
