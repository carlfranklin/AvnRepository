# AI Current

Concise checkpoint of the current task: goal, current state, relevant files/classes, what has been tried, what worked or failed, unresolved problems, and recommended next steps.

## Current Task

- **Completed 2026-08-31:** Upgraded project from net9.0 to net10.0. Changes: `AvnRepository/AvnRepository.csproj` — TargetFramework net10.0, Microsoft.EntityFrameworkCore 10.0.11, package Version 10.0.1. Verified: `dotnet build AvnRepository.sln` → 0 warnings, 0 errors; package `AvnRepository.10.0.1.nupkg` produced. Not committed (user commits).
- **In progress 2026-09-01:** NuGet trusted publishing. Failure #1: invalid `nuget: write` permission key → parse error. Fixed with `id-token: write` + `NuGet/login@v1`. Failure #2: `dotnet pack` MSB3030 on clean checkout due to `GeneratePackageOnBuild=True` — fixed with `-p:GeneratePackageOnBuild=false` (verified locally). Failure #3 (run 33453889089): push step `dotnet nuget push ./artifacts/*.nupkg` failed with "File does not exist" because the default shell on windows-latest is PowerShell 7, which does NOT expand `*` globs — fixed by adding `shell: pwsh` and resolving the nupkg via `Get-ChildItem -Filter *.nupkg | Select-Object -First 1`. NUGET_USER secret IS set (login step now passes). **Outstanding (Carl's job):** (1) commit + push the glob fix, (2) delete + re-push tag v10.0.1. NuGet policy already Active.
