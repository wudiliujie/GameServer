using System;

namespace Server.Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Protocol protocol = new Protocol();
            protocol.Read(Cfg.ProtocolName);
            protocol.AllotId();
            protocol.GenerateProto(Cfg.ProtocolGameServer, ServerType.GameServer);
        }
    }
}
