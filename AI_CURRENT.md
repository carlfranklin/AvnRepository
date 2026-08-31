# AI Current

Concise checkpoint of the current task: goal, current state, relevant files/classes, what has been tried, what worked or failed, unresolved problems, and recommended next steps.

## Current Task

- **Completed 2026-08-31:** Upgraded project from net9.0 to net10.0. Changes: `AvnRepository/AvnRepository.csproj` — TargetFramework net10.0, Microsoft.EntityFrameworkCore 10.0.11, package Version 10.0.1. Verified: `dotnet build AvnRepository.sln` → 0 warnings, 0 errors; package `AvnRepository.10.0.1.nupkg` produced. Not committed (user commits).
- **Completed 2026-08-31:** Added `.github/workflows/publish.yml` for NuGet trusted publishing (pack Release + push on `v*` tag or manual dispatch, `nuget: write` permission, no API key). YAML validated. **Outstanding (Carl's job):** link carlfranklin/AvnRepository to the AvnRepository package on nuget.org → account/trustedpublishing, then commit + tag `v10.0.1` to publish.
