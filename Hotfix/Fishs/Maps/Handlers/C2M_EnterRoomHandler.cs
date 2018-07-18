using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ETHotfix.Fishs.Maps.Systems;
using Model.Fishs.Entitys;

namespace ETHotfix.Fishs.Maps.Handlers
{
    [ActorMessageHandler(AppType.Map)]
    public class C2M_EnterRoomHandler : AMActorRpcHandler<Model.Fishs.Entitys.Unit, C2M_EnterRoom, M2C_EnterRoom>
    {
        protected override async Task Run(Model.Fishs.Entitys.Unit unit, C2M_EnterRoom message, Action<M2C_EnterRoom> reply)
        {
            await Task.CompletedTask;
            Log.Debug("EnterRoom");
            RoomManagerComponent roomManager = Game.Scene.GetComponent<RoomManagerComponent>();
            Room room = roomManager.GetBestRoom((RoomType)message.RoomType);
            var response = new M2C_EnterRoom();
            if (room == null)
            {
                response.Tag = ErrorCode.ERR_RoomNOExist;
                reply(response);
            }
            response.Tag = room.EnterRoom(unit);
            reply(response);
            room.BroadcastNewUnit(unit);

        }
    }
}
