using ETModel;
using Model.Module.MySql;
using System;
using System.Collections.Generic;
using System.Text;
using ETHotfix.Module.Sql;
using Model.Fishs.Components;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2S_UserLoginHandler : AMRpcHandler<C2S_UserLogin, S2C_UserLogin>
    {
        public static int count = 0;
        protected async override void Run(Session session, C2S_UserLogin message, Action<S2C_UserLogin> reply)
        {
            S2C_UserLogin response = new S2C_UserLogin();
            Log.Debug("请求");
            var data = await Game.Scene.GetComponent<SqlComponent>().GetUserDbInfo(1);
            Model.Fishs.Entitys.Unit unit = ComponentFactory.Create<Model.Fishs.Entitys.Unit, UnitType>(UnitType.Hero);
            Game.Scene.GetComponent<UnitManageComponent>().Add(unit);
            session.AddComponent<Model.Fishs.Components.SessionPlayerComponent>().Player =unit;
            session.AddComponent<MailBoxComponent, string>(ActorType.GateSession);

            response.Tag = 0;
            reply(response);

        Log.Debug("数据库结束");
            count++;
        }
    }
}
