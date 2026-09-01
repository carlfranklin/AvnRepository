# AI Current

Concise checkpoint of the current task: goal, current state, relevant files/classes, what has been tried, what worked or failed, unresolved problems, and recommended next steps.

## Current Task

- **Completed 2026-08-31:** Upgraded project from net9.0 to net10.0. Changes: `AvnRepository/AvnRepository.csproj` — TargetFramework net10.0, Microsoft.EntityFrameworkCore 10.0.11, package Version 10.0.1. Verified: `dotnet build AvnRepository.sln` → 0 warnings, 0 errors; package `AvnRepository.10.0.1.nupkg` produced. Not committed (user commits).
- **Completed 2026-09-01:** NuGet trusted publishing is live. Run 33454211875 (tag v10.0.1 → 95bb95d) succeeded: `AvnRepository.10.0.1.nupkg` pushed to nuget.org via OIDC trusted publishing. Debug history: (1) invalid `nuget: write` permission → `id-token: write` + `NuGet/login@v1`; (2) MSB3030 on clean checkout → `-p:GeneratePackageOnBuild=false` on pack; (3) pwsh on windows-latest doesn't expand `*` globs → resolve nupkg via `Get-ChildItem -Filter *.nupkg | Select-Object -First 1`. Repo secret `NUGET_USER` = `appvnext` is set. Future releases: bump csproj Version, commit, tag `vX.Y.Z`, push tag — workflow handles the rest.
- **Completed 2026-09-01:** Republished as **10.0.2** — removed `<PackAsTool>True</PackAsTool>` (9.0.0–10.0.1 were published as dotnet tools and unusable as PackageReference; NU1212). Committed 59524f8, tag v10.0.2, published via trusted publishing (verified on nuget.org as a library).
- **In progress 2026-09-01:** Memory-system restructure — `startup.md` reduced to a one-line pointer to agentmemory; `AI_RULES.md` and `ahem.wav` deleted (canonical copies live in agentmemory). Left uncommitted for Carl.
