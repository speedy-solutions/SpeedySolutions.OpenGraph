@echo off

%systemroot%\Microsoft.NET\Framework\v4.0.30319\msbuild .\src\SpeedySolutions.Web.OpenGraph\SpeedySolutions.Web.OpenGraph.csproj /p:Configuration=Release

if not %errorlevel% == 0 echo Cannot build project. Are you sure you have the Roslyn preview installed in Visual Studio?

exit /b errorlevel