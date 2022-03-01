using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan
{
    internal class DataService : IDataService
    {
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
            throw new NotImplementedException();
        }

        public int GetApiId()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TopicComment> GetComments(int topicID)
        {
            throw new NotImplementedException();
        }

        public int GetGroupID()
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

        public string GetUrl()
        {
            throw new NotImplementedException();
        }

        public string GetVer()
        {
            throw new NotImplementedException();
        }

        public string GetVKUrl()
        {
            throw new NotImplementedException();
        }

        public void TrainerAddNew(Person person)
        {
            throw new NotImplementedException();
        }

        public void TrainerRemove(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
