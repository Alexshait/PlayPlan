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
        IEnumerable<TopicComment> GetTopicCommentsFiltered(DateTime dateTime);
        IEnumerable<string> GetAllTrailners();
        int GetGroupID();
        int GetApiId();
        string GetVer();
        string GetVKUrl();
        void TrainerAddNew(Person person);
        void TrainerRemove(Person person);

    }
}
