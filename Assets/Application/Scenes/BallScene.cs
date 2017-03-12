using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Network;
using Entity;

public class BallScene : MonoBehaviour {

    public Text text;
	// Use this for initialization
	void Start ()
    {
        SocketService.Locator.AddReceivedEvent(Receive);
        // ミッションの状態を取得
        SocketService.Locator.Send(new Communication(Command.MissionState).GetBytes());
    }
	
	// Update is called once per frame
	void Update ()
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
                            text.text = string.Format("{0} : ({1}/{2})", m.Title, state.Conditions[0], m.Conditions[0].Value);
                        }
                    }
                }
                break;
        }
    }
}
