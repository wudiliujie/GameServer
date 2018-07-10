using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Fishs.Entitys
{
    public class Room : Entity
    {
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
