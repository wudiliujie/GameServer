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
}
