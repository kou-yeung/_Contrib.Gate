using System;

namespace CsvHelper
{
    public class CsvConfiguration
    {
        char delimiter = ',';
        char quote = '"';
        string comment = "#";

        public char Delimiter
        {
            get { return delimiter; }
            set
            {
                // TODO : 使用できない文字列のエラーチェック
                delimiter = value;
            }
        }
        public char Quote
        {
            get { return quote; }
            set
            {
                // TODO : 使用できない文字列のエラーチェック
                quote = value;
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                // TODO : 使用できない文字列のエラーチェック
                comment = value;
            }
        }

        public bool HasHeaderRecord { get; set; }
        public Func<string[], bool> ShouldSkipRecord { get; set; }

        public CsvConfiguration()
        {
            HasHeaderRecord = true;
        }
    }
}