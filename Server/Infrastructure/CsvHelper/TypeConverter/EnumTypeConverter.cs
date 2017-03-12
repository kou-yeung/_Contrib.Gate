using System;

namespace CsvHelper
{
    class EnumTypeConverter : DefaultTypeConverter
    {
        Type type;

        public EnumTypeConverter(Type type)
        {
            this.type = type;
        }

        public override object ConvertFromString(string text)
        {
            try
            {
                return Enum.Parse(type, text, true);
            }
            catch
            {
                return base.ConvertFromString(text);
            }
        }
    }
}