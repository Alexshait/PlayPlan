using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PlayPlan.Commands;
using PlayPlan.DataModel;
using PlayPlan.Views;
using PlayPlan.DataDeserialized;
using System.IO;
using System.Runtime.Serialization.Json;

namespace PlayPlan.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewNavigation _viewNavigation;
        private IDataService _ds;
        private bool _isLoading;
        private bool _IsChanged;
        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;
        private ObservableCollection<Topic> _topics;
        private ObservableCollection<TopicComment> _comments;
        private VkAuthorization _vkAuthorization;
        private SettingsData _settingsData;
        public MainViewModel(ViewNavigation viewNavigation, IDataService ds)
        {
            IsLoading = true;
            _viewNavigation = viewNavigation;
            _ds = ds;
            SettingsBtnCmd = new DelegateCommand(o => this.OpenSettingCmd());
            DownLoadBtnCmd = new DelegateCommand(o => this.RunDownLoadBtnCmd());
            AddBtnCmd = new DelegateCommand(o => this.RunAddBtnCmd());
            RemoveBtnCmd = new DelegateCommand(o => this.RunRemoveBtnCmd());
            SaveBtnCmd = new DelegateCommand(o => this.RunSaveBtnCmd());
            ExportBtnCmd = new DelegateCommand(o => this.RunExportBtnCmd());
            DoubleClickCmd = new DelegateCommand(new Action<object>(RunDoubleClickCmd));

            RunGetAllPersonsAsync();
            
        }

        public bool IsLoading { 
            get
            {
                return _isLoading;
            } 
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            } 
        }
        public ObservableCollection<Person> Persons => _persons;
        public Person SelectedPerson {
            get { return _selectedPerson; }
            set { _selectedPerson = value; }
        }
        private int _selectedPersonID;
        public int SelectedPersonID
        {
            get { return _selectedPersonID; }
            set { _selectedPersonID = value; }
        }

        public ObservableCollection<Topic> Topics
        {
            get { return _topics;  }
            set { _topics = value; }
        }
        public ObservableCollection<TopicComment> Comments
        {
            get 
            {
                //var filtredComments = _comments?.Where(t => t.Topic_ID == _selectedTopicID);
                return (_comments != null) ? new ObservableCollection<TopicComment>(_comments) : null; 
            }
            set 
            { 
                _comments = value;
                _IsChanged = false;
            }
        }

        private int _selectedTopicID;
        public int SelectedTopicID
        {
            get { return _selectedTopicID; }
            set 
            {
                if (_IsChanged == true)
                {
                    var MsgBoxRlt = MessageBox.Show("Вы не сохранили изменения. В случае продолжения изменеия в списке будут отменены. Продолжить?", "Не сохранены изменения", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (MsgBoxRlt == MessageBoxResult.No) return;
                }
                _IsChanged = false;
                IsLoading = true;
                _selectedTopicID = value;
                if (_dbIsChecked)
                {
                    RunGetCommentFromDbAsync(_selectedTopicID, _dateComment.Date);
                }
                else
                {                 
                    DataApi.RunGetComments(_vkAuthorization.AccessToken, _settingsData, this, _selectedTopicID);
                    OnPropertyChanged(nameof(Comments));
                }
            }
        }

        private IEnumerable<DsUsers.Response> _authors;
                    
        public IEnumerable<DsUsers.Response> Authors
        {
            get { return _authors; }
            set 
            { 
                _authors = value;
                if (_comments != null && Authors != null) DataApi.FillAuthorNames(Authors, ref _comments);
                OnPropertyChanged(nameof(Comments));
            }
        }

        private TopicComment _selectedComment;

        public TopicComment SelectedComment
        {
            get { return _selectedComment; }
            set { _selectedComment = value; }
        }

        private DateTime _dateComment = DateTime.Now;
        public DateTime DateComment
        {
            get { return _dateComment; }
            set { _dateComment = value; }
        }

        private bool _dbIsChecked;
        public bool DbIsChecked
        {
            get { return _dbIsChecked; }
            set 
            {
                if (_IsChanged == true)
                {
                    var MsgBoxRlt = MessageBox.Show("Вы не сохранили изменения. В случае продолжения изменеия в списке будут отменены. Продолжить?", "Не сохранены изменения", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (MsgBoxRlt == MessageBoxResult.No) return;
                }
                _dbIsChecked = value;
                if (_dbIsChecked)
                {
                    var topics = _ds.GetTopicsAll();
                    Topics = new ObservableCollection<Topic>(topics);
                    OnPropertyChanged(nameof(Topics));
                }
                else
                {
                    RunDownLoadBtnCmd();
                }
                _IsChanged = false;
            }
        }


        public ICommand SettingsBtnCmd { get; private set; }
        public ICommand DownLoadBtnCmd { get; private set; }
        public ICommand AddBtnCmd { get; private set; }
        public ICommand RemoveBtnCmd { get; private set; }
        public ICommand SaveBtnCmd { get; private set; }
        public ICommand ExportBtnCmd { get; private set; }
        public ICommand DoubleClickCmd { get; private set; }

        private void OpenSettingCmd()
        {
            IsLoading = true;
            
            _viewNavigation.SourceViewModel = this;
            _viewNavigation.CurrentViewModel = new SettingsViewModel(_viewNavigation, _ds);
            _viewNavigation.MainWindowVM.CurrentViewModel = _viewNavigation.CurrentViewModel;
        }
        private void RunDownLoadBtnCmd()
        {
            IsLoading = true;
            _dbIsChecked = false;
            OnPropertyChanged(nameof(DbIsChecked));
            _viewNavigation.SourceViewModel = this;
            _vkAuthorization = VkAuthorization.GetInstance(_ds);
            _settingsData = _ds.GetSettingsData();
            if (_settingsData == null)
            {
                MessageBox.Show("Отсутсвуют необходимые настройки приложения. Нажмите на кнопку 'Настройки' и внесите данные приложения.", "Требуются настройки", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (!_vkAuthorization.AuthorizationIsSuccess)
            {
                var logonViewModel = new LogonViewModel(_viewNavigation, _vkAuthorization, _ds);
                logonViewModel.UrlUpdated += OnUrlUpdated;
                _viewNavigation.CurrentViewModel = logonViewModel;
                _viewNavigation.MainWindowVM.CurrentViewModel = logonViewModel;
            }
            else
            {
                
                DataApi.RunGetTopics(_vkAuthorization.AccessToken, _settingsData, this);
            }

        }
        private void RunAddBtnCmd()
        {
            if (_selectedPerson == null)
            {
                MessageBox.Show("Необходимо выбрать организатора", "Внимание", MessageBoxButton.OK,MessageBoxImage.Exclamation);
                return;
            }
            var item = new TopicComment();
            item.Topic_ID = _selectedTopicID;
            item.PersonID = _selectedPersonID;
            item.CommentFrom = _selectedPerson.PersonName;
            if (item != null)
            {
                var AddDialog = new CommentAddEdit();
                var vm = new CommentAddEditViewModel(item, SelectedPerson);
                vm.OnRequestClose += (s, e) => AddDialog.Close();
                vm.OnUpdateListView += (s, e) => { _comments.Add((TopicComment)s); _IsChanged = true; };
                AddDialog.DataContext = vm;
                AddDialog.ShowDialog();
                OnPropertyChanged(nameof(Comments));
            }
        }
        private void RunRemoveBtnCmd()
        {
            if (SelectedComment != null)
            {
                var respone = MessageBox.Show($"Вы действительно намерены удалить запись {SelectedComment.CommentFrom}?",
                                              "Подтвердить действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (respone == MessageBoxResult.Yes)
                {
                    _comments.Remove(SelectedComment);
                    _IsChanged = true;
                    OnPropertyChanged(nameof(Comments));
                }
            }
            else
            {
                MessageBox.Show("Выберите организатора для удаления из списка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }
        private void RunSaveBtnCmd()
        {
            if (_dateComment == DateTime.MinValue)
            {
                MessageBox.Show("Необходимо указать дату комментария", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (SelectedPerson == null)
            {
                MessageBox.Show("Выберите организатора мероприятия", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //if (_IsChanged)
            //{
            if (MessageBox.Show($"Организатор {_selectedPerson.PersonName} проводит мероприятие {_dateComment.ToShortDateString()} числа. Сохранить измения участников?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
                IsLoading = true;
                foreach (var comment in _comments)
                {
                    comment.DateInput = DateTime.Now;
                }
                var commentsToChng = new List<TopicComment>(_comments);
                foreach (var comment in commentsToChng)
                {
                    comment.PersonID = _selectedPersonID;
                }
                Task.Run(() => _ds.RemoveTopicCommentsAsync(_dateComment)).ContinueWith(t =>
                {
                    _ds.SaveTopicCommentsAsync(commentsToChng, _dateComment);
                    _ds.SaveTopics(_topics);
                    _IsChanged = false;
                    IsLoading = false;
                });
            //}
        }
        private void RunExportBtnCmd()
        {
            var Dialog = new Export();
            Dialog.Owner = Application.Current.MainWindow;
            var vm = new ExportViewModel(_persons, _ds);
            vm.OnRequestClose += (s, e) => Dialog.Close();
            Dialog.DataContext = vm;
            Dialog.ShowDialog();
        }

        private void RunDoubleClickCmd(object sender)
        {
            var item = sender as TopicComment;
            if (item != null)
            {
                var AddDialog = new CommentAddEdit();
                var vm = new CommentAddEditViewModel(item, SelectedPerson);
                vm.OnRequestClose += (s, e) => AddDialog.Close();
                vm.OnUpdateListView += (s, e) => _IsChanged = true;
                //vm.OnUpdateListView += (s, e) => AddDialog.Close();
                AddDialog.DataContext = vm;
                AddDialog.ShowDialog();
                OnPropertyChanged(nameof(Comments));
            }
        }

        public void RunGetAllPersonsAsync()
        {
            //CancellationTokenSource ct = new CancellationTokenSource();
            //ct.CancelAfter(5000);
            Task.Run(() =>
            {
                Task<IEnumerable<Person>> task = _ds.GetAllPersonsAsync();
                task.ContinueWith(t =>
                {
                    try
                    {
                        _persons = new ObservableCollection<Person>(t.Result);
                        OnPropertyChanged(nameof(Persons));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        IsLoading = false;
                    }
                });
            });
        }

        public void RunGetCommentFromDbAsync(int topicID, DateTime commentDate)
        {
            //CancellationTokenSource ct = new CancellationTokenSource();
            //ct.CancelAfter(5000);
            Task.Run(() =>
            {
                Task<List<TopicComment>> task = _ds.GetCommentsAsync(topicID, commentDate);
                task.ContinueWith(t =>
                {
                    try
                    {
                        _comments = new ObservableCollection<TopicComment>(t.Result);
                        OnPropertyChanged(nameof(Comments));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        IsLoading = false;
                    }
                });
            });
        }
        void OnUrlUpdated(object sender, EventArgs e)
        {
            if (_viewNavigation.CurrentViewModel is LogonViewModel vm)
            {
                if (_vkAuthorization.AuthorizationIsSuccess)
                {
                    _viewNavigation.MainWindowVM.CurrentViewModel = this;
                    _viewNavigation.CurrentViewModel = this;
                    vm.Dispose();
                    //Debug.WriteLine("Authize success");
                }
            }
            
        }


    }
}
