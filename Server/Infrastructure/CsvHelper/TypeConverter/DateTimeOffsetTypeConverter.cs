using System;

namespace CsvHelper
{
    class DateTimeOffsetTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            DateTimeOffset res;
            if (DateTimeOffset.TryParse(text, out res))
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