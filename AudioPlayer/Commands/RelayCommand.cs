using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AudioPlayer.Commands
{
    public delegate void ExecuteDelegate(object parameter);
    public delegate bool CanExecuteDelegate(object parameter);

    public class RelayCommand : ICommand
    {
        private readonly ExecuteDelegate execute;
        private readonly CanExecuteDelegate canExecute;

        public RelayCommand(ExecuteDelegate execute)
            : this(execute, null)
        {
        }

        public RelayCommand(ExecuteDelegate execute,
            CanExecuteDelegate canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
