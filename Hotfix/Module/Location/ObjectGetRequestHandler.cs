using System;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Location)]
	public class ObjectGetRequestHandler : AMRpcHandler<ObjectGetRequest, ObjectGetResponse>
	{
		protected override async void Run(Session session, ObjectGetRequest message, Action<ObjectGetResponse> reply)
		{
			ObjectGetResponse response = new ObjectGetResponse();
			try
			{
				var  info = await Game.Scene.GetComponent<LocationComponent>().GetAsync(message.Key);
				if (info == null)
				{
					response.Tag = ErrorCode.ERR_ActorLocationNotFound;
                    info = new ObjectInfo();
				}
                response.Item = info;
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}