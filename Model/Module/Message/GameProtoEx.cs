using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public partial class ResponseMessage : IResponse
    {

    }
    [Message(MSG.C2SUserLogin)]
    public partial class C2S_UserLogin : IRequest
    {
    }
    [Message(MSG.C2SUserLogin)]
    public partial class S2C_UserLogin : IResponse
    {
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int RpcId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
