using PlayPlan.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlayPlan.DataModel;
using System.Windows;

namespace PlayPlan.ViewModels
{
    public class PersonAddViewModel : ViewModelBase
    {
        private IDataService _ds;
        public PersonAddViewModel(IDataService ds)
        {
            _ds = ds;
            AddBtnCmd = new DelegateCommand(obj => this.RunAddBtnCmd());
            CancelBtnCmd = new DelegateCommand(obj => this.RunCancelBtnCmd());
        }
        public event EventHandler OnRequestClose;
        public event EventHandler OnUpdateListView;
        public string PersonName { get; set; }
        public string ParsePhrases { get; set; }

        public ICommand AddBtnCmd { get; private set; }
        public ICommand CancelBtnCmd { get; private set; }
        private void RunAddBtnCmd() 
        {
            if (PersonName != null)
            {
                var newPerson = (new Person() { PersonName = PersonName, ParsePhrases = ParsePhrases });
                _ds.PersonAddNew(newPerson);
                OnUpdateListView(newPerson, new EventArgs());
                OnRequestClose(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Поле 'Организатор' не может быть пустым!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void RunCancelBtnCmd()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
