using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlayPlan.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> _execute;
        private Func<bool> _canExecute;
        public DelegateCommand(Action<object> execute) : this(execute, null)
        {
        }
        public DelegateCommand(Action<object> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute.Invoke();
            //return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
