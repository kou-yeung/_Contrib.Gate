using System;

namespace CsvHelper
{
    class UInt64TypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            UInt64 res;
            if (UInt64.TryParse(text, out res))
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