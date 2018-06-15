using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public class HotfixProtbufPacker : IMessagePacker
    {
        public object DeserializeFrom(Type type, byte[] bytes)
        {
            return ML.C2S_UserLogin.Parser.ParseFrom(bytes);
        }

        public object DeserializeFrom(Type type, byte[] bytes, int index, int count)
        {
            throw new NotImplementedException();
        }

        public T DeserializeFrom<T>(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public T DeserializeFrom<T>(byte[] bytes, int index, int count)
        {
            throw new NotImplementedException();
        }

        public T DeserializeFrom<T>(string str)
        {
            throw new NotImplementedException();
        }

        public object DeserializeFrom(Type type, string str)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializeToByteArray(object obj)
        {
            throw new NotImplementedException();
        }

        public string SerializeToText(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
