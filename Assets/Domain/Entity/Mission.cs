//======================
// ミッションデータ
// 現在は Mission.csv から読み込む
// TODO : 将来はデータコンバーターを介して MessagePack でシリアライズ (変換時のエラーチェック)
using System;
using CsvHelper.Configuration;

namespace Entity
{
    // 条件
    public class Condition
    {
        public IdWithType ID { get; set; }  // 条件の対象ID
        public int Value { get; set; }      // 条件の対象数
    }
    // 報酬
    public class Reward
    {
        public IdWithType ID { get; set; }  // 報酬ID
        public int Value { get; set; }      // 報酬数
    }
    // ミッション情報
    public class Mission
    {
        public IdWithType ID { get; set; }
        public IdWithType Accept { get; set; }  // 受注条件ミッションID : このIDによるクエスト解放
        public string Title { get; set; }
        public Condition[] Conditions { get; set; }
        public Reward[] Rewards { get; set; }

        public Mission()
        {
            Conditions = new Condition[3];
            Rewards = new Reward[1];
        }
    }
    // 将来は データコンバーターへ
    public sealed class MissionMap : CsvClassMap<Mission>
    {
        public MissionMap()
        {
            Map(m => m.ID).ConvertUsing(row =>
            {
                IdWithType id;
                if (row.TryGetField("ID", out id)) return id;
                return IdWithType.Empty;
            });
            Map(m => m.Accept).ConvertUsing(row =>
            {
                IdWithType id;
                if (row.TryGetField("受注条件", out id)) return id;
                return IdWithType.Empty;
            });
            Map(m => m.Title).Name("タイトル");

            Map(m => m.Conditions).ConvertUsing(row =>
            {
                var Conditions = new Condition[3];
                for (var i = 0; i < Conditions.Length; ++i)
                {
                    var index = i + 1;
                    Conditions[i] = new Condition() { ID = IdWithType.Empty, Value = 0 };
                    IdWithType id;
                    if (row.TryGetField(String.Format("条件{0}", index), out id))
                    {
                        Conditions[i].ID = id;
                    }
                    int value;
                    if (row.TryGetField(String.Format("条件数{0}", index), out value))
                    {
                        Conditions[i].Value = value;
                    }
                }
                return Conditions;
            });

            Map(m => m.Rewards).ConvertUsing(row =>
            {
                var Rewards = new Reward[1];
                for (var i = 0; i < Rewards.Length; ++i)
                {
                    Rewards[i] = new Reward() { ID = IdWithType.Empty, Value = 0 };
                    IdWithType id;
                    if (row.TryGetField(String.Format("報酬{0}", i + 1), out id))
                    {
                        Rewards[i].ID = id;
                    }
                }
                return Rewards;
            });
        }
    }
}
