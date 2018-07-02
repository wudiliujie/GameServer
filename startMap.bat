cd Server.Map

dotnet build 
xcopy ..\bin\Map ..\run\Map /y
cd ..\run\Map
start "Map" dotnet Server.Map.dll 3 ..\..\Config\StartConfig\LocalAllServer.json
rem pause
