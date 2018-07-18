using ETModel;
using Model.Fishs.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Fishs.Components
{
    public class RoomManagerComponent : Component
    {
        private readonly Dictionary<long, Room> idRooms = new Dictionary<long, Room>();
        private int _roomIdSeq;

        public void Awake()
        {
        }
        /// <summary>
        /// 房间ID,索引
        /// </summary>
        public int RoomIdSeq { get => _roomIdSeq++; }

        public void Add(Room room)
        {
            this.idRooms.Add(room.Id, room);
        }

        public Room Get(long id)
        {
            this.idRooms.TryGetValue(id, out Room gamer);
            return gamer;
        }

        public void Remove(long id)
        {
            this.idRooms.Remove(id);
        }

        public int Count
        {
            get
            {
                return this.idRooms.Count;
            }
        }

        public Room[] GetAll()
        {
            return this.idRooms.Values.ToArray();
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            foreach (Room room in this.idRooms.Values)
            {
                room.Dispose();
            }
        }
    }
}
