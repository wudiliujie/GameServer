using ETModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix.Fishs.Maps.Handlers
{
    [ActorMessageHandler(AppType.Map)]
    public class C2M_EnterRoomHandler : AMActorRpcHandler<Unit, C2M_EnterRoom, M2C_EnterRoom>
    {
        protected override async Task Run(Unit unit, C2M_EnterRoom message, Action<M2C_EnterRoom> reply)
        {
            await Task.CompletedTask;
            Log.Debug("EnterRoom");
            reply(new M2C_EnterRoom() { Tag = 0 });
        }
    }
}
