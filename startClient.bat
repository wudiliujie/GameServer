cd Server.Client

dotnet build 
xcopy ..\bin\Client ..\run\Client /y
cd ..\run\Client
start "Client"  dotnet Client.dll 10001 ..\..\Config\StartConfig\LocalAllServer.json
rem pause
