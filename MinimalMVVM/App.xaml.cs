using System.Collections.ObjectModel;
using System.Windows;
using MinimalMVVM.Models;
using MinimalMVVM.ViewModels;
using MinimalMVVM.Views;

namespace MinimalMVVM
{
    internal sealed partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var history = new ObservableCollection<string>();
            var model = new ConverterModel(s => s.ToUpper(), history);
            var converterPresenter = new ConverterPresenter(model, history);
            var mainWindow = new ConvertWindow {DataContext = converterPresenter};

            mainWindow.Show();
        }
    }
}