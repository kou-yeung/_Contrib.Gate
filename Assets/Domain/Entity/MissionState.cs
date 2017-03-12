//===================
// ミッションステート
// 各ユーザのミッションクリア状態を管理するクラスです
using System.Collections.Generic;

namespace Entity
{
    public class MissionState
    {
        public enum State
        {
            Order,  // 受注中
            Clear,  // クリア
            Close,  // クローズ
        }
        public IdWithType MissionId { get; set; }
        public int[] Conditions { get; set; }   // 各条件の進捗数　: Mission.Conditions と同じ長さ
        public State CurrentState { get; set; }

        public MissionState()
        {
            Conditions = new int[3];
        }

        public bool IsClear()
        {
            return CurrentState == State.Clear;
        }
        public bool IsClose()
        {
            return CurrentState == State.Close;
        }
        public bool IsOrder()
        {
            return CurrentState == State.Order;
        }
    }
}
