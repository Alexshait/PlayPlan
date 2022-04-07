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
            //DoubleClickCmd = new DelegateCommand(o => this.RunDoubleClickCmd());

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
                var filtredComments = _comments?.Where(t => t.Topic_ID == _selectedTopicID);
                return (filtredComments != null) ? new ObservableCollection<TopicComment>(filtredComments) : null; 
            }
            set 
            { 
                _comments = value;
                
            }
        }

        private int _selectedTopicID;
        public int SelectedTopicID
        {
            get { return _selectedTopicID; }
            set 
            {
                IsLoading = true;
                _selectedTopicID = value;
                DataApi.RunGetComments(_vkAuthorization.AccessToken, _settingsData, this, _selectedTopicID);
                OnPropertyChanged(nameof(Comments));
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
                //DataApi.RunGetComments(_vkAuthorization.AccessToken, _settingsData, this);
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
                vm.OnUpdateListView += (s, e) => _comments.Add((TopicComment)s);
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
            
        }
        private void RunExportBtnCmd()
        {

        }

        private void RunDoubleClickCmd(object sender)
        {
            var item = sender as TopicComment;
            if (item != null)
            {
                var AddDialog = new CommentAddEdit();
                var vm = new CommentAddEditViewModel(item, SelectedPerson);
                vm.OnRequestClose += (s, e) => AddDialog.Close();
                //vm.OnUpdateListView += (s, e) => OnPropertyChanged(nameof(Comments));
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

        void OnUrlUpdated(object sender, EventArgs e)
        {
            if (_viewNavigation.CurrentViewModel is LogonViewModel vm)
            {
                if (_vkAuthorization.AuthorizationIsSuccess)
                {
                    _viewNavigation.MainWindowVM.CurrentViewModel = this;
                    _viewNavigation.CurrentViewModel = this;
                    vm.Dispose();
                    Debug.WriteLine("Authize success");
                }
            }
            
        }


    }
}
