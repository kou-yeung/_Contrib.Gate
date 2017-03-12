using System;

namespace CsvHelper
{
    class CharTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Char res;
            if (Char.TryParse(text, out res))
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