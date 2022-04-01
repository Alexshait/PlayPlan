using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using PlayPlan.Commands;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Diagnostics;

namespace PlayPlan.ViewModels
{
    public class LogonViewModel : ViewModelBase
    {
        private ViewNavigation _viewNavigation;
        VkAuthorization _vkAuthorization;
        private string _webAddress;

        public event EventHandler UrlUpdated;

        public LogonViewModel(ViewNavigation viewNavigation, VkAuthorization vkAuthorization)
        {
            _viewNavigation = viewNavigation;
            _vkAuthorization = vkAuthorization;
            _webAddress = vkAuthorization.AuthUrl;

            BackBtn = new DelegateCommand(o => this.RunBackBtn());
        }

        public string WebAddress
        {
            get { return _webAddress; }
            set
            {
                if (_webAddress != value)
                {
                    _webAddress = value;
                    OnPropertyChanged("WebAddress");
                    string TokenMarker = "#access_token=";
                    if (_webAddress.Contains(TokenMarker))
                    {
                        _vkAuthorization.ResponseAuthUrl = _webAddress;
                        Debug.WriteLine(_webAddress);
                        UrlUpdated?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public ICommand BackBtn { get; private set; }

        private void RunBackBtn()
        {
            _viewNavigation.MainWindowVM.CurrentViewModel = _viewNavigation.SourceViewModel;
            this.Dispose();
        }

    }
}
