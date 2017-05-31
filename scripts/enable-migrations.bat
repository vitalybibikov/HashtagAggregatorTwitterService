@echo on
echo Started
SET CURRENT_PATH=%~dp0
SET BUILD_PATH=%~dp0%..\backend\HashtagAggregatorTwitterService

cd %BUILD_PATH%

dotnet ef migrations add InitialCreate

echo Finished
@echo off
dotnet ef migrations add InitialCreate


