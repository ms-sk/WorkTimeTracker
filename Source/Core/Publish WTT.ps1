﻿[string]$source = 'C:\Dev\WorkTimeTracker\Source\bin\Debug\net6.0-windows\*'
[string]$destination = 'C:\WorkTimeTracker\'

Stop-Process -Name WorkTimeTracker -ErrorAction Ignore
Copy-Item -Force -Recurse $source -Destination $destination