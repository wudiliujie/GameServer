using System;
using System.Net;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class BenchmarkComponentSystem : AwakeSystem<BenchmarkComponent, IPEndPoint>
    {
        public override void Awake(BenchmarkComponent self, IPEndPoint a)
        {
            self.Awake(a);
        }
    }

    public static class BenchmarkComponentEx
    {
        public static void Awake(this BenchmarkComponent self, IPEndPoint ipEndPoint)
        {
            try
            {
                NetOuterComponent networkComponent = Game.Scene.GetComponent<NetOuterComponent>();
                self.Session = networkComponent.Create(ipEndPoint);

                self.TestLoginAsync();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async void TestAsync(this BenchmarkComponent self, NetOuterComponent networkComponent, IPEndPoint ipEndPoint, int j)
        {
            try
            {
                using (Session session = networkComponent.Create(ipEndPoint))
                {
                    int i = 0;
                    while (i < 100000000)
                    {
                        ++i;
                        await self.Send(session, j);
                    }
                }
            }
            catch (RpcException e)
            {
                Log.Error(e);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        public static async void TestLoginAsync(this BenchmarkComponent self)
        {
            try
            {
                await self.Login(self.Session);
            }
            catch (RpcException e)
            {
                Log.Error(e);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        public static async Task Login(this BenchmarkComponent self, Session session)
        {
            var send = new C2S_UserLogin();
            send.AccountId = 1;
            Log.Debug("发送");
            var ret = await session.Call<S2C_UserLogin>(send);
            Log.Debug(ret.ToString());
            if (ret.Tag == 0)
            {
                //var  send1 = new C2S_UserLoginHandler
            }
            Log.Debug("发送:"+ ret);
            //发送进入房间
            var ret1 = await session.Call<M2C_EnterRoom>(new C2M_EnterRoom() { RoomType = 0, ActorId = ret.UnitId });
            Log.Debug(ret1.ToString());
        }

        public static async Task Send(this BenchmarkComponent self, Session session, int j)
        {
            try
            {
                var send = new C2S_UserLogin();
                send.AccountId = 1;
                Log.Debug("发送");
                await session.Call<S2C_UserLogin>(send);
                Log.Debug("发送完成");
                //++self.k;

                //if (self.k % 100000 != 0)
                //{
                //	return;
                //}

                //long time2 = TimeHelper.ClientNow();
                //long time = time2 - self.time1;
                //self.time1 = time2;
                //Log.Info($"Benchmark k: {self.k} 每10W次耗时: {time} ms");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}