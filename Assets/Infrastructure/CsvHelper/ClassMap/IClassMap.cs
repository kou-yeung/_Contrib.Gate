using System;

namespace CsvHelper
{
    interface IClassMap
    {
        object Parse(CsvReader reader, object obj);
    }
}