using System.Collections.ObjectModel;
using System.Windows.Input;
using MinimalMVVM.Models;

namespace MinimalMVVM.ViewModels
{
    public class Presenter : ObservableObject
    {
        private readonly TextConverter _textConverter = new TextConverter(s => s.ToUpper());
        private string _someText = string.Empty;

        public string SomeText
        {
            get => _someText;
            set
            {
                _someText = value;
                RaisePropertyChangedEvent();
            }
        }

        public ObservableCollection<string> History { get; } = new ObservableCollection<string>();

        public ICommand ConvertTextCommand => new DelegateCommand(_ => ConvertText());

        private void ConvertText()
        {
            if (string.IsNullOrWhiteSpace(SomeText)) return;
            AddToHistory(_textConverter.ConvertText(SomeText));
            SomeText = string.Empty;
        }

        private void AddToHistory(string item)
        {
            if (!History.Contains(item))
                History.Add(item);
        }
    }
}