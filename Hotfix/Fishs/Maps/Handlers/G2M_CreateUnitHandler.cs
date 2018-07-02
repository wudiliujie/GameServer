using ETHotfix.Fishs.Systems;
using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Maps.Handlers
{
    [MessageHandler(AppType.Map)]
    public class G2M_CreateUnitHandler : AMRpcHandler<G2M_CreateUnit, M2G_CreateUnit>
    {
        public static int count = 0;
        protected async override void Run(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
        {
            M2G_CreateUnit response = new M2G_CreateUnit();
            Log.Debug("请求");
            Model.Fishs.Entitys.Unit unit = ComponentFactory.Create<Model.Fishs.Entitys.Unit, UnitType>(UnitType.Hero);
            unit.AddComponent<UnitGateComponent, long>(message.GateSessionId);
            unit.AddComponent<PlayerDbComponent, int>(message.AccountId);
            var initRet = await unit.GetComponent<PlayerDbComponent>().InitDataSync();
            if (initRet)
            {
                Game.Scene.GetComponent<UnitManageComponent>().Add(unit);
                session.AddComponent<Model.Fishs.Components.SessionPlayerComponent>().Player = unit;
            }
            response.Tag = 0;
            reply(response);
            Log.Debug("数据库结束");
            count++;
        }
    }
}
