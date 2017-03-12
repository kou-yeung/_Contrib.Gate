using System;

namespace CsvHelper
{
    class BooleanTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Boolean res;
            if (Boolean.TryParse(text, out res))
            {
                return res;
            }
            SByte value;
            if (SByte.TryParse(text, out value))
            {
                if (value == 0) return false;
                if (value == 1) return true;
            }
            return base.ConvertFromString(text);
        }
    }
}