@echo off
echo Building EMtools...
echo.

dotnet restore
if %errorlevel% neq 0 (
    echo Failed to restore packages.
    pause
    exit /b %errorlevel%
)

dotnet build -c Release
if %errorlevel% neq 0 (
    echo Build failed.
    pause
    exit /b %errorlevel%
)

echo.
echo Build successful!
echo Executable location: EMtools\bin\Release\net9.0-windows\EMtools.exe
echo.
pause
