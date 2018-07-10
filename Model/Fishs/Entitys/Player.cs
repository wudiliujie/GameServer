using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Fishs.Entitys
{
    [ObjectSystem]
    public class PlayerSystem : AwakeSystem<Player, int>
    {
        public override void Awake(Player self, int a)
        {
            self.Awake(a);
        }
    }

    public sealed class Player : Entity
    {
        public int AccountId { get; private set; }

        public long UnitId { get; set; }

        public void Awake(int account)
        {
            this.AccountId = account;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();
        }
    }
}
