REM %WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild  "%~dp0Hibu.Sam.Concordance.sln" /t:Rebuild /p:Configuration=Release /p:OutputPath="../Output"
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild  "%~dp0Hibu.Sam.Concordance.WebApiServer\Hibu.Sam.Concordance.WebApiServer.csproj" /t:build /P:Configuration=Release /p:OutputPath="../Output"
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild  "%~dp0Hibu.Sam.Concordance.Client\Hibu.Sam.Concordance.Client.csproj" /t:build /P:Configuration=Release /p:OutputPath="../Output"
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild  "%~dp0Hibu.Sam.Concordance.Web\Hibu.Sam.Concordance.Web.csproj" /T:Package /P:Configuration=Release /p:OutputPath="../Output"

PAUSE