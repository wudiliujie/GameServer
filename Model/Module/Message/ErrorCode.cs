namespace ETModel
{
	public static class ErrorCode
	{
        public const int ERR_Success = 0;
        public const int ERR_NotFoundActor = 2;
        public const int ERR_ActorNoMailBoxComponent = 3;
        public const int ERR_ActorTimeOut = 4;
        public const int ERR_PacketParserError = 5;

        public const int ERR_AccountOrPasswordError = 102;
        public const int ERR_SessionActorError = 103;
        public const int ERR_NotFoundUnit = 104;
        public const int ERR_ConnectGateKeyError = 105;

        public const int ERR_RpcFail = 2001;
        public const int ERR_SocketDisconnected = 2002;
        public const int ERR_ReloadFail = 2003;
        public const int ERR_ActorLocationNotFound = 2004;

        public const int ERR_ServerIdExt = 3001;//服务器已经存在
        public const int ERR_Server_NO_Start = 3002;//服务器没有启动

        public const int ERR_RoomNOExist = 4001; //房间不存在
        public const int ERR_Room_Full = 4002; //房间人说已满

        public const int ERR_Exception = 100000;

        public const int ERR_SessionDispose = 100001;
        public static bool IsRpcNeedThrowException(int error)
        {
            if (error == 0)
            {
                return false;
            }

            if (error > ERR_Exception)
            {
                return false;
            }

            return true;
        }
    }
}