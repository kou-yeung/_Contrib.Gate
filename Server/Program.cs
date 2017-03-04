using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Network;

namespace Server
{
    class Program
    {
        static void TaskReceive(TcpListener listener)
        {
            while (true)
            {
                // 接続要求あるかどうか確認
                if (listener.Pending())
                {
                    // 接続要求を処理する
                    var client = listener.AcceptTcpClient();

                    // 認証待ち一覧に追加
                    var session = new TcpClientSession(client);
                    GameState.Instance.AddSession(session);
                    Console.WriteLine("AcceptTcpClient : {0}", client.Client.RemoteEndPoint);
                }

                GameState.Instance.Update();

                // TODO : 接続切断したものがあれば削除
                //if (sessions.RemoveAll(s => s.Disconnected) > 0)
                //{
                //    // TODO : 以下コピペーなのでリファクタリング
                //    var info = new Server2Client.UserInfo();
                //    info.numUser = sessions.Count;
                //    info.Username = new string[sessions.Count];
                //    for (var i = 0; i < sessions.Count; ++i)
                //    {
                //        info.Username[i] = sessions[i].userInfo.Username;
                //    }
                //    var bytes = ProtoMaker.Pack(info);
                //    // 削除されたものがあるので情報を再送信
                //    foreach (var session in sessions)
                //    {
                //        session.Send(bytes);
                //    }
                //}
                Task.Delay(16).Wait();
            }
        }

        static void Main(string[] args)
        {

            // 初期化
            GameState.CreateInstance();
            
            // サービスロケータ設定

            // 接続開始
            var port = 2007; // Listenするポート番号
            TcpListener listener = new TcpListener(IPAddress.Any, port);

            listener.Start(100);

            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() => TaskReceive(listener)));
            //tasks.Add(Task.Run(() => TaskSend()));

            Task.WaitAll(tasks.ToArray());

        }
    }
}
