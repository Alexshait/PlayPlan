using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayPlan.Test
{
    internal class SettingsMock : IDataService
    {
        public SettingsMock()
        {
        }

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

        public IEnumerable<Person> GetAllPersons()
        {
            var lst = new[] { new Person() { PersonName = "Бирюков Алексей", ParsePhrases = "Бирюков"},
                              new Person() { PersonName = "Пирогов Сергей", ParsePhrases = "Пирогов" },
            };
            return lst;
        }

        public Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            throw new NotImplementedException();
        }

        public int GetApiId()
        {
            return 8073115;
        }

        public string GetAuthUrl()
        {
            return @"https://oauth.vk.com/access_token?client_id=8073115&client_secret=0gx7Of99iqrtqPLwQmUA&redirect_uri=http://1gb.ru&code=558e4a22e5ac353dcc";
        }

        public IEnumerable<TopicComment> GetComments(int topicID)
        {
            throw new NotImplementedException();
        }

        public int GetGroupID()
        {
            return 190120334;
        }

        public SettingsData GetSettingsData()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SettingsData>> GetSettingsDataAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TopicComment> GetTopicCommentsFiltered(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetTopicsAll()
        {
            throw new NotImplementedException();
        }

        public string GetVer()
        {
            return "v=5.131";
        }

        public string GetVKUrl()
        {
            return "https://oauth.vk.com/";
        }

        public void PersonAddNew(Person person)
        {
            throw new NotImplementedException();
        }

        public void PersonRemove(Person person)
        {
            throw new NotImplementedException();
        }

        public void SettingsSave(SettingsData settingsData)
        {
            throw new NotImplementedException();
        }
    }
}