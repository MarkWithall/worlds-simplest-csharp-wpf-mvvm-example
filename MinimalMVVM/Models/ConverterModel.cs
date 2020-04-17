using System;
using System.Collections.Generic;

namespace MinimalMVVM.Models
{
    internal sealed class ConverterModel
    {
        private readonly Func<string, string> _conversion;
        private readonly ICollection<string> _history;

        public ConverterModel(Func<string, string> conversion, ICollection<string> history)
        {
            _conversion = conversion;
            _history = history;
        }

        public void ConvertText(string text, Action onUpdate)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            var converted = _conversion(text);
            if (!_history.Contains(converted)) _history.Add(converted);

            onUpdate();
        }
    }
}