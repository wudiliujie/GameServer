using System;
using System.Threading;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class LocationProxyComponentSystem : AwakeSystem<LocationProxyComponent>
    {
        public override void Awake(LocationProxyComponent self)
        {
            self.Awake();
        }
    }
    [ObjectSystem]
    public class LocationProxyComponentSystemStart : StartSystem<LocationProxyComponent>
    {
        public override async void Start(LocationProxyComponent self)
        {
            Log.Debug("b");

            var t = await self.RegisterServer();

            Log.Debug("a");
            Log.Debug("a" + t);
        }
    }

    public static class LocationProxyComponentEx
    {
        public static void Awake(this LocationProxyComponent self)
        {
            StartConfigComponent startConfigComponent = Game.Scene.GetComponent<StartConfigComponent>();

            StartConfig startConfig = startConfigComponent.StartConfig;
            self.LocationAddress = startConfig.GetComponent<LocationConfig>().IPEndPoint;
        }

        public static async Task<bool> RegisterServer(this LocationProxyComponent self)
        {
            var success = false;
            for (int i = 0; i < 10; i++)
            {
                Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.LocationAddress);
                StartConfig startConfig = Game.Scene.GetComponent<StartConfigComponent>().StartConfig;
                ServerInfo info = new ServerInfo();
                var innerConfig = startConfig.GetComponent<InnerConfig>();
                info.NetInnerIp = innerConfig.Host;
                info.NetInnerPort = innerConfig.Port;
                info.ServerId = startConfig.AppId;
                info.ServerType = (int)startConfig.AppType;
                Log.Debug("开始发送:" + info);
                var a = await session.Call<ResponseMessage>(new S2L_RegisterServer() { Info = info });
                if (a.Tag != 0)
                {
                    Log.Fatal("连接本地服务器失败" + a);
                }
                else
                {
                    success = true;
                    break;
                }
                Thread.Sleep(1000);
            }
            if (success)
            {
                return true;
            }
            else
            {
                Environment.Exit(0); //0代表正常退出，非0代表某种错误的退出
            }
            return true;
        }

        /// <summary>
        /// 获取一个指定map类型的地址
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mapType"></param>
        /// <returns></returns>
        public static async Task<L2G_GetMapAddress> GetMapAddress(this LocationProxyComponent self, int mapType)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.LocationAddress);
            L2G_GetMapAddress result = await session.Call<L2G_GetMapAddress>(new G2L_GetMapAddress() { MapType = mapType });
            return result;
        }

        public static async Task Add(this LocationProxyComponent self, long key, long instanceId)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.LocationAddress);
            await session.Call<ObjectAddResponse>(new ObjectAddRequest() { Key = key, InstanceId = instanceId });
        }

        public static async Task Lock(this LocationProxyComponent self, long key, long instanceId, int time = 1000)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.LocationAddress);
            await session.Call<ObjectLockResponse>(new ObjectLockRequest() { Key = key, InstanceId = instanceId, Time = time });
        }

        public static async Task UnLock(this LocationProxyComponent self, long key, long oldInstanceId, long instanceId)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.LocationAddress);
            await session.Call<ObjectUnLockResponse>(new ObjectUnLockRequest() { Key = key, OldInstanceId = oldInstanceId, InstanceId = instanceId });
        }

        public static async Task Remove(this LocationProxyComponent self, long key)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.LocationAddress);
            await session.Call<ObjectRemoveResponse>(new ObjectRemoveRequest() { Key = key });
        }

        public static async Task<long> Get(this LocationProxyComponent self, long key)
        {
            Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.LocationAddress);
            ObjectGetResponse response =await session.Call<ObjectGetResponse>(new ObjectGetRequest() { Key = key });
            return response.InstanceId;
        }

    }
}