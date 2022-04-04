using System;
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
            for (int i = 0; i < items.Count(); i++)
            {
                var topicItem = new Topic();
                topicItem.TopicID = items[i].id;
                topicItem.TopicTitle = items[i].title;
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                topicItem.TopicDateCreate = dateTime.AddSeconds(items[i].created).ToLocalTime();
                topicItem.TopicDateUpdate = dateTime.AddSeconds(items[i].updated).ToLocalTime();
                topicItem.TopicCreatedBy = items[i].created_by;
                topicItem.TopicUpdatedBy = items[i].updated_by;
                topicItem.TopicCommentsAmount = items[i].comments;

                result.Add(topicItem);
            }
            return result;
        }

        public static IEnumerable<TopicComment> MapComments(DsComments.Item[] items)
        {
            var result = new List<TopicComment>();
            for (int i = 0; i < items.Count(); i++)
            {
                var CommentItem = new TopicComment();
                CommentItem.Topic_ID = items[i].id;
                CommentItem.Comment = items[i].text;
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                CommentItem.DateComment = dateTime.AddSeconds(items[i].date).ToLocalTime();
                CommentItem.PersonID = items[i].from_id;

                result.Add(CommentItem);
            }
            return result;
        }
    }
}
