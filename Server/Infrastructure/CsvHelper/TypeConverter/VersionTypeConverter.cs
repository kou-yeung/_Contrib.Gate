using System;

namespace CsvHelper
{
    class VersionTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            Version res;
            if (TryParse(text, out res))
            {
                return res;
            }
            else
            {
                return base.ConvertFromString(text);
            }
        }
        // Workaround for Version.TryParse ( .Net4 )
        bool TryParse(string text, out Version version)
        {
            try
            {
                version = new Version(text);
                return true;
            }
            catch
            {
                version = default(Version);
                return false;
            }
        }
    }
}