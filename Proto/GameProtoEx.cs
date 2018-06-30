using System;
namespace ETModel
{
	public partial class ResponseMessage : IResponse
	{
	}
	public partial class C2S_UserLogin : IRequest
	{
	}
	public partial class S2C_UserLogin : IResponse
	{
	}
	public partial class S2L_RegisterServer : IRequest
	{
	}
	public partial class G2M_CreateUnit : IRequest
	{
	}
	public partial class M2G_CreateUnit : IResponse
	{
	}
	public partial class G2L_GetMapAddress : IRequest
	{
	}
	public partial class L2G_GetMapAddress : IResponse
	{
	}
	public static class RegisterClass
	{
		public static void Register(this OpcodeTypeComponent self)
		{
			self.RegisterType(1, typeof(ResponseMessage), () => { return new ResponseMessage(); });
			self.RegisterType(2, typeof(C2WEB_UserLogin), () => { return new C2WEB_UserLogin(); });
			self.RegisterType(3, typeof(WEB2C_UserLogin), () => { return new WEB2C_UserLogin(); });
			self.RegisterType(4, typeof(C2WEB_CreateRole), () => { return new C2WEB_CreateRole(); });
			self.RegisterType(5, typeof(WEB2C_CreateRole), () => { return new WEB2C_CreateRole(); });
			self.RegisterType(6, typeof(C2S_UserLogin), () => { return new C2S_UserLogin(); });
			self.RegisterType(7, typeof(S2C_UserLogin), () => { return new S2C_UserLogin(); });
			self.RegisterType(8, typeof(SaveRoleInfo), () => { return new SaveRoleInfo(); });
			self.RegisterType(9, typeof(S2C_RoleInfo), () => { return new S2C_RoleInfo(); });
			self.RegisterType(10, typeof(ServerHeart), () => { return new ServerHeart(); });
			self.RegisterType(11, typeof(S2L_RegisterServer), () => { return new S2L_RegisterServer(); });
			self.RegisterType(12, typeof(G2M_CreateUnit), () => { return new G2M_CreateUnit(); });
			self.RegisterType(13, typeof(M2G_CreateUnit), () => { return new M2G_CreateUnit(); });
			self.RegisterType(14, typeof(G2L_GetMapAddress), () => { return new G2L_GetMapAddress(); });
			self.RegisterType(15, typeof(L2G_GetMapAddress), () => { return new L2G_GetMapAddress(); });
		}
	}
}
