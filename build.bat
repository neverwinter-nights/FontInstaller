SET SOLUTION="FontInstaller.sln"
SET DEVENV="C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe"

%DEVENV% %SOLUTION% /Rebuild Debug
%DEVENV% %SOLUTION% /Rebuild Release
