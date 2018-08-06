using System;
using System.Windows.Input;

namespace MinimalMVVM.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter) => _action();

        public bool CanExecute(object parameter) => true;

        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}
