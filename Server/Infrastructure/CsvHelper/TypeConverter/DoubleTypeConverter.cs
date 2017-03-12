using System;

namespace CsvHelper
{
    class DoubleTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Double res;
            if (Double.TryParse(text, out res))
            {
                return res;
            }
            else
            {
                return base.ConvertFromString(text);
            }
        }
    }
}