using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Fishs.Components
{
    public class ServerData
    {
        public int ServerId { get; set; }
        public AppType ServerType { get; set; }
        public string NetInnerIp { get; set; }
        public int NetInnerPort { get; set; }
        public long SessionId { get; set; }
    }
    //服务器组件
    public class ServerManagerComponent : Component
    {
        public Dictionary<int, ServerData> ServerPool { get; } = new Dictionary<int, ServerData>();

    }
}
