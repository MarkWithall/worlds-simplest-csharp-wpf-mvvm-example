using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MinimalMVVM.Models;

namespace MinimalMVVM.ViewModels
{
    /*
     * HOW IT WORKS:
     * 1. When you type into the input TextBox, you're actually setting the Presenter class's SomeText property (via data binding). Watch the debug console to see the PropertyChange events firing when you type
     * 2. When you click the "Convert" button (or press enter) you are running the Command specified in the convert button's Command property.
     *      - This is data-bound to Presenter's ConvertTextCommand, which returns an instance of DelegateCommand (see relevant region below)
     *      - So when you click the button or press enter, you are running ConvertTextCommand.Execute()
     *      - ConvertTextCommand.Execute() ends up pointing to Presenter.ConvertText()
     *          - When Presenter.ConvertText() is called, it shoves Presenter.SomeText through Presenter._textConverter.ConvertText(), which returns the converted all-caps string
     *          - Then Presenter.ConvertText() adds the converted string to Presenter._history, which is data bound (via the Presenter.History property) to the history ListBox in the XAML UI
     *
     * The contents of Presenter are best understood when read from top to bottom.
     * It's easy to miss, but the actual instance of Presenter that we use is created in XAML, not code-behind, in ConvertWindow.xaml
    */
    
    /// <summary>
    /// Viewmodel for our application, based on <see cref="ObservableObject"/> so simple properties will play nice with data binding.
    /// </summary>
    public class Presenter : ObservableObject
    {
        #region Private TextConverter Instance
        // Create an instance of TextConverter, using an anonymous method as a parameter to pass in the conversion function since it's so simple
        private readonly TextConverter _textConverter = new TextConverter(s => {
            // Uncomment to see when this runs
            //Debug.WriteLine("Anonymous method within TextConverter instance ran!");

            return s.ToUpper();
        });
        #endregion

        #region SomeText (User Input) Property
        // SomeText is the property the input TextBox is data-bound to
        // _someText is just the backend variable for SomeText
        private string _someText;
        public string SomeText
        {
            get { return _someText; }

            // This is called (because of the binding) each time the user changes the contents of the input TextBox
            set
            {
                // Set backend variable
                _someText = value;
                // Raise the property changed event for the property "SomeText"
                RaisePropertyChangedEvent("SomeText");
            }
        }
        #endregion

        #region History ObservableCollection

        // Create a backend ObservableCollection variable, which is basically a WPF Data-binding-friendly List
        // ObservableCollections implement the INotifyPropertyChanged and INotifyCollectionChanged interfaces so they play nice with bindings
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();

        // This is basically a wrapper to prevent external entities from being able to write to _history
        // The ListBox history display in the XAML is bound to this property
        public IEnumerable<string> History
        {
            get { return _history; }
        }

        #endregion

        #region AddToHistory Method
        // Add a string of text to our History ObservableCollection
        // which will consequently add it to the data-bound history display in the UI
        private void AddToHistory(string item)
        {
            // If this item doesn't already contain this item...
            if (!_history.Contains(item))
                // Add it to the private backend _history ObservableCollection, which is publicly accessible through the IEnumerable History property
                _history.Add(item);
        }
        #endregion

        #region ConvertText Command and Delegate Function

        // This method runs our SomeText property through our TextConverter instance,
        // Then adds the converted text to our History ObservableCollection (and thus the history ListBox) using AddToHistory()
        // This is the method which will be passed to an instance of the DelegateCommand class, essentially encapsulating this method in a WPF Command
        private void ConvertText()
        {
            // If this ViewModel (this class)'s SomeText property is empty, do nothing
            if (string.IsNullOrWhiteSpace(SomeText))
                return;
            // Otherwise use our private TextConverter instance to convert the text, and then add it to the History ObservableCollection
            AddToHistory(_textConverter.ConvertText(SomeText));
            // Then clear the SomeText property, which consequently clears the data-bound input TextBox
            SomeText = string.Empty;
        }

        // This is where the Command binding points on the "Convert" button in the XAML
        // This uses the DelegateCommand class to encapsulate the ConvertText method (above) in a WPF Command
        public ICommand ConvertTextCommand
        {
            get
            {
                // More under the hood info about DelegateCommand can be found in DelegateCommand.cs, but this basically slides ConvertText in as the Execute() method of the command
                // If you're not familiar with commands, you should learn them, but for now just understand that whatever function you pass to DelegateCommand's constructor
                // (in this case, ConvertText) will run when the button data-bound to this command is pressed
                return new DelegateCommand(ConvertText);
            }
        }

        #endregion
    }
}
