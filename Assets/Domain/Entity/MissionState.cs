//===================
// ミッションステート
// 各ユーザのミッションクリア状態を管理するクラスです
using System.Collections.Generic;
using System.Linq;

namespace Entity
{
    public class MissionState
    {
        public class State : Workaround.ENUM
        {
            static public readonly State Order = new State { value = 0 };  // 受注中
            static public readonly State Clear = new State { value = 1 };  // クリア
            static public readonly State Close = new State { value = 2 };  // クローズ
        }

        public IdWithType MissionId { get; set; }
        public List<int> Conditions { get; set; }   // 各条件の進捗数　: Mission.Conditions と同じ長さ
        public State CurrentState { get; set; }

        public MissionState()
        {
            Conditions = new int[3].ToList();
            CurrentState = State.Order;
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
