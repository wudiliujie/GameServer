using System;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Location)]
	public class ObjectAddRequestHandler : AMRpcHandler<ObjectAddRequest, ObjectAddResponse>
	{
		protected override void Run(Session session, ObjectAddRequest message, Action<ObjectAddResponse> reply)
		{
			ObjectAddResponse response = new ObjectAddResponse();
			try
			{
				Game.Scene.GetComponent<LocationComponent>().Add(message.Item);
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}