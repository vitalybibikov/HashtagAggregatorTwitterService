@echo off

echo Started

SET currentPath=%~dp0
SET twitterServicePath=%~dp0..\backend\HashtagAggregatorTwitter.Service

setx ASPNETCORE_ENVIRONMENT dev

cd %twitterServicePath%

echo %twitterServicePath%

dotnet run --no-launch-profile

cd %currentPath%

echo Finished


