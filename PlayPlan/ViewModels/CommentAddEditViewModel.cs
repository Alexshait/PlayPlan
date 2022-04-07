using PlayPlan.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlayPlan.DataModel;
using System.Windows;

namespace PlayPlan.ViewModels
{
    public class CommentAddEditViewModel : ViewModelBase
    {
        private TopicComment _topicComment;
        public CommentAddEditViewModel(TopicComment topicComment, Person selectedPerson)
        {
            _topicComment = topicComment;
            Author = topicComment.CommentFrom;
            Comment = topicComment.Comment;
            Participant = topicComment.Participants;
            SaveBtnCmd = new DelegateCommand(obj => this.RunSaveBtnCmd());
            CancelBtnCmd = new DelegateCommand(obj => this.RunCancelBtnCmd());
        }
        public event EventHandler OnRequestClose;
        public event EventHandler OnUpdateListView;
        public string Author { get; set; }
        public string Comment { get; set; }
        public string Participant { get; set; }

        public ICommand SaveBtnCmd { get; private set; }
        public ICommand CancelBtnCmd { get; private set; }
        private void RunSaveBtnCmd() 
        {
            if (Comment != null || Participant != null)
            {
                _topicComment.CommentFrom = Author;
                _topicComment.Comment = Comment;
                _topicComment.Participants = Participant;
                OnUpdateListView(_topicComment, new EventArgs());
                OnRequestClose(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Поле 'Комментарий' или 'Участник' не может быть пустым!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void RunCancelBtnCmd()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
