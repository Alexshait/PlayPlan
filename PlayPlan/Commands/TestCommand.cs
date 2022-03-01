using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlayPlan.Commands
{
    public class TestCommand : ICommand
    {
        private Action<object> execute;
        private Func<bool> canExecute;

        public TestCommand(Action<object> execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            //return this.canExecute == null || this.canExecute(parameter);
            return canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
