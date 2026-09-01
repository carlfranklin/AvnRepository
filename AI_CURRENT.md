# AI Current

Concise checkpoint of the current task: goal, current state, relevant files/classes, what has been tried, what worked or failed, unresolved problems, and recommended next steps.

## Current Task

- **Completed 2026-08-31:** Upgraded project from net9.0 to net10.0. Changes: `AvnRepository/AvnRepository.csproj` — TargetFramework net10.0, Microsoft.EntityFrameworkCore 10.0.11, package Version 10.0.1. Verified: `dotnet build AvnRepository.sln` → 0 warnings, 0 errors; package `AvnRepository.10.0.1.nupkg` produced. Not committed (user commits).
- **In progress 2026-08-31:** NuGet trusted publishing. First workflow version failed (invalid `nuget: write` permission key → "Unexpected value 'nuget'" parse error, 0s failure, no jobs). Fixed: `id-token: write` + `NuGet/login@v1` (user from secret `NUGET_USER`) + push with temp key. **Outstanding (Carl's job):** (1) commit + push the fixed workflow, (2) add repo secret `NUGET_USER` = `appvnext` (Settings → Secrets and variables → Actions), (3) delete old tag `git push origin :refs/tags/v10.0.1`, re-tag `git tag v10.0.1 && git push origin v10.0.1`. NuGet policy already Active — no changes needed there.
