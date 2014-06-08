@echo off
call build

if not %errorlevel% == 0 (
	echo Error build project. Cannot pack the project
	exit /b errorlevel
)

nuget pack src\SpeedySolutions.Web.OpenGraph\SpeedySolutions.Web.OpenGraph.csproj -Prop Configuration=Release