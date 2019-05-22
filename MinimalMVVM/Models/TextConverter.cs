using System;

namespace MinimalMVVM.Models
{
    /// <summary>
    /// This is the entirety of the "business logic" for this program.
    /// 
    /// It's a simple class which you pass a conversion function on instantiation, and then use with TextConverterInstance.ConvertText("some text")
    /// The instance we use is stored in Presenter
    /// </summary>
    public class TextConverter
    {
        // The backend delegate variable which holds the function that does the actual text conversion. Set this on instantiation
        private readonly Func<string, string> _conversionMethod;

        /// <summary>
        /// Create an instance of TextConverter, allowing the creator to pass in whatever actual conversion method they want
        /// </summary>
        public TextConverter(Func<string, string> conversionMethod)
        {
            _conversionMethod = conversionMethod;
        }

        /// <summary>
        /// Run the conversion function put into the private delegate above during instantiation, and return its output (the converted text)
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public string ConvertText(string inputText)
        {
            return _conversionMethod(inputText);
        }
    }
}
