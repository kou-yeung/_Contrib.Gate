//======================
// ユーザデータ
using System.Collections.Generic;
using Entity;
using Logic;

namespace Entity
{
    class UserData
    {
        public int Id { get; set; }

        // ミッションのクリア状況＆進捗管理
        // MEMO : 現在は起動時リセットしますが、将来はDBから復元する
        List<MissionState> missionStates = new List<MissionState>();
        public List<MissionState> MissionStates
        {
            get { return missionStates; }
        }

        public UserData()
        {
            MissionLogic.CheckOrder(GameEnities.Instance.missions, missionStates);
        }
    }
}