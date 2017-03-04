using System;

namespace CsvHelper
{
    class UInt16TypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            UInt16 res;
            if (UInt16.TryParse(text, out res))
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