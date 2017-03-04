using System;

namespace CsvHelper
{
    class Int32TypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Int32 res;
            if (Int32.TryParse(text, out res))
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