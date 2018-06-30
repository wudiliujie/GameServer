cd Server.Gate

dotnet build 
xcopy ..\bin\Gate ..\run\Gate /y
cd ..\run\Gate
start "Gate" dotnet Server.Gate.dll 2 ..\..\Config\StartConfig\LocalAllServer.json
rem pause
