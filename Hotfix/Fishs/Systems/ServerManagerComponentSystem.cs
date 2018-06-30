using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Systems
{
    public class ServerManagerComponentSystem
    {

    }
    public static class ServerManagerComponentEx
    {
        public static int Add(this ServerManagerComponent self, ServerInfo info, long SessionId)
        {
            if (self.ServerPool.ContainsKey(info.ServerId))
            {
                return ErrorCode.ERR_ServerIdExt;
            }
            ServerData data = new ServerData();
            data.ServerId = info.ServerId;
            data.ServerType = (AppType)info.ServerType;
            data.NetInnerIp = info.NetInnerIp;
            data.NetInnerPort = info.NetInnerPort;
            data.SessionId = SessionId;
            self.ServerPool.Add(info.ServerId, data);
            return ErrorCode.ERR_Success;
        }
        public static void OnSessionDispose(this ServerManagerComponent self, Session session)
        {
            Log.Debug("session 断开连接");
            foreach (var item in self.ServerPool)
            {
                if (item.Value.SessionId == session.InstanceId)
                {
                    self.ServerPool.Remove(item.Key);
                    break;
                }
            }
        }
        public static string GetAddressByType(this ServerManagerComponent self, int mapType)
        {
            foreach (var item in self.ServerPool)
            {
                if (item.Value.ServerType == AppType.Map)
                {
                    return item.Value.NetInnerIp + ":" + item.Value.NetInnerPort;
                }
            }
            return "";
        }

    }
}
