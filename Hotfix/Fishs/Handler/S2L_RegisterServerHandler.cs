using ETModel;
using Model.Module.MySql;
using System;
using System.Collections.Generic;
using System.Text;
using ETHotfix.Module.Sql;
using Model.Fishs.Components;
using ETHotfix.Fishs.Systems;

namespace ETHotfix
{
    [MessageHandler(AppType.Location)]
    public class S2L_RegisterServerHandler : AMRpcHandler<S2L_RegisterServer, ResponseMessage>
    {
        protected override void Run(Session session, S2L_RegisterServer message, Action<ResponseMessage> reply)
        {
            ResponseMessage response = new ResponseMessage();
            Log.Debug("收到注册消息:"+message);
            var component = Game.Scene.GetComponent<ServerManagerComponent>();
            response.Tag= component.Add(message.Info,session.InstanceId);
            reply(response);
        }
    }
}
