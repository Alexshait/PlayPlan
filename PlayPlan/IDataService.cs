using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan
{
    internal interface IDataService
    {
        IEnumerable<Topic> GetTopicsAll();
        void CommentAddNew(TopicComment topicComment);
        void CommentRemove(int commentID);
        IEnumerable<TopicComment> GetComments(int topicID);
        int CommentsAmount(int topicID);

    }
}
