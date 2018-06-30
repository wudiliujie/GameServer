cd Server.Location

dotnet build 
xcopy ..\bin\location ..\run\Location /y
cd ..\run\Location
start "Location" dotnet Server.Location.dll 1 ..\..\Config\StartConfig\LocalAllServer.json
rem pause
