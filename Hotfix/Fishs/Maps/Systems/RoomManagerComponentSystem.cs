using ETModel;
using Model.Fishs.Components;
using Model.Fishs.Entitys;
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
    public static class RoomManagerComponentSystemEx
    {
        public static Room GetBestRoom(this RoomManagerComponent self, RoomType roomType)
        {
            var rooms = self.GetAll();
            //
            foreach (var item in rooms)
            {
                if (item.RoomType == roomType)
                {
                    if (item.UnitCount < CFG.RoomMaxNum)
                    {
                        return item;
                    }
                }
            }
            //创建一个新的房间
            var room = ComponentFactory.Create<Room, RoomType>(roomType);
            self.Add(room);
            return room;
        }
    }
}
