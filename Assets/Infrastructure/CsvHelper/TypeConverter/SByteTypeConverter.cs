using System;

namespace CsvHelper
{
    class SByteTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            SByte res;
            if (SByte.TryParse(text, out res))
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