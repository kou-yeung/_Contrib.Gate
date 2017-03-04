using System;

namespace CsvHelper
{
    public class DefaultTypeConverter : ITypeConverter
    {
        public virtual object ConvertFromString(string text)
        {
            throw new Exception("The conversion cannot be performed.");
        }
    }
}