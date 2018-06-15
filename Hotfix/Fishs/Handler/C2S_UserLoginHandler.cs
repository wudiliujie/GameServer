using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2S_UserLoginHandler : AMHandler<C2S_UserLogin>
    {
        public static int count = 0;
        protected override void Run(Session session, C2S_UserLogin message)
        {
            Log.Debug("请求");
            count++;
        }
    }
}
