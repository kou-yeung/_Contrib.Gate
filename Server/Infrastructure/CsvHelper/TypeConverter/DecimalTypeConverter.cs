using System;

namespace CsvHelper
{
    class DecimalTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Decimal res;
            if (Decimal.TryParse(text, out res))
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