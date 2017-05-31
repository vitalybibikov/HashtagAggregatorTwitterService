@echo on
echo Started

SET CURRENT_PATH=%~dp0

cd "C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator"


AzureStorageEmulator.exe stop
AzureStorageEmulator.exe init /server .
AzureStorageEmulator.exe start

cd %CURRENT_PATH=%

echo Finished