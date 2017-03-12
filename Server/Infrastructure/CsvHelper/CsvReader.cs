using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace CsvHelper
{
    public class CsvReader
    {
        private CsvParser parser;
        private CsvConfiguration configuration;
        private string[] currentRecord;
        private string[] headerRecord;

        public CsvReader(TextReader reader) : this(reader, new CsvConfiguration()) { }

        public CsvReader(TextReader reader, CsvConfiguration configuration)
        {
            this.parser = new CsvParser(reader, configuration);
            this.configuration = configuration;
        }
        /// <summary>
        /// 一行読み進む
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            ReadHeaderRecord();
            do
            {
                currentRecord = parser.Read();
            } while (ShouldSkipRecord());

            return currentRecord != null;
        }
        /// <summary>
        /// ヘッダーレコード読み込み
        /// </summary>
        void ReadHeaderRecord()
        {
            if (headerRecord == null && configuration.HasHeaderRecord)
            {
                headerRecord = parser.Read();
            }
        }
        /// <summary>
        /// 行のスキップ判定
        /// </summary>
        /// <returns></returns>
        bool ShouldSkipRecord()
        {
            if (currentRecord == null) return false;
            if (currentRecord[0].StartsWith(configuration.Comment)) return true;
            return (configuration.ShouldSkipRecord != null) && configuration.ShouldSkipRecord(currentRecord);
        }
        /// <summary>
        /// 指定ヘッダーのフィールドのデータを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="header"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public T GetField<T>(string header, T def = default(T))
        {
            return (T)GetField(typeof(T), header, def);
        }
        /// <summary>
        /// 指定ヘッダーのフィールドのデータを取得
        /// </summary>
        /// <param name="type"></param>
        /// <param name="header"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public object GetField(Type type, string header, object def = default(object))
        {
            if (headerRecord == null) return def;
            var index = Array.FindIndex(headerRecord, s => s == header);
            if (index < 0) return def;
            return GetField(type, index, def);
        }
        /// <summary>
        /// 指定Indexのフィールドのデータを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public T GetField<T>(int index, T def = default(T))
        {
            return (T)GetField(typeof(T), index, def);
        }

        /// <summary>
        /// 指定Indexのフィールドのデータを取得
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public object GetField(Type type, int index, object def = default(object))
        {
            var typeConverter = TypeConverterFactory.GetConverter(type);
            if (index < 0 || index >= currentRecord.Length)
            {
                throw new Exception(String.Format("Field at index '{0}' does not exist.", index));
            }

            if (String.IsNullOrEmpty(currentRecord[index])) return def;

            return typeConverter.ConvertFromString(currentRecord[index]);
        }

        /// <summary>
        /// 一行を解析して、指定されたクラスを生成する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetRecord<T>() where T : new()
        {
            if (Read())
            {
                return (T)ClassMapFactory.GetClassMap(typeof(T)).Parse(this, new T());
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// すべての行を解析して、指定されたクラス配列を生成する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] GetRecords<T>() where T : new()
        {
            List<T> records = new List<T>();

            var map = ClassMapFactory.GetClassMap(typeof(T));

            ReadHeaderRecord();

            while (Read())
            {
                records.Add((T)map.Parse(this, new T()));
            }
            return records.ToArray();
        }
    }
}