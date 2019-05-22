using System;
using System.Windows.Input;

namespace MinimalMVVM.ViewModels
{
    /// <summary>
    /// Extremely simple implementation of a delegate command (aka relay command).
    /// - _action : points to the "business" method that the command actually runs
    /// - Execute() : takes the object parameter required by the ICommand interface, then runs _action(). Basically a wrapper.
    /// - CanExecute() : tells whether the command can be run or not. It always returns true, because this is a really simple implementation of a relay command which essentially creates a command from a method.
    /// - CanExecuteChanged : this does nothing since CanExecute is always true, but we have to include it since it's part of the ICommand interface spec.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            // Put the Action (basically a void method) passed as a parameter into our private field _action, where it can be run by the Execute function when the command is called
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        // This essentially means when someone subscribes to this event, do nothing, since the event never needs to fire (because CanExecute is always true).
#pragma warning disable 67
        public event EventHandler CanExecuteChanged { add { } remove { } }
#pragma warning restore 67
    }
}
