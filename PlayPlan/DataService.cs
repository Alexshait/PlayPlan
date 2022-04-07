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
        public void CommentAddNew(TopicComment topicComment)
        {
            throw new NotImplementedException();
        }

        public void CommentRemove(int commentID)
        {
            throw new NotImplementedException();
        }

        public int CommentsAmount(int topicID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            var result = new List<Person>();
            using (var db = new PlayPlanContext())
            {
                var qry = await db.Persons.ToListAsync();
                if (qry != null)
                {
                    result = qry;
                }
            }
            return result;
        }

        //public IEnumerable<Person> GetAllPersons()
        //{
        //    var result =  new List<Person>();
        //    using (var db = new PlayPlanContext())
        //    {
        //        IQueryable<Person> qry = db.Persons;
        //        result = qry.ToList();
        //    }
        //    return result;
        //}


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

        public IEnumerable<TopicComment> GetComments(int topicID)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SettingsData>> GetSettingsDataAsync()
        {
            var result = new List<SettingsData>();
            using (var db = new PlayPlanContext())
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
            throw new NotImplementedException();
        }

        public void PersonAddNew(Person person)
        {
            using (var db = new PlayPlanContext())
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }

        public void PersonRemove(Person person)
        {
            using (var db = new PlayPlanContext())
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
        }

        public void SettingsSave(SettingsData settingsData)
        {
            using (var db = new PlayPlanContext())
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
    }
}
