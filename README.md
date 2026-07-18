# Campaign Core

A turn-based RPG backend built in .NET with Clean Architecture, demonstrating real backend depth — resource-based authorization, JWT auth, the Repository/Unit-of-Work pattern, and a full unit + integration test suite. Players register, create campaigns, join others' campaigns via share codes, roll up characters, and fight monsters in turn-based combat.

> **Status:** Actively developed. The backend implements a complete vertical slice — auth, campaigns, characters, and combat all work end-to-end for the Necromancer class. Remaining playable classes, refresh tokens, and a React frontend are on the roadmap (see [Roadmap](#roadmap)).

## Architecture

The solution follows Clean Architecture with a strict dependency rule — the domain core depends on nothing outward:

| Project | Responsibility |
|---|---|
| **RPG.Core** | Domain entities, business services, and interfaces. Zero dependency on EF Core or any infrastructure — all persistence is accessed through interfaces defined here. |
| **RPG.Infrastructure** | EF Core `DbContext`, repository implementations, and framework-specific providers (JWT token generation, password hashing, caching). |
| **RPG.API** | ASP.NET Core controllers, DTOs, and composition root. The only layer that knows about HTTP and authorization. |
| **RPG.Tests** | xUnit + Moq unit tests over the domain services. |
| **RPG.API.IntegrationTests** | Full-stack tests via `WebApplicationFactory`, exercising the real DI container, routing, and a real PostgreSQL instance spun up with Testcontainers. |

### Key engineering decisions

- **Repository + Unit of Work.** Repositories only track changes; they never call `SaveChanges`. Committing is a business decision owned by the service orchestrating an operation, via `IUnitOfWork.CompleteAsync()` — giving atomic multi-repository commits per logical operation for free.
- **Authorization lives in controllers**, using ASP.NET Core resource-based authorization checked against the actual loaded entity — because attribute-based `[Authorize]` alone can't answer "does *this* user own *this* campaign?"
- **Data-driven character classes.** Rather than a subclass-per-class hierarchy, characters are a single entity keyed by a `PlayableClass` enum, with starting stats and abilities resolved from providers. Combat abilities are registered via keyed dependency injection and scoped to the class allowed to use them.
- **Ephemeral monsters.** Monsters are never persisted — they live in an in-memory cache for the duration of an encounter, since only the player's state is meaningful after combat.
- **Deliberate separation over premature abstraction.** Combat, campaign lifecycle, and character management stay as independent, separately-testable services composed together, rather than merged into a "god service."

## Tech stack

- **.NET 10** / ASP.NET Core
- **Entity Framework Core** + **PostgreSQL** (Npgsql)
- **JWT bearer authentication** (self-issued) with **BCrypt** password hashing
- **Mapster** for entity ↔ DTO mapping
- **xUnit** + **Moq** for unit tests
- **Testcontainers** + **Respawn** for isolated integration tests
- **GitHub Actions** for CI

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- **PostgreSQL** (to run the app)
- **Docker** (only needed to run the integration test suite — Testcontainers manages the database)

### Configuration

The app reads configuration from a `.env` file at the API project root. Create one with your database connection and JWT settings:

```env
ConnectionStrings__DefaultConnection=Host=localhost;Database=campaigncore;Username=postgres;Password=yourpassword
Jwt__SigningKey=your-signing-key-at-least-32-bytes-long
Jwt__Issuer=CampaignCore
Jwt__Audience=CampaignCore.Client
```

### Run

```bash
# Apply migrations
dotnet ef database update --project RPG.Infrastructure --startup-project RPG.API

# Start the API
dotnet run --project RPG.API
```

The API serves on `http://localhost:5251` (or `https://localhost:7290`).

## Testing

```bash
# Everything (unit + integration)
dotnet test RPG.slnx

# Unit tests only (no Docker required)
dotnet test RPG.Tests
```

Unit tests cover the branching domain logic (damage calculation, the campaign-code uniqueness retry loop, combat resolution). Integration tests boot the real application against a throwaway PostgreSQL container and exercise the full request pipeline — auth, DI resolution, routing, and persistence — with per-test database resets via Respawn.

## API overview

| Area | Endpoints |
|---|---|
| **Auth** | Register, login (issues a JWT) |
| **Campaigns** | Create, fetch owned/shared campaigns, join via share code, add a character, delete |
| **Characters** | Fetch full detail or roster, level up, delete |
| **Combat** | Execute a turn against a cached monster encounter |

All endpoints except registration and login require a bearer token.

## Roadmap

- [ ] Wire up remaining playable classes (Wizard, Rogue, Warrior)
- [ ] Refresh tokens + rotation for token revocation
- [ ] Server-side validation of level-up stat allocation
- [ ] Read endpoint for campaign action history
- [ ] Migrate configuration to layered `appsettings` + User Secrets
- [ ] React frontend