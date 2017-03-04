using System;
using System.Reflection;

namespace CsvHelper
{
    class AutoClassMap : IClassMap
    {
        PropertyInfo[] infos;

        public AutoClassMap(Type type)
        {
            infos = type.GetProperties();
        }

        public object Parse(CsvReader reader, object obj)
        {
            for (var i = 0; i < infos.Length; ++i)
            {
                infos[i].SetValue(obj, reader.GetField(infos[i].PropertyType, i), null);
            }
            return obj;
        }
    }
}