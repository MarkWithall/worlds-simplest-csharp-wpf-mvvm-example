using System.Collections.ObjectModel;
using System.Windows.Input;
using MinimalMVVM.Models;
using MinimalMVVM.ViewModels.MVVM;

namespace MinimalMVVM.ViewModels
{
    public class ConverterPresenter : Presenter
    {
        private readonly TextConverter _textConverter = new TextConverter(s => s.ToUpper());
        private string _someText = string.Empty;

        public string SomeText
        {
            get => _someText;
            set => Update(ref _someText, value);
        }

        public ObservableCollection<string> History { get; } = new ObservableCollection<string>();

        public ICommand ConvertTextCommand => new Command(_ =>
        {
            if (string.IsNullOrWhiteSpace(SomeText)) return;
            AddToHistory(_textConverter.ConvertText(SomeText));
            SomeText = string.Empty;
        });

        private void AddToHistory(string item)
        {
            if (!History.Contains(item))
                History.Add(item);
        }
    }
}