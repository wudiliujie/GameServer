using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Gates
{
    // 分发数值监听
    [Event(EventIdType.SessionDispose)]
    public class NetOuterSessionDisposeEvent : AEvent<Session>
    {
        public override void Run(Session a)
        {
            var componnet = a.GetComponent<SessionPlayerComponent>();
            if (componnet != null)
            {
                var playerManager = Game.Scene.GetComponent<PlayerManagerComponent>();
                var player = playerManager.Get(componnet.Player.Id);
                if (player != null)
                {
                    //

                }
            }
        }
    }
}
