using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayPlan.ViewModels;
using PlayPlan.DataModel;
using System.Windows.Input;
using PlayPlan.Commands;
using PlayPlan.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;
using System.Threading;

namespace PlayPlan.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private ViewNavigation _viewNavigation;
        private IDataService _ds;
        private ObservableCollection<Person> _persons;
        private bool _isLoading;

        public ObservableCollection<Person> Persons => _persons;
        public Person SelectedPerson { get; set; }
        public string ApiID { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string ApiVer { get; set; }
        public string ApiUrl { get; set; }
        public bool IsLoading
        {
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
        public SettingsViewModel(ViewNavigation viewNavigation, IDataService ds)
        {
            IsLoading = true;
            _viewNavigation = viewNavigation;
            _ds = ds;
            SettingsCancelBtnCmd = new DelegateCommand(o => this.RunSettingsCancelBtnCmd());
            SettingsAddBtnCmd = new DelegateCommand(o => this.RunSettingsAddBtnCmd());
            SettingsRemoveBtnCmd = new DelegateCommand(o => this.RunSettingsRemoveBtnCmd());
            SettingsSaveBtnCmd = new DelegateCommand(o => this.RunSettingsSaveBtnCmd());

            //var ts = _ds.GetAllPersonsAsync();
            //_persons = new ObservableCollection<Person>(_ds.GetAllPersonsAsync().Result);
            RunGetAllPersonsAsync(_ds);
            RunGetSettingsDataAsync(_ds);

            if (_viewNavigation.SourceViewModel is MainViewModel viewModel)
            {
                viewModel.IsLoading = false;
            }
        }

        private void RunGetAllPersonsAsync(IDataService ds)
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

        private void RunGetSettingsDataAsync(IDataService ds)
        {
            //CancellationTokenSource ct = new CancellationTokenSource();
            //ct.CancelAfter(5000);
            Task.Run(() =>
            {
                Task<IEnumerable<SettingsData>> task = _ds.GetSettingsDataAsync();
                task.ContinueWith(t =>
                {
                    try
                    {
                        var settingData = t.Result.FirstOrDefault();
                        if (settingData != null)
                        {
                            ApiID = settingData.ApiID.ToString();
                            GroupID = settingData.GroupID.ToString();
                            GroupName = settingData.GroupName;
                            ApiVer = settingData.VkApiVer;
                            ApiUrl = settingData.ApiUrl;
                        }
                        OnPropertyChanged(nameof(ApiID));
                        OnPropertyChanged(nameof(GroupID));
                        OnPropertyChanged(nameof(GroupName));
                        OnPropertyChanged(nameof(ApiVer));
                        OnPropertyChanged(nameof(ApiUrl));
                        //IsLoading = false;
                    }
                    catch (Exception ex)
                    {
                    MessageBox.Show(ex.Message);
                    }
                    
                });
            }
            );
        }

        public ICommand SettingsCancelBtnCmd { get; private set; }
        public ICommand SettingsAddBtnCmd { get; private set; }
        public ICommand SettingsRemoveBtnCmd { get; private set; }
        public ICommand SettingsSaveBtnCmd { get; private set; }

        private void RunSettingsCancelBtnCmd()
        {
            if (_viewNavigation.SourceViewModel is MainViewModel vm)
            {
                vm.RunGetAllPersonsAsync();
            }
            _viewNavigation.MainWindowVM.CurrentViewModel = _viewNavigation.SourceViewModel;
            this.Dispose();
        }
        private void RunSettingsAddBtnCmd()
        {
            var AddDialog = new PersonAdd();
            var vm = new PersonAddViewModel(_ds);
            vm.OnRequestClose += (s, e) => AddDialog.Close();
            vm.OnUpdateListView += (s, e) => _persons.Add((Person)s);
            AddDialog.DataContext = vm;
            AddDialog.ShowDialog();
        }
        private void RunSettingsRemoveBtnCmd()
        {
            if ( SelectedPerson != null)
            {
                var respone = MessageBox.Show($"Вы действительно намереваетесь удалить организатора {SelectedPerson.PersonName}?", 
                                              "Подтвердить действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (respone == MessageBoxResult.Yes)
                {
                    _ds.PersonRemove(SelectedPerson);
                    _persons.Remove(SelectedPerson);
                }
            }
            else
            {
                MessageBox.Show("Выберите организатора для удаления из списка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            };
        }
        private void RunSettingsSaveBtnCmd()
        {
            if (int.TryParse(ApiID, out _) && int.TryParse(GroupID, out _) && GroupName != null && ApiVer != null && ApiUrl !=null)
            {
                var settingsData = new SettingsData()
                {
                    ID = 0,
                    ApiID = int.Parse(ApiID),
                    GroupID = int.Parse(GroupID),
                    GroupName = GroupName,
                    VkApiVer = ApiVer,
                    ApiUrl = ApiUrl
                };
                _ds.SettingsSave(settingsData);
                MessageBox.Show("Настройки сохранены.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Введены некорректные данные. ID приложения и ID группы должны содержать только цифры. \n" +
                    "Имя группы, версия API, API URL не должны быть пустыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
