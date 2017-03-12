using System;
using System.Collections.Generic;
using System.Net;

namespace CsvHelper
{
    public static class TypeConverterFactory
    {
        static readonly Dictionary<Type, ITypeConverter> typeConverters = new Dictionary<Type, ITypeConverter>();

        static TypeConverterFactory()
        {
            CreateDefaultConverter();
        }

        public static void AddConverter(Type type, ITypeConverter typeConverter)
        {
            typeConverters[type] = typeConverter;
        }
        public static ITypeConverter GetConverter(Type type)
        {
            ITypeConverter res;
            if (typeConverters.TryGetValue(type, out res))
            {
                return res;
            }

            if (typeof(Enum).IsAssignableFrom(type))
            {
                AddConverter(type, new EnumTypeConverter(type));
                return GetConverter(type);
            }

            return new DefaultTypeConverter();
        }
        static void CreateDefaultConverter()
        {
            AddConverter(typeof(Byte), new Int64TypeConverter());
            AddConverter(typeof(SByte), new Int64TypeConverter());
            AddConverter(typeof(Char), new CharTypeConverter());
            AddConverter(typeof(Int16), new Int16TypeConverter());
            AddConverter(typeof(UInt16), new UInt16TypeConverter());
            AddConverter(typeof(Int32), new Int32TypeConverter());
            AddConverter(typeof(UInt32), new UInt32TypeConverter());
            AddConverter(typeof(Int64), new Int64TypeConverter());
            AddConverter(typeof(UInt64), new UInt64TypeConverter());
            AddConverter(typeof(Single), new SingleTypeConverter());
            AddConverter(typeof(Double), new DoubleTypeConverter());
            AddConverter(typeof(Decimal), new DecimalTypeConverter());
            AddConverter(typeof(Boolean), new BooleanTypeConverter());
            AddConverter(typeof(String), new StringTypeConverter());

            AddConverter(typeof(DateTime), new DateTimeTypeConverter());
            AddConverter(typeof(DateTimeOffset), new DateTimeOffsetTypeConverter());
            AddConverter(typeof(TimeSpan), new TimeSpanTypeConverter());
            AddConverter(typeof(Guid), new GuidTypeConverter());
            AddConverter(typeof(IPAddress), new IPAddressTypeConverter());
            AddConverter(typeof(Version), new VersionTypeConverter());
        }
    }
}
