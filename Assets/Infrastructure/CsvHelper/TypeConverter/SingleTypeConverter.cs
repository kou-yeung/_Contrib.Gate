using System;

namespace CsvHelper
{
    class SingleTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Single res;
            if (Single.TryParse(text, out res))
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