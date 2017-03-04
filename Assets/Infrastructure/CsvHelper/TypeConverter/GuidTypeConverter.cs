using System;

namespace CsvHelper
{
    class GuidTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Guid res;
            if (TryParse(text, out res))
            {
                return res;
            }
            else
            {
                return base.ConvertFromString(text);
            }
        }
        // Workaround for Guid.TryParse ( .Net4 )
        bool TryParse(string text, out Guid guid)
        {
            try
            {
                guid = new Guid(text);
                return true;
            }
            catch
            {
                guid = default(Guid);
                return false;
            }
        }

    }
}