using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using Entity;

namespace ContribGate
{
    public class HitBall : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 検証目的です。動作は最適化していません(重いはず)
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.RaycastAll(ray);
                foreach (var hit in hits)
                {
                    var go = hit.collider.gameObject;
                    var ball = hit.collider.gameObject.GetComponent<Ball>();
                    if (ball != null)
                    {
                        var communication = new Communication(Command.GetBall);
                        communication.Pack(ball.idWithType);
                        SocketService.Locator.Send(communication.GetBytes());
                    }
                    GameObject.Destroy(go);
                }
            }
        }
    }
}