using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PlayPlan.Commands;
using PlayPlan.DataModel;

namespace PlayPlan.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewNavigation _viewNavigation;
        private IDataService _ds;
        private bool _isLoading;
        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;
        public MainViewModel(ViewNavigation viewNavigation, IDataService ds)
        {
            IsLoading = true;
            //topicComment = _topicComment;
            _viewNavigation = viewNavigation;
            _ds = ds;
            SettingsBtnCmd = new DelegateCommand(o => this.OpenSettingCmd());
            DownLoadBtnCmd = new DelegateCommand(o => this.RunDownLoadBtnCmd());
            AddBtnCmd = new DelegateCommand(o => this.RunAddBtnCmd());
            RemoveBtnCmd = new DelegateCommand(o => this.RunRemoveBtnCmd());
            SaveBtnCmd = new DelegateCommand(o => this.RunSaveBtnCmd());
            ExportBtnCmd = new DelegateCommand(o => this.RunExportBtnCmd());

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

        public TopicComment topicComment { get; set; }
        public ICommand SettingsBtnCmd { get; private set; }
        public ICommand DownLoadBtnCmd { get; private set; }
        public ICommand AddBtnCmd { get; private set; }
        public ICommand RemoveBtnCmd { get; private set; }
        public ICommand SaveBtnCmd { get; private set; }
        public ICommand ExportBtnCmd { get; private set; }

        private void OpenSettingCmd()
        {
            IsLoading = true;
            
            _viewNavigation.SourceViewModel = this;
            _viewNavigation.CurrentViewModel = new SettingsViewModel(_viewNavigation, _ds);
            _viewNavigation.MainWindowVM.CurrentViewModel = _viewNavigation.CurrentViewModel;
        }
        private void RunDownLoadBtnCmd()
        {
            MessageBox.Show(_selectedPerson.PersonName + " " + _selectedPersonID);
        }
        private void RunAddBtnCmd()
        {

        }
        private void RunRemoveBtnCmd()
        {

        }
        private void RunSaveBtnCmd()
        {
            RunGetAllPersonsAsync();
            OnPropertyChanged(nameof(Persons));
        }
        private void RunExportBtnCmd()
        {

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
            }
            );
        }
    }
}
