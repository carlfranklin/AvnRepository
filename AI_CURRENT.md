# AI Current

Concise checkpoint of the current task: goal, current state, relevant files/classes, what has been tried, what worked or failed, unresolved problems, and recommended next steps.

## Current Task

- **Completed 2026-08-31:** Upgraded project from net9.0 to net10.0. Changes: `AvnRepository/AvnRepository.csproj` — TargetFramework net10.0, Microsoft.EntityFrameworkCore 10.0.11, package Version 10.0.1. Verified: `dotnet build AvnRepository.sln` → 0 warnings, 0 errors; package `AvnRepository.10.0.1.nupkg` produced. Not committed (user commits).
- **In progress 2026-08-31:** NuGet trusted publishing. Failure #1: invalid `nuget: write` permission key → parse error. Fixed with `id-token: write` + `NuGet/login@v1`. Failure #2 (run 33453332988): `dotnet pack` MSB3030 on clean checkout due to `GeneratePackageOnBuild=True` — fixed by adding `-p:GeneratePackageOnBuild=false` to the Pack step (verified locally). **Outstanding (Carl's job):** (1) commit + push the pack fix, (2) add repo secret `NUGET_USER` = `appvnext` (Settings → Secrets and variables → Actions) — NOT YET CONFIRMED DONE, (3) delete + re-push tag v10.0.1. NuGet policy already Active.
