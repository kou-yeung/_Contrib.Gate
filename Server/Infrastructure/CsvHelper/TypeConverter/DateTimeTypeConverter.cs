using System;

namespace CsvHelper
{
    class DateTimeTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            DateTime res;
            if (DateTime.TryParse(text, out res))
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