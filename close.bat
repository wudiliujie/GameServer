set p=%~dp0
set "p=%p:\=\\%"
wmic process where "ExecutablePath Like '%%dotnet%%'" call terminate