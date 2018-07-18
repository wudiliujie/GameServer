using ETModel;
using Model.Fishs.Components;
using Model.Fishs.Entitys;
using ETHotfix.Fishs.Maps.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix.Fishs.Maps.Handlers
{
    [ActorMessageHandler(AppType.Map)]
    public class G2M_UnitDisposeHandler : AMActorHandler<Unit, G2M_UnitDispose>
    {
        protected override async Task Run(Unit unit, G2M_UnitDispose message)
        {
            long unitId = unit.Id;
            // 删除unit,让其它进程发送过来的消息找不到actor，重发
            Game.EventSystem.Remove(unitId);
            Game.Scene.GetComponent<UnitManageComponent>().Remove(unitId);
            await Game.Scene.GetComponent<LocationProxyComponent>().Remove(unitId); //刪除远程
            var unitRoom = unit.GetComponent<UnitRoomComponent>();
            if (unitRoom != null)
            {
                Log.Debug("离开room");
                unitRoom.LeaveRoom();
            }


            //保存数据
            Log.Debug("删除数据:unit" + unitId);
            unit.Dispose();
            Log.Debug("釋放:unit" + unitId);
        }
    }
}
