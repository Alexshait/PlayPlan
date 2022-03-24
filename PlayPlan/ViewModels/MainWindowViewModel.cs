using PlayPlan.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PlayPlan.ViewModels;
using PlayPlan.Views;

namespace PlayPlan.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private ViewNavigation _navigator;
        private IDataService _ds;

        public MainWindowViewModel()
        {
            //CurrentViewModel = new LogonViewModel() { PageTitle = "LOGON",
            // WebAddress = @"https://oauth.vk.com/authorize?client_id=8073115&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.131"
            //};
            _navigator = new ViewNavigation();
            _ds = new DataService();
            _navigator.MainWindowVM = this;
            _navigator.CurrentViewModel = new MainViewModel(_navigator, _ds);
            CurrentViewModel = _navigator.CurrentViewModel;
        }

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                this.OnPropertyChanged("CurrentViewModel");
            }
        }

    }
}
