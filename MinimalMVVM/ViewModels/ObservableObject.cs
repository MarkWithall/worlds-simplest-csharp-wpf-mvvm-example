using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MinimalMVVM.ViewModels
{
    /// <summary>
    /// Implements INotifyPropertyChanged interface in a simple way to make objects provide
    /// data-binding property change notifications.
    /// </summary>
    /// <remarks>
    /// To use, base a class on ObservableObject, then call RaisedPropertyChangedEvent("myPropertyName") in the set method for your property
    /// </remarks>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        // Make an event handler using (pre-existing?) PropertyChangedEventHandler delegate.
        // This defines the signature ("shape") of the functions that can be subscribed to this event.
        // Data-Bindings will "subscribe" to this event
        public event PropertyChangedEventHandler PropertyChanged;

        // A method that's inherited by any child of this class, which is fired (using get{} and set{}) whenever a property is changed in the child class
        // This "raises the event", or runs all the functions "subscribed", i.e. all the functions listed in the PropertyChanged event handler
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            // Grab the PropertyChanged handler we define previously
            // This is done because PropertyChanged can become null if a subscriber unsubscribes after the null check but before the invocation.
            // This way we still have a non-null copy, that won't cause problems, to work with.
            var handler = PropertyChanged;
            // Make sure the event is not empty
            // i.e. make sure the list of functions to run when this event fires is not empty (meaning no subscribers to the event)
            if (handler != null)
                // Fire the PropertyChanged event for this instance of ObservableObject (with the propertyName provided in the call to RaisePropertyChangedEvent)
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));

            // For the sake of clarity, I have added a simple Console log here so you can see when the event fires
            Debug.WriteLine("PropertyChange raised for the " + propertyName + " property of an instance of " + this);
        }
    }
}
