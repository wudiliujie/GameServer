using ETHotfix;
using ETModel;
using Model.Fishs.Components;
using NLog;
using System;
using System.Net;
using System.Threading;

namespace Server.Location
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Log.Fatal("请输入服务器编号");
                return;
            }
            // 异步方法全部会回掉到主线程
            SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);
            try
            {
                int AppId = Convert.ToInt32(args[0]);
                string configFile = "../../Config/StartConfig/LocalAllServer.json";
                if (args.Length >= 2)
                {
                    configFile = args[1];
                }
                Game.EventSystem.Add(DLLType.Model, typeof(Game).Assembly);
                //Game.EventSystem.Add(DLLType.Hotfix, DllHelper.GetHotfixAssembly());
                Game.EventSystem.Add(DLLType.Hotfix, typeof(Hotfix).Assembly);
                StartConfig startConfig = Game.Scene.AddComponent<StartConfigComponent, string, int>(configFile, AppId).StartConfig;
                IdGenerater.AppId = AppId;

                if (startConfig.AppType != AppType.Location)
                {
                    Log.Error("命令行参数apptype与配置不一致");
                    return;
                }



                LogManager.Configuration.Variables["appType"] = startConfig.AppType.ToString();
                LogManager.Configuration.Variables["appId"] = startConfig.AppId.ToString();
                LogManager.Configuration.Variables["appTypeFormat"] = $"{startConfig.AppType,-8}";
                LogManager.Configuration.Variables["appIdFormat"] = $"{startConfig.AppId:D3}";

                Log.Info($"server start........................ {startConfig.AppId} {startConfig.AppType}");

                Game.Scene.AddComponent<OpcodeTypeComponent>();
                Game.Scene.AddComponent<MessageDispatherComponent>();

                InnerConfig innerConfig = startConfig.GetComponent<InnerConfig>();

                Game.Scene.AddComponent<NetInnerComponent, IPEndPoint>(innerConfig.IPEndPoint);
                Game.Scene.AddComponent<LocationComponent>();
                Game.Scene.AddComponent<ServerManagerComponent>();


                while (true)
                {
                    try
                    {
                        Thread.Sleep(1);
                        OneThreadSynchronizationContext.Instance.Update();
                        Game.EventSystem.Update();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Error(e);
            }
        }
    }
}
