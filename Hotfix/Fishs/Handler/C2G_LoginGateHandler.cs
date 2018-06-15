using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Handler
{
    [MessageHandler(AppType.Gate)]
    public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
    {
        protected override void Run(Session session, C2G_LoginGate message, Action<G2C_LoginGate> reply)
        {
            G2C_LoginGate response = new G2C_LoginGate();
            try
            {
                string account = Game.Scene.GetComponent<GateSessionKeyComponent>().Get(message.Key);
                if (account == null)
                {
                    response.Error = ErrorCode.ERR_ConnectGateKeyError;
                    response.Message = "Gate key验证失败!";
                    reply(response);
                    return;
                }
                var player = ComponentFactory.Create<Model.Fishs.Entitys.Unit, long>(message.Key);
                Game.Scene.GetComponent<UnitManageComponent>().Add(player);
                session.AddComponent<Model.Fishs.Components.SessionPlayerComponent>().Player = player;
                session.AddComponent<MailBoxComponent, string>(ActorType.GateSession);
                //读取用户数据
                //Game.Scene.GetComponent<DBProxyComponent>().Save


                response.PlayerId = player.Id;
                reply(response);

                session.Send(new G2C_TestHotfixMessage() { Info = "recv hotfix message success" });
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
