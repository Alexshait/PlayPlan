﻿using PlayPlan.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlayPlan.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _webAddress;
        public string WebAddress
        {
            get { return _webAddress; }
            set
            {
                if (_webAddress != value)
                {
                    _webAddress = value;
                    OnPropertyChanged("WebAddress");
                }
            }
        }

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new TestCommand(o => MyAction(), () => CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public void MyAction()
        {
            MessageBox.Show("test");
        }
    }
}
