using PlayPlan.DataModel;
using System;
using System.Collections.Generic;

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
        public int GetApiId()
        {
            return 8073115;
        }

        public IEnumerable<TopicComment> GetComments(int topicID)
        {
            throw new NotImplementedException();
        }

        public int GetGroupID()
        {
            return 190120334;
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

        IEnumerable<string> IDataService.GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}