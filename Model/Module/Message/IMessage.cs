using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtoBuf;

// 不要在这个文件加[ProtoInclude]跟[BsonKnowType]标签,加到InnerMessage.cs或者OuterMessage.cs里面去
namespace ETModel
{
    public interface IRequest : IMessage
    {
        int RpcId { get; set; }
    }

    public interface IResponse : IMessage
    {
        int Tag { get; set; }
        string Message { get; set; }
        int RpcId { get; set; }
    }
}