
using System;
using System.Linq;
using System.IO;

namespace Network
{
    class StandaloneSocketService : ISocketService
    {
        StandaloneSession session;
        Action<byte[]> ReceviedEvent;

        public void Connect(string hostname, int port)
        {
            session = new StandaloneSession();
            GameState.Instance.AddSession(session);
        }

        public bool Connected()
        {
            return session != null;
        }

        public void Poll()
        {
            if (!Connected()) return;

            session.EachReceive(receive =>
            {
                using (var ms = new MemoryStream(receive))
                {
                    var packet = new PacketHelper(ms);
                    byte[] bytes;
                    if (packet.Receive(out bytes))
                    {
                        if (ReceviedEvent != null)
                        {
                            ReceviedEvent(bytes);
                        }
                    }
                }
            });
        }

        public void AddReceivedEvent(Action<byte[]> cb)
        {
            ReceviedEvent += cb;
        }
        public void RemoveReceivedEvent(Action<byte[]> cb)
        {
            ReceviedEvent -= cb;
        }

        public void Send(byte[] bytes)
        {
            session.C2S(bytes);
        }
    }
}