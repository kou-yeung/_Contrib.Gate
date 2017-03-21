using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Network;
using Entity;

namespace ContribGate
{
    public class BallScene : MonoBehaviour
    {

        public Text text;
        // Use this for initialization
        void Start()
        {
            SocketService.Locator.AddReceivedEvent(Receive);
            // ミッションの状態を取得
            SocketService.Locator.Send(new Communication(Command.MissionState).GetBytes());
        }

        // Update is called once per frame
        void Update()
        {
        }

        void Receive(byte[] bytes)
        {
            var c = new Communication(bytes);
            switch (c.command)
            {
                case Command.MissionState:
                    var states = c.Unpack<MissionState[]>();
                    text.text = "";
                    foreach (var state in states)
                    {
                        foreach (var m in GameEnities.Instance.missions)
                        {
                            if (state.MissionId == m.ID)
                            {
                                string data = m.Title;
                                for (var i = 0; i < m.Conditions.Length; ++i)
                                {
                                    if (m.Conditions[i].ID != IdWithType.Empty)
                                    {
                                        data += string.Format("\n({0}/{1})", state.Conditions[i], m.Conditions[i].Value);
                                    }
                                }
                                text.text = data;
                            }
                        }
                    }
                    break;
            }
        }
    }
}