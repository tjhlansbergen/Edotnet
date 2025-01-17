### Edotnet

# Getting started: 
- Navigate to EConsole
- `dotnet run`
- Paste path to .e script
- Enjoy

# Running tests:
From the root directory (the one where this readme is), run `dotnet test -v=quiet`. Expect results like:

```
Passed!  - Failed:     0, Passed:    27, Skipped:     0, Total:    27, Duration: 527 ms - EBuildIn.Tests.dll (net8.0)
Passed!  - Failed:     0, Passed:   128, Skipped:     0, Total:   128, Duration: 871 ms - EInterpreter.Tests.dll (net8.0)
Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration:  94 ms - EConsole.Tests.dll (net8.0)
```
