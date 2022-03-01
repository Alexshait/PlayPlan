using PlayPlan.DataModel;
using System;
using System.Collections.Generic;

namespace PlayPlan
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

        public IEnumerable<string> GetAllTrailners()
        {
            return new List<string>()
            {
                "Бтрюков Алексей",
                "Пирогов Сергей"
            };
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

        public void TrainerAddNew(Person person)
        {
            throw new System.NotImplementedException();
        }

        public void TrainerRemove(Person person)
        {
            throw new System.NotImplementedException();
        }
    }
}