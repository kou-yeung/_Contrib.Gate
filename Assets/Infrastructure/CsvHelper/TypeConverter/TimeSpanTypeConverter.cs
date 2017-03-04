using System;

namespace CsvHelper
{
    class TimeSpanTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            TimeSpan res;
            if (TimeSpan.TryParse(text, out res))
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