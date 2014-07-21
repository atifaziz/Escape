@echo off
setlocal
chcp 1252 > nul
for %%i in (NuGet.exe) do set nuget=%%~dpnx$PATH:i
if "%nuget%"=="" goto :nonuget
cd "%~dp0"
if not exist dist md dist > nul
for /d %%d in (*) do if %%d==dist set DISTDIR=%%d
if not defined DISTDIR (
    echo Error creating directory `dist`.hg st
    exit /b 3
)
call build /v:m ^
 && NuGet pack -OutputDirectory dist Escape.nuspec -Symbols
goto :EOF

:nonuget
echo NuGet executable not found in PATH
echo For more on NuGet, see http://nuget.codeplex.com
exit /b 2
