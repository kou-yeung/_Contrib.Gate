//======================
// ミッションデータ
// 現在は Mission.csv から読み込む
// TODO : 将来はデータコンバーターを介して MessagePack でシリアライズ (変換時のエラーチェック)
using System;
using CsvHelper;

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
    public class MissionMap : IClassMap
    {
        public object Parse(CsvReader reader, object obj)
        {
            var mission = obj as Mission;
            mission.ID = reader.GetField<IdWithType>("ID");
            mission.Accept = reader.GetField<IdWithType>("受注条件");
            mission.Title = reader.GetField<string>("タイトル");

            for (var i = 0; i < mission.Conditions.Length; ++i)
            {
                mission.Conditions[i] = new Condition();
                mission.Conditions[i].ID = reader.GetField<IdWithType>(string.Format("条件{0}", i + 1));
                mission.Conditions[i].Value = reader.GetField<int>(string.Format("条件数{0}", i + 1));
            }
            for (var i = 0; i < mission.Rewards.Length; ++i)
            {
                mission.Rewards[i] = new Reward();
                mission.Rewards[i].ID = reader.GetField<IdWithType>(string.Format("報酬{0}", i + 1));
            }
            return obj;
        }
    }
}
