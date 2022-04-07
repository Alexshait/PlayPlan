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

        public static IEnumerable<TopicComment> MapComments(DsTopicComments dsTopic)
        {
            var result = new List<TopicComment>();
            //foreach (DsTopicComments item in items)
            //{
                var CommentItem = new TopicComment();
                CommentItem.Topic_ID = dsTopic.TopicId;
                foreach (DsComments.Item commentItem in dsTopic.Items)
                {
                    CommentItem.Comment = commentItem.text;
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    CommentItem.DateComment = dateTime.AddSeconds(commentItem.date).ToLocalTime();
                    CommentItem.PersonID = commentItem.from_id;
                    CommentItem.CommentID = commentItem.id;
                }
                result.Add(CommentItem);
            //}
            return result;
        }
    }
}
