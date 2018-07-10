using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Gates.Systems
{
    [ObjectSystem]
    public class PlayerComponentSystem : AwakeSystem<PlayerManagerComponent>
    {
        public override void Awake(PlayerManagerComponent self)
        {
            self.Awake();
        }
    }
}
