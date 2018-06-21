using ETHotfix.Module.Sql;
using ETModel;
using Model.Fishs.Components;
using Model.Module.MySql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
namespace ETHotfix.Fishs.Systems
{
    [ObjectSystem]
    public class PlayerDbComponentSystem : AwakeSystem<PlayerDbComponent, int>
    {
        public override void Awake(PlayerDbComponent self, int a)
        {
            self.Awake(a);
        }
    }
    public static class PlayerDbComponentEx
    {
        public static void Awake(this PlayerDbComponent self, int a)
        {
            //初始化数据
            self.AccountId = a;

        }
        public static async Task<bool> InitDataSync(this PlayerDbComponent self)
        {
            RoleDbInfo dbInfo = await Game.Scene.GetComponent<SqlComponent>().GetUserDbInfo(self.AccountId);

            var unit = self.GetParent<Model.Fishs.Entitys.Unit>();
            if (unit != null)
            {
                unit.AddComponent<AttributeComponent, RoleDbInfo>(dbInfo);
            }
            else
            {
                Log.Error("InitDataAsync Parent 不存在");
                return false;

            }
            self.UpdateFrameAsync();
            return true;
        }
        public static async void UpdateFrameAsync(this PlayerDbComponent self)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();

            long instanceId = self.InstanceId;

            while (true)
            {
                if (self.InstanceId != instanceId)
                {
                    return;
                }

                await timerComponent.WaitAsync(10000);
                Log.Debug("保存");
                RoleDbInfo roledb = new RoleDbInfo();
                self.GetParent<Model.Fishs.Entitys.Unit>().GetComponent<AttributeComponent>().GetRoleDbInfo(roledb);
                //

                await Game.Scene.GetComponent<SqlComponent>().SaveUserDbInfo(self.AccountId, roledb);
            }
        }
    }
}
