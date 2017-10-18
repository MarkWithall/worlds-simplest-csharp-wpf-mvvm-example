using System;

namespace MinimalMVVM.Models
{
    public class TextConverter
    {
        private readonly Func<string, string> convertion;

        public TextConverter(Func<string, string> convertion)
        {
            this.convertion = convertion;
        }

        public string ConvertText(string inputText)
        {
            return this.convertion(inputText);
        }
    }
}
