using System;
using System.Collections.Generic;

namespace CsvHelper
{
    static class ClassMapFactory
    {
        static readonly Dictionary<Type, IClassMap> classMaps = new Dictionary<Type, IClassMap>();

        public static void AddClassMap(Type type, IClassMap classMap)
        {
            classMaps[type] = classMap;
        }
        public static void RemoveClassMap(Type type)
        {
            classMaps.Remove(type);
        }
        public static IClassMap GetClassMap(Type type)
        {
            IClassMap classMap;
            if (classMaps.TryGetValue(type, out classMap))
            {
                return classMap;
            }
            AddClassMap(type, new AutoClassMap(type));
            return GetClassMap(type);
        }
    }
}