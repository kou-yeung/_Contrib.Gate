using System;
using Entity;

namespace Network
{
    interface ISession
    {
        void Poll();
        void Send(byte[] bytes);
        void Close();
        void SetCommandExec(Action<ISession, byte[]> commandExec);

        void Userdata(UserData userdata);
        UserData Userdata();
    }
}