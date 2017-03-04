using System;
using System.Net;

namespace CsvHelper
{
    class IPAddressTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text)
        {
            IPAddress res;
            if (IPAddress.TryParse(text, out res))
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