using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MinimalMVVM.ViewModels.MVVM
{
    public abstract class Presenter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Update<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                RaisePropertyChangedEvent(propertyName);
            }
        }

        protected void RaisePropertyChangedEvent([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}