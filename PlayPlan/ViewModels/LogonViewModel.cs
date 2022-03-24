using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PlayPlan.ViewModels
{
    public class LogonViewModel : ViewModelBase
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
                    //RaisePropertyChanged("WebAddress");
                    OnPropertyChanged("WebAddress");
                }
            }
        }
        //public void RaisePropertyChanged(string propertyName)
        //{
        //    OnPropertyChanged(propertyName);
        //}


    }
}
