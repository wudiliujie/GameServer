using ETHotfix.Fishs.Systems;
using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.EventHandlers
{
    // 分发数值监听
    [Event(EventIdType.SessionDispose)]
    public class SessionDisposeEvent : AEvent<Session>
    {
        public override void Run(Session a)
        {
            var serverManager = Game.Scene.GetComponent<ServerManagerComponent>();
            if (serverManager != null)
            {
                serverManager.OnSessionDispose(a);
            }

        }
    }
}
