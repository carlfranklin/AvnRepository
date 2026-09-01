# AI Memory

Durable knowledge discovered while working on this project: important design decisions, non-obvious behavior, solutions, APIs, dependencies, configuration details, and lessons learned.

## Knowledge

- Project is a single classlib `AvnRepository` (sln at repo root) that packs a NuGet package on build (`GeneratePackageOnBuild=True`, `PackAsTool=True`); package version in csproj tracks the .NET major version (was 9.0.1 on net9.0, now 10.0.1 on net10.0). **Gotcha:** `dotnet pack` on a clean checkout FAILS with MSB3030 (missing dll/pdb/deps.json) because `GeneratePackageOnBuild=True` breaks the pack pipeline — must pass `-p:GeneratePackageOnBuild=false` (the publish workflow does this).
- Only dependency is `Microsoft.EntityFrameworkCore` (currently 10.0.11). No global.json, Directory.Build.props, or nuget.config in the repo.
- Machine has .NET SDKs 5–10 installed; `dotnet --version` reports 10.0.400.
- README.md documents the API surface but contains no framework-version references, so framework upgrades only touch the csproj.
- NuGet publishing uses **trusted publishing** (no long-lived API key): `.github/workflows/publish.yml` packs in Release and pushes on tag push (`v*`) or manual dispatch. Correct mechanism (per NuGet docs, verified 2026-08-31): job needs `permissions: id-token: write` (NOT `nuget: write` — that key doesn't exist and fails workflow parsing with "Unexpected value 'nuget'"), then `NuGet/login@v1` action with `user: ${{ secrets.NUGET_USER }}` exchanges the OIDC token for a 1-hour temp API key, used in `dotnet nuget push --api-key`. Repo secret `NUGET_USER` must be the nuget.org profile name of the policy owner (`appvnext`). Policy on nuget.org (owner appvnext, repo carlfranklin/AvnRepository, workflow publish.yml, glob AvnRepository) is Active. Local `dotnet nuget push` still needs an API key — trusted publishing only works from GitHub Actions in the linked repo.
