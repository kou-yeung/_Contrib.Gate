using System;

namespace CsvHelper
{
    class Int64TypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Int64 res;
            if (Int64.TryParse(text, out res))
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