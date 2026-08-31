# AI Memory

Durable knowledge discovered while working on this project: important design decisions, non-obvious behavior, solutions, APIs, dependencies, configuration details, and lessons learned.

## Knowledge

- Project is a single classlib `AvnRepository` (sln at repo root) that packs a NuGet package on build (`GeneratePackageOnBuild=True`, `PackAsTool=True`); package version in csproj tracks the .NET major version (was 9.0.1 on net9.0, now 10.0.1 on net10.0).
- Only dependency is `Microsoft.EntityFrameworkCore` (currently 10.0.11). No global.json, Directory.Build.props, or nuget.config in the repo.
- Machine has .NET SDKs 5–10 installed; `dotnet --version` reports 10.0.400.
- README.md documents the API surface but contains no framework-version references, so framework upgrades only touch the csproj.
