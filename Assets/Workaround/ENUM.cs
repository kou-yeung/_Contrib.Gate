//===========================================
// enum のワークアラウンド
// MsgPack for Unity で enum が正しくシリアライズされないため、一時対応します。
// https://github.com/msgpack/msgpack-cli/blob/0.9.0-beta2/src/MsgPack/Serialization/EnumMessagePackSerializer%601.cs#L302

namespace Workaround
{
    public class ENUM
    {
        public int value { get; set; }
    }

}