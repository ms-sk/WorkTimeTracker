Compress-Archive -Path ".\Release" -Force -DestinationPath Content.zip -CompressionLevel Optimal

& cmd /c copy /b ".\WorkTimeTracker.Installer.Packing.exe" + "Content.zip" "WorkTimeTracker.exe"