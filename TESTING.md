# Testing Notes

This environment does not include the .NET SDK, so commands such as `dotnet build` or `dotnet test` are unavailable here (the CLI binary is missing). When attempting to run them the shell responds with `bash: command not found: dotnet`, which is why earlier test results were reported with a warning icon. Run the usual `.NET` commands on a machine that has the SDK installed to validate the solution locally.
