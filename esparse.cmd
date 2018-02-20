@echo off
setlocal
set BINPATH=%~dp0bin\Release\netcoreapp2.0\Esparse.dll
if not exist "%BINPATH%" goto :error
dotnet "%BINPATH%" %*
goto :EOF

:error
echo>&2 Esparse release build not found. Did you forget to build?
exit /b 1
