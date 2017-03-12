//=================
// ミッションを処理するためのロジック
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class MissionLogic
    {
        static void EachOrderMission(Mission[] missions, List<MissionState> states, Action<Mission, MissionState> cb)
        {
            foreach (var state in states)
            {
                var mission = Array.Find(missions, (m) => m.ID == state.MissionId);
                if (mission != null)
                {
                    cb(mission, state);
                }
            }
        }
        static void EachCondition(Mission mission, IdType type, Action<Condition, int> cb)
        {
            for (var i = 0; i < mission.Conditions.Length; ++i)
            {
                var condition = mission.Conditions[i];
                if (condition.ID.IdType == type)
                {
                    cb(condition, i);
                }
            }
        }

        // ボール取得時の条件確認
        static public void CheckBallGet(Mission[] missions, List<MissionState> states, IdWithType ball)
        {
            EachOrderMission(missions, states, (mission, state) =>
            {
                if (!state.IsOrder()) return; // 受注中以外なら弾く

                EachCondition(mission, IdType.Ball, (condition, index) =>
                {
                    if (condition.ID == ball)
                    {
                        state.Conditions[index] = Math.Min(condition.Value, state.Conditions[index] + 1);
                    }
                });
            });

            CheckClear(missions, states);
            CheckOrder(missions, states);
        }

        // 受注中のミッションがクリアしたかどうか確認
        static public void CheckClear(Mission[] missions, List<MissionState> states)
        {
            EachOrderMission(missions, states, (mission, state) =>
            {
                var a = (from c in mission.Conditions select c.Value).ToArray();
                var b = (from s in state.Conditions select s).ToArray();
                if (a.Length != b.Length) return;
                for (var i = 0; i < a.Length; ++i)
                {
                    if (a[i] != b[i]) return;
                }
                state.CurrentState = MissionState.State.Clear;
            });
        }
        // 受注確認 : 未開放ミッションを追加する
        static public void CheckOrder(Mission[] missions, List<MissionState> states)
        {
            foreach (var mission in missions)
            {
                // すでに登録済なら弾く
                if (states.Exists((s) => s.MissionId == mission.ID)) continue;

                if (mission.Accept == IdWithType.Empty)
                {
                    // 対象が必要としません
                    states.Add(new MissionState { MissionId = mission.ID });
                }
                else
                {
                    // 対象を検索
                    var state = states.Find((s) => s.MissionId == mission.Accept);
                    // 状態をチェック
                    if (state != null && (state.IsClear() || state.IsClose()))
                    {
                        states.Add(new MissionState { MissionId = mission.ID });
                    }
                }
            }
        }
    }
}