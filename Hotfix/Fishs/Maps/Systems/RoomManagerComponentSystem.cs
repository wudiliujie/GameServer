using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Maps.Systems
{
    [ObjectSystem]
    public class RoomManagerComponentSystem : AwakeSystem<RoomManagerComponent>
    {
        public override void Awake(RoomManagerComponent self)
        {
            self.Awake();
        }
    }
    /// <summary>
    /// 房间组件管理系统
    /// </summary>
    public class RoomManagerComponentSystemEx
    {

    }
}
