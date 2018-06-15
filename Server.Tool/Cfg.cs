using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Server.Tool
{
    public static class Cfg
    {
        static IConfigurationRoot Root;
        static Cfg()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Root = builder.Build();
        }
        public static string ProtocolName { get { return Root["ProtocolName"]; } }
        public static string ProtocolGameServer { get { return Root["ProtocolGameServer"]; } }
    }
}
