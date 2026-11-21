@echo off
title Student Course Registration System - 01CE1523
echo ======================================================================
echo    MARWADI UNIVERSITY - DEPARTMENT OF COMPUTER ENGINEERING
echo    .NET Technologies (01CE1523) - ASP.NET Core MVC Project
echo    Starting Student Course Registration Web App...
echo ======================================================================
echo.
cd /d "%~dp0"
echo [1/2] Opening browser at http://localhost:5188 ...
start http://localhost:5188
echo [2/2] Running 'dotnet run' ...
echo.
echo ======================================================================
echo Project is running! Press Ctrl+C in this window anytime to stop.
echo ======================================================================
dotnet run
pause
