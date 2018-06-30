using System;
using ETModel;
using Google.Protobuf;

namespace ETHotfix
{
    public class InnerMessageDispatcher : IMessageDispatcher
    {
        public void Dispatch(Session session, Packet packet)
        {
            Console.WriteLine("inner dispath");
            IMessage message;
            try
            {
                message = session.Network.Entity.GetComponent<OpcodeTypeComponent>().GetNewMessage(packet.Opcode);
                message.MergeFrom(packet.Bytes, packet.Offset, packet.Length);
            }
            catch (Exception e)
            {
                // 出现任何解析消息异常都要断开Session，防止客户端伪造消息
                Log.Error(e);
                session.Error = ErrorCode.ERR_PacketParserError;
                session.Network.Remove(session.Id);
                return;
            }


            // 收到actor消息,放入actor队列
            if (message is IActorMessage iActorMessage)
            {
                Entity entity = (Entity)Game.EventSystem.Get(iActorMessage.ActorId);
                if (entity == null)
                {
                    Log.Warning($"not found actor: {iActorMessage.ActorId}");
                    ActorResponse response = new ActorResponse
                    {
                        Tag = ErrorCode.ERR_NotFoundActor,
                        RpcId = iActorMessage.RpcId
                    };
                    session.Reply(response);
                    return;
                }

                MailBoxComponent mailBoxComponent = entity.GetComponent<MailBoxComponent>();
                if (mailBoxComponent == null)
                {
                    ActorResponse response = new ActorResponse
                    {
                        Tag = ErrorCode.ERR_ActorNoMailBoxComponent,
                        RpcId = iActorMessage.RpcId
                    };
                    session.Reply(response);
                    Log.Error($"actor没有挂载MailBoxComponent组件: {entity.GetType().Name} {entity.Id}");
                    return;
                }

                mailBoxComponent.Add(new ActorMessageInfo() { Session = session, Message = iActorMessage });
                return;
            }

            Game.Scene.GetComponent<MessageDispatherComponent>().Handle(session, new MessageInfo(packet.Opcode, message));
        }
    }
}
