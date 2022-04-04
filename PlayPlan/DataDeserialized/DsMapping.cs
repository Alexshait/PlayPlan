﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayPlan.DataModel;
using PlayPlan.DataDeserialized;

namespace PlayPlan.DataDeserialized
{
    public static class DsMapping
    {
        public static IEnumerable<Topic> MapTopics(DsTopics.Item[] items)
        {
            var result = new List<Topic>();
            foreach (DsTopics.Item item in items)
            {
                var topicItem = new Topic();
                topicItem.TopicID = item.id;
                topicItem.TopicTitle = item.title;
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                topicItem.TopicDateCreate = dateTime.AddSeconds(item.created).ToLocalTime();
                topicItem.TopicDateUpdate = dateTime.AddSeconds(item.updated).ToLocalTime();
                topicItem.TopicCreatedBy = item.created_by;
                topicItem.TopicUpdatedBy = item.updated_by;
                topicItem.TopicCommentsAmount = item.comments;

                result.Add(topicItem);
            }
            return result;
        }

        public static IEnumerable<TopicComment> MapComments(DsComments.Item[] items)
        {
            var result = new List<TopicComment>();
            foreach (DsComments.Item item in items)
            {
                var CommentItem = new TopicComment();
                CommentItem.Topic_ID = item.id;
                CommentItem.Comment = item.text;
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                CommentItem.DateComment = dateTime.AddSeconds(item.date).ToLocalTime();
                CommentItem.PersonID = item.from_id;

                result.Add(CommentItem);
            }
            return result;
        }
    }
}