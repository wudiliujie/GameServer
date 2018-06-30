using ETModel;
using Model.Module.MySql;
using System;
using System.Collections.Generic;
using System.Text;
using ETHotfix.Module.Sql;
using Model.Fishs.Components;
using ETHotfix.Fishs.Systems;
using System.Net;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2S_UserLoginHandler : AMRpcHandler<C2S_UserLogin, S2C_UserLogin>
    {
        public static int count = 0;
        protected async override void Run(Session session, C2S_UserLogin message, Action<S2C_UserLogin> reply)
        {
            S2C_UserLogin response = new S2C_UserLogin();
            //从local获取一个map地址
            var ret = await Game.Scene.GetComponent<LocationProxyComponent>().GetMapAddress(1);
            if (ret.Tag != 0)
            {
                response.Tag = ret.Tag;
                reply(response);
            }
            Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(NetworkHelper.ToIPEndPoint(ret.Address));
            M2G_CreateUnit createUnit = (M2G_CreateUnit)await mapSession.Call(new G2M_CreateUnit() { AccountId = message.AccountId, GateSessionId = session.InstanceId });
            response.Tag = createUnit.UnitId;

            reply(response);

            Log.Debug("数据库结束");
            count++;
        }
    }
}
