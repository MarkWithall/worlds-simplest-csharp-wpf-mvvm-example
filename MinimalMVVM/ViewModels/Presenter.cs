using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MinimalMVVM.ViewModels
{
    public abstract class Presenter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChangedEvent([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}