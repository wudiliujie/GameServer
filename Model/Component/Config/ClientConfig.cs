using ETModel;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ETModel
{
    public class ClientConfig : AConfigComponent
    {
        public string Host { get; set; }
        public int Port { get; set; }


        private IPEndPoint ipEndPoint;

        public override void EndInit()
        {
            base.EndInit();

            this.ipEndPoint = NetworkHelper.ToIPEndPoint(this.Host, this.Port);
        }

        public IPEndPoint IPEndPoint
        {
            get
            {
                return this.ipEndPoint;
            }
        }
    }
}
