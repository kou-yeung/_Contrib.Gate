using System;

namespace Network
{
    interface ISession
    {
        void Poll();
        void Send(byte[] bytes);
        void Close();
        void SetCommandExec(Action<ISession, byte[]> commandExec);
    }
}