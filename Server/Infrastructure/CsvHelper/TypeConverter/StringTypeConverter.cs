using System;

namespace CsvHelper
{
    class StringTypeConverter : ITypeConverter
    {
        public object ConvertFromString(string text)
        {
            return (text != null) ? (text) : (String.Empty);
        }
    }
}