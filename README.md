# Campaign Core

[![CI](https://github.com/dnoetz/CampaignCore/actions/workflows/ci.yml/badge.svg)](https://github.com/dnoetz/CampaignCore/actions/workflows/ci.yml)

A turn-based RPG campaign management platform built in .NET with Clean Architecture — demonstrating production-grade backend depth (resource-based authorization, JWT auth, the Repository/Unit-of-Work pattern, and a full unit + integration test suite). Players register, create campaigns, join others' campaigns via share codes, roll up characters, and fight monsters in turn-based combat.

**🔗 Live demo:** https://brave-pebble-07fd05f0f.7.azurestaticapps.net/

> **Status:** Deployed and functional end to end. The full slice — auth, campaigns, characters, and combat — runs live on Azure. Remaining playable classes, refresh tokens, and various refinements are on the [roadmap](#roadmap).

## Architecture

Clean Architecture with a strict dependency rule — the domain core depends on nothing outward:

| Project | Responsibility |
|---|---|
| **RPG.Core** | Domain entities, business services, and interfaces. Zero dependency on EF Core or any infrastructure — all persistence is accessed through interfaces defined here. |
| **RPG.Infrastructure** | EF Core `DbContext`, repository implementations, and framework-specific providers (JWT token generation, password hashing, caching). |
| **RPG.API** | ASP.NET Core controllers, DTOs, and composition root. The only layer that knows about HTTP and authorization. |
| **RPG.Tests** | xUnit + Moq unit tests over the domain services. |
| **RPG.API.IntegrationTests** | Full-stack tests via `WebApplicationFactory`, exercising the real DI container, routing, and a real PostgreSQL instance spun up with Testcontainers. |
| **client** | React + TypeScript (Vite, Tailwind) frontend consuming the API. |

### Key engineering decisions

- **Repository + Unit of Work.** Repositories only track changes; they never call `SaveChanges`. Committing is a business decision owned by the service orchestrating an operation, via `IUnitOfWork.CompleteAsync()` — giving atomic multi-repository commits per logical operation for free.
- **Authorization lives in controllers**, using resource-based authorization checked against the actual loaded entity — because attribute-based `[Authorize]` alone can't answer "does *this* user own *this* campaign?"
- **Data-driven character classes.** Rather than a subclass-per-class hierarchy, characters are a single entity keyed by a `PlayableClass` enum, with starting stats and abilities resolved from providers. Combat abilities are registered via keyed dependency injection and scoped to the class allowed to use them.
- **Ephemeral monsters.** Monsters are never persisted — they live in an in-memory cache for the duration of an encounter, since only the player's state matters after combat.
- **Deliberate separation over premature abstraction.** Combat, campaign lifecycle, and character management stay as independent, separately-testable services composed together, rather than merged into a "god service."

## Tech stack

- **Backend:** .NET 10, C#, ASP.NET Core, Entity Framework Core
- **Database:** PostgreSQL (Npgsql)
- **Auth:** self-issued JWT bearer tokens, BCrypt password hashing, resource-based authorization
- **Mapping:** Mapster
- **Frontend:** React, TypeScript, Vite, Tailwind CSS
- **Testing:** xUnit, Moq, Testcontainers, Respawn
- **DevOps:** Docker, GitHub Container Registry, Azure App Service, Azure Static Web Apps, Azure Database for PostgreSQL, GitHub Actions CI

## Deployment

The whole stack runs on Azure:

- **API** — containerized with Docker, image published to **GitHub Container Registry**, running on **Azure App Service (Web App for Containers)**.
- **Database** — **Azure Database for PostgreSQL (Flexible Server)**.
- **Frontend** — built with Vite and hosted on **Azure Static Web Apps**, auto-deployed via GitHub Actions on push.
- **Configuration** — secrets and connection strings are injected as environment variables (App Service settings); nothing sensitive lives in source control.
- **CI** — **GitHub Actions** builds the solution and runs the full test suite (unit + integration against a real PostgreSQL container via Testcontainers) on every push and pull request.

## Getting started (local)

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (for the frontend)
- **PostgreSQL** (to run the API)
- **Docker** (only needed to run the integration test suite — Testcontainers manages the database)

### Configuration

The API reads configuration from a `.env` file at the API project root:

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

# Start the API (http://localhost:5251)
dotnet run --project RPG.API

# Start the frontend (http://localhost:5173)
cd client
npm install
npm run dev
```

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
| **Campaigns** | Create, fetch owned/shared/playable campaigns, join via share code, add a character, delete |
| **Characters** | Fetch full detail or roster, level up, delete |
| **Combat** | Execute a turn against a cached monster encounter |
| **Dice** | Roll for initiative and damage |

All endpoints except registration and login require a bearer token.

## Roadmap

- [ ] Wire up remaining playable classes (Wizard, Rogue, Warrior)
- [ ] Refresh tokens + rotation for token revocation
- [ ] Server-side validation of level-up stat allocation
- [ ] Move deploy config to fully env-driven (`VITE_API_URL`, config-driven CORS origins)
- [ ] Combat/UI polish and loading states