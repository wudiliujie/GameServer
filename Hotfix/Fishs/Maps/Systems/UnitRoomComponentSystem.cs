using ETModel;
using Model.Fishs.Components;
using Model.Fishs.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Maps.Systems
{
    [ObjectSystem]
    public class UnitRoomComponentSystem : AwakeSystem<UnitRoomComponent, long>
    {
        public override void Awake(UnitRoomComponent self, long a)
        {
            self.Awake(a);
        }
    }
    /// <summary>
    /// 房间组件管理系统
    /// </summary>
    public static class UnitRoomComponentSystemEx
    {
        public static void Awake(this UnitRoomComponent self, long tableId)
        {
            Log.Debug("RoomId:" + tableId);
            self.RoomId = tableId;
        }
        /// <summary>
        /// 离开房间
        /// </summary>
        /// <param name="self"></param>
        public static int LeaveRoom(this UnitRoomComponent self)
        {

            var room = Game.Scene.GetComponent<RoomManagerComponent>().Get(self.RoomId);
            if (room == null)
            {
                return 0;
            }
            room.LeaveRoom(self.GetParent<Unit>());
            return 0;
        }
    }
}
