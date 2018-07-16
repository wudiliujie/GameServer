using ETModel;
using Model.Fishs;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Systems
{
    [ObjectSystem]
    public class AttributeComponentSystem : AwakeSystem<AttributeComponent, RoleDbInfo>
    {
        public override void Awake(AttributeComponent self, RoleDbInfo a)
        {
            self.Awake(a);
        }
    }
    public static class AttributeComponentEx
    {
        public static void Awake(this AttributeComponent self, RoleDbInfo data)
        {
            if (data.AttrInts.Count == 0)
            {
                self.Name = "刘杰";
            }
            else
            {

                //初始化数据
                foreach (var item in data.AttrInts)
                {
                    self.InitAttrInt(item);
                }
                foreach (var item in data.AttrStrs)
                {
                    self.InitAttrStr(item);
                }
            }
            self.UpdateAttrAsync();


        }
        private static void InitAttrInt(this AttributeComponent self, AttrInt attr)
        {
            switch ((AttrType)attr.K)
            {
                case AttrType.Level:
                    self.Level = (int)attr.V;
                    break;
                case AttrType.Exp:
                    self.Exp = attr.V;
                    break;
                case AttrType.Gold:
                    self.Gold = attr.V;
                    break;
                case AttrType.Atk:
                    self.Atk = (int)attr.V;
                    break;
            }
        }
        private static void InitAttrStr(this AttributeComponent self, AttrStr attr)
        {
            switch ((AttrType)attr.K)
            {
                case AttrType.Name:
                    self.Name = attr.V;
                    break;
            }
        }

        public static void GetRoleDbInfo(this AttributeComponent self, RoleDbInfo data)
        {
            data.AddAttr(AttrType.Level, self.Level);
            data.AddAttr(AttrType.Name, self.Name);
            data.AddAttr(AttrType.Exp, self.Exp);
            data.AddAttr(AttrType.Gold, self.Gold);
            data.AddAttr(AttrType.Atk, self.Atk);
        }
        //更新属性
        public static async void UpdateAttrAsync(this AttributeComponent self)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();

            long instanceId = self.InstanceId;

            while (true)
            {
                await timerComponent.WaitAsync(500);
                if (self.InstanceId != instanceId)
                {
                    return;
                }
                if (self.TempAttrIntPool.Count > 0 || self.TempAttrStrPool.Count > 0)
                {
                    S2C_RoleInfo send = new S2C_RoleInfo();
                    foreach (var item in self.TempAttrIntPool)
                    {
                        send.AttrInts.Add(new AttrInt() { K = (int)item.Key, V = item.Value });
                    }
                    foreach (var item in self.TempAttrStrPool)
                    {
                        send.AttrStrs.Add(new AttrStr() { K = (int)item.Key, V = item.Value });
                    }
                    //self.GetParent<Unit>().GetComponent<Model.Fishs.Components.SessionPlayerComponent>().
                }
            }
        }
    }
}

