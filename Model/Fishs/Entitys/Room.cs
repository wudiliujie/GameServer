using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Fishs.Entitys
{
    /// <summary>
    /// 房间类型：
    /// </summary>
    public enum RoomType
    {
        T10,
        T100,
        T1000,
        TBoss
    }
    public class Room : Entity
    {
        public RoomType RoomType { get; set; }
        public Dictionary<long, Unit> Units { get; set; } = new Dictionary<long, Unit>();
        public int UnitCount { get { return Units.Count; } }
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
