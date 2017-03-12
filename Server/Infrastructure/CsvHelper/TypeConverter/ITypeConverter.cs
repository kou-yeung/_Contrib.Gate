using System;

namespace CsvHelper
{
    public interface ITypeConverter
    {
        object ConvertFromString(string text);
    }
}
