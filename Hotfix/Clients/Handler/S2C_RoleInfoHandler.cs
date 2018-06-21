using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Clients.Handler
{
    public class C2S_UserLoginHandler : AMHandler<S2C_RoleInfo>
    {
        protected override void Run(Session session, S2C_RoleInfo message)
        {
            Log.Debug("收到用户信息:" + message.ToString());
        }
    }
}
