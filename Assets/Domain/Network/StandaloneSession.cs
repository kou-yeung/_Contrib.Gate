using System;
using System.IO;
using System.Collections.Generic;

namespace Network
{
    class StandaloneSession : ISession
    {
        Action<ISession, byte[]> commandExec;

        List<byte[]> c2s = new List<byte[]>();
        List<byte[]> s2c = new List<byte[]>();

        public void EachReceive(Action<byte[]> cb)
        {
            if (s2c.Count <= 0) return;
            if (cb == null) return;

            var list = s2c.ToArray();
            s2c.Clear();
            foreach (var data in list)
            {
                cb(data);
            }
        }
        public void Close()
        {
        }

        public void Poll()
        {
            if (c2s.Count <= 0) return;

            // コマンド処理時、リストに新たなデータを追加される可能性があるため
            // 配列に変換して処理します
            var list = c2s.ToArray();
            c2s.Clear();

            foreach (var data in list)
            {
                using (var ms = new MemoryStream(data))
                {
                    var packet = new PacketHelper(ms);
                    byte[] bytes;
                    if (packet.Receive(out bytes))
                    {
                        if (commandExec != null)
                        {
                            commandExec(this, bytes);
                        }
                    }
                }
            }
        }

        public void Send(byte[] bytes)
        {
            s2c.Add(bytes);
        }

        public void SetCommandExec(Action<ISession, byte[]> commandExec)
        {
            this.commandExec = commandExec;
        }

        // Client => Server
        public void C2S(byte[] bytes)
        {
            c2s.Add(bytes);
        }
    }
}