using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlayPlan.Commands;
using PlayPlan.DataModel;

namespace PlayPlan.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewNavigation _viewNavigation;
        private IDataService _ds;
        public MainViewModel(ViewNavigation viewNavigation, IDataService ds)
        {
            //topicComment = _topicComment;
            _viewNavigation = viewNavigation;
            _ds = ds;
            SettingsBtnCmd = new DelegateCommand(o => this.OpenSettingCmd());
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
            _viewNavigation.SourceViewModel = this;
            _viewNavigation.CurrentViewModel = new SettingsViewModel(_viewNavigation, _ds);
            _viewNavigation.MainWindowVM.CurrentViewModel = _viewNavigation.CurrentViewModel;
        }
    }
}
