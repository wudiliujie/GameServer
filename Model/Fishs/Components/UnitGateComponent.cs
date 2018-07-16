using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Fishs.Components
{
    [ObjectSystem]
    public class UnitGateComponentAwakeSystem : AwakeSystem<UnitGateComponent, long>
    {
        public override void Awake(UnitGateComponent self, long a)
        {
            self.Awake(a);
        }
    }

    public class UnitGateComponent : Component, ISerializeToEntity
    {
        public long GateSessionActorId;

        public bool IsDisconnect;

        public void Awake(long gateSessionId)
        {
            this.GateSessionActorId = gateSessionId;
        }

        public ActorMessageSender GetActorMessageSender()
        {
            return Game.Scene.GetComponent<ActorMessageSenderComponent>().Get(this.GateSessionActorId);
        }
    }
}
