using ETHotfix.Fishs.Systems;
using ETModel;
using Model.Fishs.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix.Fishs.Locations.Handlers
{
    [MessageHandler(AppType.Location)]
    public class G2L_GetMapAddressHandler : AMRpcHandler<G2L_GetMapAddress, L2G_GetMapAddress>
    {
        protected override void Run(Session session, G2L_GetMapAddress message, Action<L2G_GetMapAddress> reply)
        {
            L2G_GetMapAddress response = new L2G_GetMapAddress();
            Log.Debug("获取地址:" + message);
            var address = Game.Scene.GetComponent<ServerManagerComponent>().GetAddressByType(message.MapType);
            if (address == "")
            {
                response.Tag = ErrorCode.ERR_AccountOrPasswordError;//地址不存在
            }
            else
            {
                response.Address = address;
            }
            reply(response);
        }
    }
}
