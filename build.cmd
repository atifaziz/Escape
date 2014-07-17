@echo off
setlocal
cd "%~dp0"
set MSBUILD=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
if not exist "%MSBUILD%" (
    echo The .NET Framework 4.0 does not appear to be installed on this 
    echo machine, which is required to build the solution.
    exit /b 1
)
call :msbuild Debug %* && call :msbuild Release %*
goto :EOF

:msbuild
"%MSBUILD%" Escape.sln "/p:Configuration=%~1" /v:m %2 %3 %4 %5 %6 %7 %8 %9
goto :EOF
