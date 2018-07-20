@echo off
if exist publish rd /s /q publish
set Build="%SYSTEMDRIVE%\Program Files (x86)\MSBuild\14.0\Bin\MsBuild.exe"
%Build% src\Server\AfxTcpFileServerSample.WinService\AfxTcpFileServerSample.WinService.csproj /t:Rebuild /p:Configuration=Release /p:OutputPath="..\..\..\publish\server"
%Build% src\Client\Client.WinMain\Client.WinMain.csproj /t:Rebuild /p:Configuration=Release /p:OutputPath="..\..\..\publish\client"
cd Publish
del /q/s *.pdb
