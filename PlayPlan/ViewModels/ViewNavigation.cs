using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.ViewModels
{
    public class ViewNavigation
    {
        private ViewModelBase _currentViewModel;
        private ViewModelBase _sourceViewModel;
        private MainWindowViewModel _mainWindowViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
            }
        }
        public ViewModelBase SourceViewModel
        {
            get => _sourceViewModel;
            set
            {
                _sourceViewModel = value;
            }
        }
        public MainWindowViewModel MainWindowVM
        {
            get => _mainWindowViewModel;
            set
            {
                _mainWindowViewModel = value;
            }
        }
    }
}
