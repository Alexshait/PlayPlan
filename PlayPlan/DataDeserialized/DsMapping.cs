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

                result.Add(topicItem);
            }
            return result;
        }

        public static IEnumerable<TopicComment> MapComments(DsTopicComments dsTopic)
        {
            var result = new List<TopicComment>();
            foreach (DsComments.Item commentItem in dsTopic.Items)
            {
                var NewComment = new TopicComment();
                NewComment.Topic_ID = dsTopic.TopicId;
                NewComment.Comment = commentItem.text;
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                NewComment.DateComment = dateTime.AddSeconds(commentItem.date).ToLocalTime();
                NewComment.PersonID = commentItem.from_id;
                NewComment.CommentID = commentItem.id;
                result.Add(NewComment);
            }
            return result;
        }
    }
}
