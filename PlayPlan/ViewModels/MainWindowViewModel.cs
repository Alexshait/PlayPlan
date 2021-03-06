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

        public MainWindowViewModel(string dbPath)
        {
            _titleVersion = "PlayPlan v." + typeof(App).Assembly.GetName().Version.ToString();
            _navigator = new ViewNavigation();
            _ds = new DataService(dbPath);
            _navigator.MainWindowVM = this;
            _navigator.CurrentViewModel = new MainViewModel(_navigator, _ds);
            CurrentViewModel = _navigator.CurrentViewModel;
        }

        private string _titleVersion;

        public string TitleVersion
        {
            get { return _titleVersion; }
            set { 
                _titleVersion = value;
                OnPropertyChanged(nameof(TitleVersion));
            }
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
