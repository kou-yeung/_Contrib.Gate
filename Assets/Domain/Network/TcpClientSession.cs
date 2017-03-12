using System;
using System.Net.Sockets;
using Entity;

namespace Network
{
    class TcpClientSession : ISession
    {
        TcpClient client;
        NetworkStream stream;
        PacketHelper packet;
        Action<ISession, byte[]> commandExec;

        UserData userdata;

        public TcpClientSession(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
            this.packet = new PacketHelper(stream);
        }

        public void Close()
        {
            client.Close();
        }

        public void Poll()
        {
            while (client.Available > 0)
            {
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

        public void Send(byte[] bytes)
        {
            try
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            catch
            {
                Close();
            }
        }

        public void SetCommandExec(Action<ISession, byte[]> commandExec)
        {
            this.commandExec = commandExec;
        }

        public void Userdata(UserData userdata)
        {
            this.userdata = userdata;
        }

        public UserData Userdata()
        {
            return this.userdata;
        }
    }
}