using System;

namespace CsvHelper
{
    class Int16TypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Int16 res;
            if (Int16.TryParse(text, out res))
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