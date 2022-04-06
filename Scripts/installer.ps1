Compress-Archive -Path "..\Source\bin\Release" -Force -CompressionLevel Optimal -DestinationPath "..\Releases\1\Content.zip"

cmd /c copy /b "..\Source\WorkTimeTracker.Installer.Packing\bin\Release\net6.0\WorkTimeTracker.Installer.Packing.exe" + "..\Releases\1\Content.zip" "..\Releases\1\WorkTimeTracker.exe"

Remove-Item "..\Releases\1\Content.zip"