using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Gates.Systems
{
    [ObjectSystem]
    public class SessionPlayerComponentDestroySystem : DestroySystem<SessionPlayerComponent>
    {
        public override async void Destroy(SessionPlayerComponent self)
        {
            // 发送断线消息
            //ActorMessageSender actorMessageSender = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(self.Player.UnitId);
            //actorMessageSender.Send(new G2M_SessionDisconnect());
            Log.Debug("断开连接:"+self.Player.Id);
            if (self.Player.UnitId != 0) //需要通知map清理掉unit
            {
                ActorMessageSender actorMessageSender = Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(self.Player.UnitId);
                var ret =await actorMessageSender.Call(new G2M_UnitDispose() { ActorId=self.Player.UnitId } );
                Log.Debug("G2M_UnitDispose:" + ret);
                Game.Scene.GetComponent<ActorMessageSenderComponent>().Remove(self.Player.UnitId);
            }
            Game.Scene.GetComponent<PlayerManagerComponent>()?.Remove(self.Player.Id);
        }
    }
}
