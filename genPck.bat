cd Server.Tool

dotnet run
cd ..
cd Proto
protoc --csharp_out=./ ./GameProto.proto
copy GameProto.cs ..\Model\Module\Message\GameProto.cs 
copy GameProtoEx.cs ..\Model\Module\Message\GameProtoEx.cs 
pause
