// 通信用データを構築するクラス
using System;
using System.IO;
using System.Linq;
using MsgPack.Serialization;

namespace Network
{
    class Communication
    {
        MemoryStream stream;
        public Command command { get; private set; }

        public Communication(Command command)
        {
            this.command = command;
            stream = new MemoryStream();
        }
        public Communication(byte[] bytes)
        {
            this.command = (Command)BitConverter.ToInt32(bytes, 0);
            stream = new MemoryStream(bytes.Skip(4).ToArray());
        }

        MessagePackSerializer<T> GetSerializer<T>()
        {
            return SerializationContext.Default.GetSerializer<T>();
        }
        public Communication Pack<T>(T obj)
        {
            GetSerializer<T>().Pack(stream, obj);
            return this;
        }
        public T Unpack<T>()
        {
            return GetSerializer<T>().Unpack(stream);
        }
        public byte[] GetBytes()
        {
            var cmd = BitConverter.GetBytes((int)command);
            var data = stream.ToArray();
            var size = BitConverter.GetBytes(cmd.Length + data.Length);
            return size.Concat(cmd).Concat(data).ToArray();
        }

        static public Communication Create(Command command)
        {
            return new Communication(command);
        }
        static public Communication Create(byte[] bytes)
        {
            return new Communication(bytes);
        }
    }
}

