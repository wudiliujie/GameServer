using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ETModel
{
    public class LocationConfig : AConfigComponent
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public override void EndInit()
        {
            base.EndInit();

            this.IPEndPoint = NetworkHelper.ToIPEndPoint(this.Host, this.Port);
        }
        public IPEndPoint IPEndPoint { get; private set; }
    }
}
