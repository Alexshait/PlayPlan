﻿using PlayPlan.DataModel;
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
        void CommentAddNew(TopicComment topicComment);
        void CommentRemove(int commentID);
        IEnumerable<TopicComment> GetComments(int topicID);
        int CommentsAmount(int topicID);
        IEnumerable<TopicComment> GetTopicCommentsFiltered(DateTime dateTime);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<IEnumerable<SettingsData>> GetSettingsDataAsync();
        void PersonAddNew(Person person);
        void PersonRemove(Person person);
        void SettingsSave(SettingsData settingsData);
        string GetAuthUrl();
        public SettingsData GetSettingsData();

    }
}
