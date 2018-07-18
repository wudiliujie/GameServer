using ETModel;
using Google.Protobuf;
using Model.Fishs.Components;
using Model.Fishs.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Maps.Systems
{
    public static class RoomSystem
    {
        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static int EnterRoom(this Room self, Unit unit)
        {
            if (self.UnitCount >= CFG.RoomMaxNum)
            {
                return ErrorCode.ERR_Room_Full;
            }
            self.Units.Add(unit.Id, unit);
            unit.AddComponent<UnitRoomComponent, long>(self.Id);
            Log.Debug("进入房间" + self.Id);
            return 0;
        }
        public static int LeaveRoom(this Room self, Unit unit)
        {
            self.Units.Remove(unit.Id);
            unit.RemoveComponent<UnitRoomComponent>();
            Log.Debug("离开房间" + self.Id);
            return 0;
        }

        /// <summary>
        /// 广播新的unit进入房间
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unit"></param>
        public static void BroadcastNewUnit(this Room self, Unit unit)
        {

        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="self"></param>
        /// <param name="message"></param>
        private static void Broadcast(this Room self, IActorMessage message)
        {
            ActorMessageSenderComponent actorMessageSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
            foreach (Unit unit in self.Units.Values)
            {
                UnitGateComponent unitGateComponent = unit.GetComponent<UnitGateComponent>();
                if (unitGateComponent.IsDisconnect)
                {
                    continue;
                }

                actorMessageSenderComponent.GetWithActorId(unitGateComponent.GateSessionActorId).Send(message);
            }
        }

    }
}
