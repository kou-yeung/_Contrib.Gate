using System;

namespace CsvHelper
{
    class UInt32TypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            UInt32 res;
            if (UInt32.TryParse(text, out res))
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