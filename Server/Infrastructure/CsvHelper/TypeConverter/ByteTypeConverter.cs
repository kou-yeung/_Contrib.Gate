using System;

namespace CsvHelper
{
    class ByteTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Byte res;
            if (Byte.TryParse(text, out res))
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