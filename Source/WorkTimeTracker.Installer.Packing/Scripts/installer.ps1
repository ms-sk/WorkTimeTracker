param($solutionDir, $projectDir, $configuration)

$bin = "bin\$configuration\net6.0-windows"
$solutionOutputDir = "$projectDir..\$bin"
$projectOutputDir = "$projectDir$bin"

Compress-Archive -Path "$solutionOutputDir\*" -Force -DestinationPath Content.zip -CompressionLevel Optimal

& cmd /c copy /b "$projectOutputDir\*.*" + "Content.zip" "$solutionDir\WorkTimeTracker.Setup.exe"

Remove-Item Content.zip