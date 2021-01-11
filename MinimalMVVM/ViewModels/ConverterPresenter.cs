using System.Collections.ObjectModel;
using System.Windows.Input;
using MinimalMVVM.Models;
using MinimalMVVM.ViewModels.MVVM;

namespace MinimalMVVM.ViewModels
{
    internal sealed class ConverterPresenter : Presenter
    {
        private readonly ConverterModel _model;
        private string _someText = string.Empty;

        public ConverterPresenter(ConverterModel model, ObservableCollection<string> history) =>
            (_model, History) = (model, history);

        public string SomeText
        {
            get => _someText;
            set => Update(ref _someText, value);
        }

        public ObservableCollection<string> History { get; }

        public ICommand ConvertTextCommand => new Command(_ => _model.ConvertText(SomeText, OnUpdate));

        private void OnUpdate() => SomeText = string.Empty;
    }
}