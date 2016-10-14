using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinimalMVVM.Models
{
    public class TextConverter
    {
        private readonly Func<string, string> _convertion;

        public TextConverter(Func<string, string> convertion)
        {
            _convertion = convertion;
        }

        public string ConvertText(string inputText)
        {
            return _convertion(inputText);
        }
    }
}
