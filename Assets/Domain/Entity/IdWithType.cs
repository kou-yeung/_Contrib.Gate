//====================
// タイプ付きID
// 仕様 : 
// 上位 8 bit はタイプ : 0xFF000000
// 下位 24 bit は ID  : 0x00FFFFFF
using System;
using System.Text.RegularExpressions;
using CsvHelper.TypeConversion;

namespace Entity
{
    public enum IdType
    {
        Unknown,    // 不明
        Mission,    // ミッション
        Ball,       // ボール ※テスト用 : 実際のゲームには使用しない
    }

    public struct IdWithType
    {
        public uint Value { get; set; }

        public IdType IdType
        {
            get { return GetIdType(this); }
        }
        public uint Id
        {
            get { return GetId(this); }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(IdWithType a, IdWithType b)
        {
            return a.Value == b.Value;
        }
        public static bool operator !=(IdWithType a, IdWithType b)
        {
            return a.Value != b.Value;
        }

        static public readonly IdWithType Empty = Create(IdType.Unknown, 0);

        // IdWhitType -> IdType を取得
        static public IdType GetIdType(IdWithType id)
        {
            return (IdType)((id.Value >> 24) & 0xFF);
        }
        // IdWhitType -> Id を取得
        static public uint GetId(IdWithType id)
        {
            return (id.Value & 0x00FFFFFF);
        }
        // ヘルパーメソッド
        static public IdWithType Create(IdType type, uint id)
        {
            var res = new IdWithType();
            res.Value = 0;
            res.Value |= (((uint)type) << 24) & 0xFF000000;
            res.Value |= id & 0x00FFFFFF;
            return res;
        }

        static Regex regex = new Regex(@"ID_([a-zA-Z]*?)_(\d{3,3})_(\d{3,3})");
        static public bool TryParse(string str, out IdWithType res)
        {
            res = IdWithType.Empty;

            // フォーマット : ID_{{IdType}}_{{MAIN_ID}}_{SUB_ID}
            // 正規表現 : @"ID_([a-zA-Z]*?)_(\d{3,3})_(\d{3,3})"
            var match = regex.Match(str);
            if (match == Match.Empty) return false;

            var type = (IdType)Enum.Parse(typeof(IdType), match.Groups[1].Value, true);
            var main = UInt32.Parse(match.Groups[2].Value);
            var sub = UInt32.Parse(match.Groups[3].Value);
            res = IdWithType.Create(type, (main * 1000) + sub);
            return true;
        }
    }

    public class IdWithTypeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            IdWithType res;
            if (IdWithType.TryParse(text, out res)) return res;
            return IdWithType.Empty;
        }
    }
}
